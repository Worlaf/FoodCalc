import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";

Vue.use(VueRouter);

export const routePaths = {
  home: "/",
  about: "/about",
  nutrients: "/nutrients",
  food: "/food"
};

const routes = [
  {
    path: routePaths.home,
    name: "Home",
    component: Home
  },
  {
    path: routePaths.about,
    name: "About",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue")
  },
  {
    path: routePaths.nutrients,
    name: "Nutrients",
    component: () => import("../views/NutrientList")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
