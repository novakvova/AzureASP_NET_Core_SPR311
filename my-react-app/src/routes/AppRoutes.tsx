import {Route, Routes} from "react-router-dom";
import UsersList from "../pages/users/UsersList.tsx";
import Login from "../pages/auth/Login.tsx";
import * as React from "react";
import MainLayout from "../layouts/MainLayout.tsx";
import NotFound from "../pages/NotFound.tsx";
import UserView from "../pages/user/UserView.tsx";
import CateogriesList from "../pages/categories/list";


const AppRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path="/" element={<MainLayout/>}>
                <Route index element={<UsersList />} />
                <Route path={"login"} element={<Login />} />
                <Route path={"user"}>
                    <Route path={":id"} element={<UserView />} />
                </Route>

                <Route path={"categories"}>
                    <Route index element={<CateogriesList />} />
                </Route>

            </Route>

            <Route path="*" element={<NotFound />} />

        </Routes>
    )
}
export default AppRoutes;