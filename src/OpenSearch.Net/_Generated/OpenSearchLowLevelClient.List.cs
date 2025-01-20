/* SPDX-License-Identifier: Apache-2.0
*
* The OpenSearch Contributors require contributions made to
* this file be licensed under the Apache-2.0 license or a
* compatible open source license.
*/
/*
* Modifications Copyright OpenSearch Contributors. See
* GitHub history for details.
*
*  Licensed to Elasticsearch B.V. under one or more contributor
*  license agreements. See the NOTICE file distributed with
*  this work for additional information regarding copyright
*  ownership. Elasticsearch B.V. licenses this file to you under
*  the Apache License, Version 2.0 (the "License"); you may
*  not use this file except in compliance with the License.
*  You may obtain a copy of the License at
*
*   http://www.apache.org/licenses/LICENSE-2.0
*
*  Unless required by applicable law or agreed to in writing,
*  software distributed under the License is distributed on an
*  "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
*  KIND, either express or implied.  See the License for the
*  specific language governing permissions and limitations
*  under the License.
*/
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//
// This file is automatically generated
// Please do not edit these files manually
// Run the following in the root of the repos:
//
//      *NIX        :   ./build.sh codegen
//      Windows     :   build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenSearch.Net;
using static OpenSearch.Net.HttpMethod;

// ReSharper disable InterpolatedStringExpressionIsNotIFormattable
// ReSharper disable once CheckNamespace
// ReSharper disable InterpolatedStringExpressionIsNotIFormattable
// ReSharper disable RedundantExtendsListEntry
namespace OpenSearch.Net.Specification.ListApi
{
    /// <summary>
    /// List APIs.
    /// <para>Not intended to be instantiated directly. Use the <see cref="IOpenSearchLowLevelClient.List"/> property
    /// on <see cref="IOpenSearchLowLevelClient"/>.
    /// </para>
    /// </summary>
    public partial class LowLevelListNamespace : NamespacedClientProxy
    {
        internal LowLevelListNamespace(OpenSearchLowLevelClient client)
            : base(client) { }

        /// <summary>GET on /_list <para>https://opensearch.org/docs/latest/api-reference/list/index/</para></summary>
        /// <param name="requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
        /// <remarks>Supported by OpenSearch servers of version 2.18.0 or greater.</remarks>
        public TResponse Help<TResponse>(HelpRequestParameters requestParameters = null)
            where TResponse : class, IOpenSearchResponse, new() =>
            DoRequest<TResponse>(GET, "_list", null, RequestParams(requestParameters));

        /// <summary>GET on /_list <para>https://opensearch.org/docs/latest/api-reference/list/index/</para></summary>
        /// <param name="requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
        /// <remarks>Supported by OpenSearch servers of version 2.18.0 or greater.</remarks>
        [MapsApi("list.help", "")]
        public Task<TResponse> HelpAsync<TResponse>(
            HelpRequestParameters requestParameters = null,
            CancellationToken ctx = default
        )
            where TResponse : class, IOpenSearchResponse, new() =>
            DoRequestAsync<TResponse>(GET, "_list", ctx, null, RequestParams(requestParameters));

        /// <summary>GET on /_list/indices <para>https://opensearch.org/docs/latest/api-reference/list/list-indices/</para></summary>
        /// <param name="requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
        /// <remarks>Supported by OpenSearch servers of version 2.18.0 or greater.</remarks>
        public TResponse Indices<TResponse>(IndicesRequestParameters requestParameters = null)
            where TResponse : class, IOpenSearchResponse, new() =>
            DoRequest<TResponse>(GET, "_list/indices", null, RequestParams(requestParameters));

        /// <summary>GET on /_list/indices <para>https://opensearch.org/docs/latest/api-reference/list/list-indices/</para></summary>
        /// <param name="requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
        /// <remarks>Supported by OpenSearch servers of version 2.18.0 or greater.</remarks>
        [MapsApi("list.indices", "")]
        public Task<TResponse> IndicesAsync<TResponse>(
            IndicesRequestParameters requestParameters = null,
            CancellationToken ctx = default
        )
            where TResponse : class, IOpenSearchResponse, new() =>
            DoRequestAsync<TResponse>(
                GET,
                "_list/indices",
                ctx,
                null,
                RequestParams(requestParameters)
            );

        /// <summary>GET on /_list/indices/{index} <para>https://opensearch.org/docs/latest/api-reference/list/list-indices/</para></summary>
        /// <param name="index">Comma-separated list of data streams, indexes, and aliases used to limit the request. Supports wildcards (`*`). To target all data streams and indexes, omit this parameter or use `*` or `_all`.</param>
        /// <param name="requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
        /// <remarks>Supported by OpenSearch servers of version 2.18.0 or greater.</remarks>
        public TResponse Indices<TResponse>(
            string index,
            IndicesRequestParameters requestParameters = null
        )
            where TResponse : class, IOpenSearchResponse, new() =>
            DoRequest<TResponse>(
                GET,
                Url($"_list/indices/{index:index}"),
                null,
                RequestParams(requestParameters)
            );

        /// <summary>GET on /_list/indices/{index} <para>https://opensearch.org/docs/latest/api-reference/list/list-indices/</para></summary>
        /// <param name="index">Comma-separated list of data streams, indexes, and aliases used to limit the request. Supports wildcards (`*`). To target all data streams and indexes, omit this parameter or use `*` or `_all`.</param>
        /// <param name="requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
        /// <remarks>Supported by OpenSearch servers of version 2.18.0 or greater.</remarks>
        [MapsApi("list.indices", "index")]
        public Task<TResponse> IndicesAsync<TResponse>(
            string index,
            IndicesRequestParameters requestParameters = null,
            CancellationToken ctx = default
        )
            where TResponse : class, IOpenSearchResponse, new() =>
            DoRequestAsync<TResponse>(
                GET,
                Url($"_list/indices/{index:index}"),
                ctx,
                null,
                RequestParams(requestParameters)
            );

        /// <summary>GET on /_list/shards <para>https://opensearch.org/docs/latest/api-reference/list/list-shards/</para></summary>
        /// <param name="requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
        /// <remarks>Supported by OpenSearch servers of version 2.18.0 or greater.</remarks>
        public TResponse Shards<TResponse>(ShardsRequestParameters requestParameters = null)
            where TResponse : class, IOpenSearchResponse, new() =>
            DoRequest<TResponse>(GET, "_list/shards", null, RequestParams(requestParameters));

        /// <summary>GET on /_list/shards <para>https://opensearch.org/docs/latest/api-reference/list/list-shards/</para></summary>
        /// <param name="requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
        /// <remarks>Supported by OpenSearch servers of version 2.18.0 or greater.</remarks>
        [MapsApi("list.shards", "")]
        public Task<TResponse> ShardsAsync<TResponse>(
            ShardsRequestParameters requestParameters = null,
            CancellationToken ctx = default
        )
            where TResponse : class, IOpenSearchResponse, new() =>
            DoRequestAsync<TResponse>(
                GET,
                "_list/shards",
                ctx,
                null,
                RequestParams(requestParameters)
            );

        /// <summary>GET on /_list/shards/{index} <para>https://opensearch.org/docs/latest/api-reference/list/list-shards/</para></summary>
        /// <param name="index">A comma-separated list of data streams, indexes, and aliases used to limit the request. Supports wildcards (`*`). To target all data streams and indexes, omit this parameter or use `*` or `_all`.</param>
        /// <param name="requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
        /// <remarks>Supported by OpenSearch servers of version 2.18.0 or greater.</remarks>
        public TResponse Shards<TResponse>(
            string index,
            ShardsRequestParameters requestParameters = null
        )
            where TResponse : class, IOpenSearchResponse, new() =>
            DoRequest<TResponse>(
                GET,
                Url($"_list/shards/{index:index}"),
                null,
                RequestParams(requestParameters)
            );

        /// <summary>GET on /_list/shards/{index} <para>https://opensearch.org/docs/latest/api-reference/list/list-shards/</para></summary>
        /// <param name="index">A comma-separated list of data streams, indexes, and aliases used to limit the request. Supports wildcards (`*`). To target all data streams and indexes, omit this parameter or use `*` or `_all`.</param>
        /// <param name="requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
        /// <remarks>Supported by OpenSearch servers of version 2.18.0 or greater.</remarks>
        [MapsApi("list.shards", "index")]
        public Task<TResponse> ShardsAsync<TResponse>(
            string index,
            ShardsRequestParameters requestParameters = null,
            CancellationToken ctx = default
        )
            where TResponse : class, IOpenSearchResponse, new() =>
            DoRequestAsync<TResponse>(
                GET,
                Url($"_list/shards/{index:index}"),
                ctx,
                null,
                RequestParams(requestParameters)
            );
    }
}