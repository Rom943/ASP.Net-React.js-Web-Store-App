import axios from "axios";

const api = axios.create({
    baseURL: 'https://localhost:7237/api/'
});

api.interceptors.request.use((o) => {
    const token = localStorage.getItem('token');
    o.headers.Authorization = token ? `Bearer ${token}` : "";
    return o;
})

export default api;