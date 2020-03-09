export interface IRoute {
    path: string,
    title: string
}

const routes = {
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