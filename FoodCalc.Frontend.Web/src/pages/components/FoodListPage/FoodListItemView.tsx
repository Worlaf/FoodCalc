import React from "react";
import { INutrient } from "../../../api/Nutrient";
import { IFood } from "../../../api/Food";
import { makeStyles, createStyles, Theme, ExpansionPanel, ExpansionPanelSummary, ExpansionPanelDetails, Typography } from "@material-ui/core";
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { stringify } from "../../../api/ValueRange";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        heading: {
            fontSize: theme.typography.pxToRem(15),
            flexBasis: '33.33%',
            flexShrink: 0,
        },
        secondaryHeading: {
            fontSize: theme.typography.pxToRem(12),
            color: theme.palette.text.secondary,
        },
        column: {
            flexBasis: "33.33%"
        }
    }),
);

interface IFoodListItemViewProps {
    food: IFood,
    nutrients: { [id: number]: INutrient }
    expanded: boolean,
    toggle: () => void
}

const FoodListItemView: React.FC<IFoodListItemViewProps> = props => {
    const classes = useStyles();

    return <ExpansionPanel expanded={props.expanded} onChange={props.toggle}>
        <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />}>
            <div className={classes.column}>
                <Typography className={classes.heading}>{props.food.name}</Typography>
            </div>
            <div className={classes.column}>
                {
                    props.food.nutrientsPer100Gram
                        .filter(n => props.nutrients[n.nutrientId].isRequired)
                        .map(n => <Typography className={classes.secondaryHeading}>{props.nutrients[n.nutrientId].name}: {stringify(n.countInGramsPer100GramsOfFood, "г")}.</Typography>)
                }
            </div>
        </ExpansionPanelSummary>
        <ExpansionPanelDetails>
            <Typography>
                {props.food.descriptionMarkdown}
            </Typography>
            <div>
                {props.food.nutrientsPer100Gram
                    .filter(n => !props.nutrients[n.nutrientId].isRequired)
                    .map(n => <div>{props.nutrients[n.nutrientId].name}: {stringify(n.countInGramsPer100GramsOfFood, "г")}.</div>)}
            </div>
        </ExpansionPanelDetails>
    </ExpansionPanel>
}

export default FoodListItemView;