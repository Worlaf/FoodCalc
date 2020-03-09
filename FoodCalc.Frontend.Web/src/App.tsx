import React from 'react';
import './App.css';
import NutrientListPage from './pages/NutrientListPage';
import FoodListPage from './pages/FoodListPage';
import HomePage from './pages/HomePage';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import routes from './routes';
import { GraphQL, GraphQLContext } from 'graphql-react';

const graphql = new GraphQL();

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <GraphQLContext.Provider value={graphql}>
          <Switch>
            <Route exact path={routes.home.path} children={HomePage} />
            <Route exact path={routes.nutrientList.path}>
              <NutrientListPage />
            </Route>
            <Route exact path={routes.foodList.path} children={FoodListPage} />
          </Switch>
        </GraphQLContext.Provider>
      </BrowserRouter>
    </div>
  );
}

export default App;
