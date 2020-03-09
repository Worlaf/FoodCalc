export interface IRoute {
    path: string,
    title: string
}

const routes = {
    home: <IRoute>{
        path: "/",
        title: "home"
    },
    nutrientList: <IRoute>{
        path: "/nutrients",
        title: "nutrients"
    },
    foodList: <IRoute>{
        path: "/food",
        title: "food"
    }
}

export default routes;