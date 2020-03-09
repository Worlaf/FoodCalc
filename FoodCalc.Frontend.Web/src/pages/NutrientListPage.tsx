import React from "react"
import { INutrient } from "../api/Nutrient"
import RootLayout from "../layouts/RootLayout"
import DataQuery from "../components/DataQuery"

const NutrientListPage: React.FC = props => {
    return <RootLayout>
        <DataQuery<{ nutrients: INutrient[] }, {}>
            operation={{
                query: `
                {
                    nutrients {
                        id
                        name
                    }
                }`,
                variables: {}
            }}
            render={data => <div>{data.nutrients.map(n => <div>{n.name}</div>)}</div>}
        />
    </RootLayout>
}

export default NutrientListPage;