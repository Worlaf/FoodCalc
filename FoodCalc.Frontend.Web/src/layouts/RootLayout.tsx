import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import routes from '../routes';
import Container from '@material-ui/core/Container';
import Paper from '@material-ui/core/Paper';
import { makeStyles } from '@material-ui/core/styles';
import NavTabs from '../components/NavTabs';
import stomach from '../resourses/stomach.svg';
import SvgIcon from '@material-ui/core/SvgIcon';
import Box from '@material-ui/core/Box';

const useStyles = makeStyles({
    header: {
        display: "grid",
        gridTemplateColumns: "min-content auto"
    },
    logo: {
        display: "grid",
        gridTemplateColumns: "min-content auto"
    }
})

const RootLayout: React.FC = props => {
    const classes = useStyles();

    return <div>
        <AppBar position="static">
            <Box className={classes.header}>
                <Box className={classes.logo}>
                    <img height="100%" src={stomach} />
                    FoodCalc
                </Box>
                <NavTabs routes={[routes.nutrientList, routes.foodList]} />
            </Box>
        </AppBar>
        <Container maxWidth="md">
            {props.children}
        </Container>
    </div>
}

export default RootLayout;