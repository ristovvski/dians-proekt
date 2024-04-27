import axios from "axios";

const instance = axios.create({
    baseURL: 'http://localhost:5187',
    headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': localStorage.getItem("JWT"),
        'Content-Type': 'application/json'
    }
})

instance.interceptors.request.use(
    config => {
        const token = localStorage.getItem("JWT");
        if (token) config.headers.Authorization = `Bearer ${token}`;
        return config;
    },
    error => {
        if (error.response.status === 403) {
            localStorage.removeItem("JWT");
            window.location.href = '/login';
        }
        return Promise.reject(error);
    }
);



export default instance;