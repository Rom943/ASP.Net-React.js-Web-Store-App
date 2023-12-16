<<<<<<< HEAD
import axios from "axios";

const api = axios.create({
    baseURL: 'https://localhost:7237/api/'
});

api.interceptors.request.use((o) => {
    const token = localStorage.getItem('token');
    o.headers.Authorization = token ? `Bearer ${token}` : "";
    return o;
})

=======
import axios from "axios";

const api = axios.create({
    baseURL: 'https://localhost:7237/api/'
});

api.interceptors.request.use((o) => {
    const token = localStorage.getItem('token');
    o.headers.Authorization = token ? `Bearer ${token}` : "";
    return o;
})

>>>>>>> 47570808c5eba5690650b57895085a31f452edbc
export default api;