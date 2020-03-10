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

    return <Box>{props.food.map((f, i) => <FoodListItemView food={f} nutrients={props.nutrients} expanded={selectedIndex === i} toggle={() => setSelectedIndex(i)} key={i} />)}</Box>
}

export default FoodListView;

