// SPDX-License-Identifier: Apache-2.0
//
// The OpenSearch Contributors require contributions made to
// this file be licensed under the Apache-2.0 license or a
// compatible open source license.
//
// Modifications Copyright OpenSearch Contributors. See
// GitHub history for details.
//
//  Licensed to Elasticsearch B.V. under one or more contributor
//  license agreements. See the NOTICE file distributed with
//  this work for additional information regarding copyright
//  ownership. Elasticsearch B.V. licenses this file to you under
//  the Apache License, Version 2.0 (the "License"); you may
//  not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
// 	http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing,
//  software distributed under the License is distributed on an
//  "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
//  KIND, either express or implied.  See the License for the
//  specific language governing permissions and limitations
//  under the License.
//

module Tests.YamlRunner.TestsExporter

open System
open System.Globalization
open Tests.YamlRunner.OperationExecutor
open Tests.YamlRunner.TestsReader
open System.Xml.Linq
open System.Xml.XPath

type SectionResults = string * ExecutionResult list
type FileResults = SectionResults list
type FolderResults = (YamlTestDocument * FileResults) list
type RunResults = (YamlTestFolder * FolderResults) list

let XElement(name, content: 'a list) = XElement(XName.Get name, Seq.toArray content)
let XAttribute(name, value) = XAttribute(XName.Get name, value)

let mapExecutionResult result =
    match result with
    | ExecutionResult.NotSkipped _ 
    | ExecutionResult.Succeeded _ -> None
    | ExecutionResult.Failed f ->
        let c = f.Context
        let message =
            let m = sprintf "%s: %s %s" result.Name (c.Operation.Log()) (f.Log())
            XAttribute("message", m)
        match f with
        | SeenException (_, e) -> 
            let error = XElement("error", [message])
            error.Value <- e.ToString()
            Some error
        | ValidationFailure (c, _) -> 
            let failure = XElement("failure", [message])
            match c.Stashes.ResponseOption with
            | Some r -> failure.Value <- r.DebugInformation
            | None -> failure.Value <- "Could not access response!"
            Some failure
    | ExecutionResult.Skipped (_, reason) ->
        Some <| XElement("skipped", [XAttribute("message", reason)])
        
let private timeOf result =
    match result with
    | ExecutionResult.Succeeded c -> !c.Elapsed
    | ExecutionResult.NotSkipped c -> !c.Elapsed
    | ExecutionResult.Failed f -> !f.Context.Elapsed
    | ExecutionResult.Skipped (c, _reason) -> !c.Elapsed

let testCasesSection document (results: FileResults) =
    results
    |> List.filter (fun (section, _) -> section <> "TEARDOWN")
    |> List.map (fun (section, results) ->
       let name = sprintf "%s - %s" document.FileInfo.Name section
       let time =
           let totalMs = results |> List.sumBy timeOf
           Convert.ToDouble(totalMs) / 1000.0
       let results =
           results
           |> List.map(fun r -> mapExecutionResult r)
           |> List.choose id
       let name = XAttribute("name", name)
       let time = XAttribute("time", time.ToString(CultureInfo.InvariantCulture))
    
       let testCase = XElement("testcase", [])
       testCase.Add(results, name, time)
       testCase
    )
    
let testCases (results: FolderResults) =
    results
    |> List.map (fun (document, sections) ->
       testCasesSection document sections
    )
    |> List.concat
    
let countTests (xElement:XElement) =
    let xp = xElement.XPathSelectElements 
    let x s = xp s |> Seq.length
    let testCases = x "//testcase" 
    let errors = x "//testcase[error]"
    let failed = x "//testcase[failure]"
    let skipped = x "//testcase[skipped]"
    let time =
        xp "//testcase[@time]"
        |> Seq.map (fun a -> Double.Parse (a.Attribute(XName.Get "time")).Value)
        |> Seq.sum
        
    xElement.Add
        (
            XAttribute("tests", testCases),
            XAttribute("failures", failed),
            XAttribute("errors", errors),
            XAttribute("disabled", skipped),
            XAttribute("time", time)
        )
    {|
       Tests = testCases;
       Failed = failed;
       Errors=errors;
       Skipped= skipped;
       Time = time
    |}

let testSuites (results: RunResults) =
    results
    |> List.map (fun (folder, documents) ->
        let name = XAttribute("name", folder.Folder)
        let testCases = testCases documents
        let suite = XElement("testsuite", [name])
        suite.Add(testCases)
        let _summary = countTests suite
        suite
    )

let Export (results: RunResults) (outputFile:string) =
    let suites = testSuites results
    let xml = XElement("testsuites", suites)
    let summary = countTests xml
    
    let fullPath = System.IO.Path.GetFullPath outputFile
    printfn "Persisting junit file to %s" fullPath
    
    xml.Save(outputFile)
    summary
    
let PrettyPrintResults (outputFile:string) =
    let fullPath = System.IO.Path.GetFullPath outputFile
    
    let xml = XDocument.Load fullPath
    let xp (e:XElement) = e.XPathSelectElements
    
    for suite in (xp xml.Root "//testsuite[testcase[failure|error]]") do
        Console.ForegroundColor <- ConsoleColor.Yellow
        printfn "%s" <| suite.Attribute(XName.Get "name").Value
        for testcase in (xp suite "testcase[failure|error]") do
            Console.ForegroundColor <- ConsoleColor.Blue
            printfn "  - %s" <| testcase.Attribute(XName.Get "name").Value
            for error in (xp testcase "failure|error") do
                Console.ForegroundColor <- ConsoleColor.Red
                printfn "    - %s" <| error.Attribute(XName.Get "message").Value
                Console.ForegroundColor <- ConsoleColor.Gray
                printfn "" 
                printfn "%s" <| error.Value
                printfn "" 
                
        Console.ResetColor()
    
    
