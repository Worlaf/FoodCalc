import React, { useState } from "react";
import { INutrient } from "../../../api/Nutrient";
import { IFood } from "../../../api/Food";
import FoodListItemView from "./FoodListItemView";
import { Box } from "@material-ui/core";

interface IFoodListViewProps {
    nutrients: INutrient[],
    food: IFood[]
}

const FoodListView: React.FC<IFoodListViewProps> = props => {
    const [selectedIndex, setSelectedIndex] = useState<number>();
    const nutrients = props.nutrients.reduce((map, nutrient) => {
        map[nutrient.id] = nutrient;
        return map;
    }, {} as { [index: number]: INutrient });

    return <Box>{props.food.map((f, i) => <FoodListItemView food={f} nutrients={nutrients} expanded={selectedIndex === i} toggle={() => setSelectedIndex(i)} key={i} />)}</Box>
}

export default FoodListView;

