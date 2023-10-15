import { Navigate } from "react-router-dom";
import { loggedIn } from "./Auth";

const RequireAuth = ({children}) => {

    if(!loggedIn()) {
        return <Navigate to='/auth'/>
    }

    return children;
}

export default RequireAuth;