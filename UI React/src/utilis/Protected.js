<<<<<<< HEAD
import { Navigate } from 'react-router-dom'
import jwtDecode from 'jwt-decode';

function Protected({ children }) {

    const haveToken = Boolean(localStorage.getItem("token"));

    if (haveToken) {
        const decodedToken = jwtDecode(localStorage.getItem("token"))
        const currentTime = new Date();
        const expTime = new Date(decodedToken.exp*1000);
        if(currentTime>expTime){
            localStorage.setItem("token","")
            localStorage.setItem("userType","")
            localStorage.setItem("userId","")
            return <Navigate to="/Login"/>
        }
        return children;
    }
    console.log("not isAuthenticated")
    return <Navigate to = "/Login" />;
}
=======
import { Navigate } from 'react-router-dom'
import jwtDecode from 'jwt-decode';

function Protected({ children }) {

    const haveToken = Boolean(localStorage.getItem("token"));

    if (haveToken) {
        const decodedToken = jwtDecode(localStorage.getItem("token"))
        const currentTime = new Date();
        const expTime = new Date(decodedToken.exp*1000);
        if(currentTime>expTime){
            localStorage.setItem("token","")
            localStorage.setItem("userType","")
            localStorage.setItem("userId","")
            return <Navigate to="/Login"/>
        }
        return children;
    }
    console.log("not isAuthenticated")
    return <Navigate to = "/Login" />;
}
>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
export default Protected