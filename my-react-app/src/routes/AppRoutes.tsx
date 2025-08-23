import {Route, Routes} from "react-router-dom";
import UsersList from "../pages/users/UsersList.tsx";
import Login from "../pages/auth/Login.tsx";
import * as React from "react";


const AppRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path="/">
                <Route index element={<UsersList />} />
                <Route path={"login"} element={<Login />} />
            </Route>
        </Routes>
    )
}
export default AppRoutes;