import React from "react";
import RootLayout from "../layouts/RootLayout";
import DataQuery from "../components/DataQuery";
import { INutrient } from "../api/Nutrient";
import { IFood } from "../api/Food";
import { Typography } from "@material-ui/core";
import FoodListView from "./components/FoodListPage/FoodListView";

const FoodListPage: React.FC = () => {
    return <RootLayout>
        <Typography variant="h1">Food</Typography>
        <DataQuery<{ nutrients: INutrient[], food: IFood[] }>
            operation={{
                query: `{
                    nutrients{id, name, parentId, isRequired}
                    food{
                        id
                        name
                        nutrientsPer100Gram {
                            nutrientId
                            countInGramsPer100GramsOfFood {
                            min
                            max
                            }
                        }
                    }
                }`,
                variables: {}
            }}
            render={data => {
                return <FoodListView food={data.food} nutrients={data.nutrients} />
            }} />
    </RootLayout>
}

export default FoodListPage;