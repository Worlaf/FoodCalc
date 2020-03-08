import React from 'react';
import './App.css';
import NutrientListPage from './pages/NutrientListPage';
import FoodListPage from './pages/FoodListPage';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import routes from './routes';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Switch>
          <Route exact path={routes.nutrientList.path} children={NutrientListPage} />
          <Route exact path={routes.foodList.path} children={FoodListPage} />
        </Switch>
      </BrowserRouter>
    </div>
  );
}

export default App;
