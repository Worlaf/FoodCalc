import React from "react"
import { INutrient } from "../api/Nutrient"
import RootLayout from "../layouts/RootLayout"
import DataQuery from "../components/DataQuery"
import { Typography, Box, makeStyles } from "@material-ui/core"

const useNutrientViewStyles = makeStyles({
    childNutrients: {
        paddingLeft: "1em"
    }

})

interface INutrientViewProps {
    nutrient: INutrient

}

const NutrientView: React.FC<INutrientViewProps> = props => {
    const classes = useNutrientViewStyles();

    return <Box>
        <Typography>
            {props.nutrient.name}
        </Typography>
        <Box className={classes.childNutrients}>
            {props.children}
        </Box>
    </Box>
}

const NutrientListPage: React.FC = props => {
    const RenderNutrients = (nutrients: INutrient[], parentId: number | null) => {
        return nutrients.filter(n => n.parentId === parentId).map(n => <NutrientView nutrient={n}>
            {RenderNutrients(nutrients, n.id)}
        </NutrientView>)
    }

    return <RootLayout>
        <DataQuery<{ nutrients: INutrient[] }, {}>
            operation={{
                query: `
                {
                    nutrients {
                        id
                        name
                        parentId
                    }
                }`,
                variables: {}
            }}
            render={data => <div>
                {RenderNutrients(data.nutrients, null)}
            </div>}
        />
    </RootLayout>
}

export default NutrientListPage;