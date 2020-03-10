import React from "react";
import { INutrient } from "../../../api/Nutrient";
import { IFood } from "../../../api/Food";
import { makeStyles, createStyles, Theme, ExpansionPanel, ExpansionPanelSummary, ExpansionPanelDetails, Typography } from "@material-ui/core";
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        heading: {
            fontSize: theme.typography.pxToRem(15),
            flexBasis: '33.33%',
            flexShrink: 0,
        },
        secondaryHeading: {
            fontSize: theme.typography.pxToRem(15),
            color: theme.palette.text.secondary,
        },
    }),
);

interface IFoodListItemViewProps {
    food: IFood,
    nutrients: INutrient[],
    expanded: boolean,
    toggle: () => void
}

const FoodListItemView: React.FC<IFoodListItemViewProps> = props => {
    const classes = useStyles();

    return <ExpansionPanel expanded={props.expanded} onChange={props.toggle}>
        <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />}>
            <Typography className={classes.heading}>{props.food.name}</Typography>
        </ExpansionPanelSummary>
        <ExpansionPanelDetails>
            <Typography>
                {props.food.descriptionMarkdown}
            </Typography>
        </ExpansionPanelDetails>
    </ExpansionPanel>
}

export default FoodListItemView;