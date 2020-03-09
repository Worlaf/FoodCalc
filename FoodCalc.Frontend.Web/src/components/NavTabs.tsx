import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import { Link, RouteComponentProps, withRouter } from 'react-router-dom';
import routes, { IRoute } from '../routes';
import Container from '@material-ui/core/Container';
import Paper from '@material-ui/core/Paper';
import { makeStyles } from '@material-ui/core/styles';

const NavTab: React.FC<{ route: IRoute }> = props => {
    return <Tab
        component={Link}
        to={props.route.path}
        label={props.route.title}
    />
}

const NavTabs: React.FC<{ routes: IRoute[] } & RouteComponentProps> = props => {
    const value = props.routes.findIndex(r => r.path == props.match.path);

    return <Tabs value={value}>
        {props.routes.map((r, i) => <NavTab route={r} key={i} />)}
    </Tabs>
}

export default withRouter(NavTabs);