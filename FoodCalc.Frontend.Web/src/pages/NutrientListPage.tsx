import React from "react"
import { INutrient } from "../api/Nutrient"
import RootLayout from "../layouts/RootLayout"

const NutrientListPage: React.FC = props => {
    const nurtients: INutrient[] = [
        {
            id: 1,
            name: "Nutrient 1"
        },
        {
            id: 2,
            name: "Nutrient 2"
        }
    ]

    return <RootLayout>
        <div>
            {nurtients.map(n => {
                return <div>{n.name}</div>
            })}
        </div>
    </RootLayout>
}

export default NutrientListPage;