import React, { ReactElement } from "react"
import { useGraphQL, GraphQLOperation } from "graphql-react"

interface IDataQueryProps<T, V> {
    operation: GraphQLOperation<V>
    render: (data: T) => ReactElement
}

export default function DataQuery<T, V = {}>(props: IDataQueryProps<T, V>): ReactElement {
    const { loading, cacheValue } = useGraphQL<T, V>({
        fetchOptionsOverride: options => {
            options.url = process.env.REACT_APP_GRAPHQL_URL || "https://localhost:5001/graphql"
        },
        operation: props.operation,
        loadOnMount: true,
        loadOnReload: true,
        loadOnReset: true,
        reloadOnLoad: true
    })

    if (loading)
        return <div>Loading...</div>
    else if (cacheValue) {
        if (cacheValue.fetchError)
            return <div>Fetch error: {cacheValue.fetchError}</div>
        if (cacheValue.graphQLErrors)
            return <div>GraphQL errors: {cacheValue.graphQLErrors}</div>
        if (cacheValue.httpError)
            return <div>HTTP error: {cacheValue.httpError}</div>
        if (cacheValue.parseError)
            return <div>Parse error: {cacheValue.parseError}</div>
        if (cacheValue.data)
            return props.render(cacheValue.data);
    }

    return <div>Unexpected state</div>
}