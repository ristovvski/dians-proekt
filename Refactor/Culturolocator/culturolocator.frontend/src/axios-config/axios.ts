import axios from "axios";

const instance = axios.create({
    baseURL: 'https://localhost:7198',
    headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': localStorage.getItem("access"),
        'Access-Control-Allow-Credentials': true
    }
})

instance.interceptors.request.use(
    config => {
        const token = localStorage.getItem("access");
        if (token) config.headers.Authorization = `Bearer ${token}`;
        return config;
    },
    error => {
        if (error.response.status === 403) {
            localStorage.removeItem("access");
            window.location.href = '/login';
        }
        return Promise.reject(error);
    }
);

 //Add a response interceptor
//instance.interceptors.response.use(
//    (response) => response,
//    async (error) => {
//        const originalRequest = error.config;
//        console.log(originalRequest)
//        // If the error status is 401 and there is no originalRequest._retry flag,
//        // it means the token has expired and we need to refresh it
//        if (error.response.status === 401 && !originalRequest._retry) {
//            originalRequest._retry = true;

//            try {
//                const refreshToken = localStorage.getItem('refresh');
//                const response = await instance.post("/refresh", {
//                    "refreshToken": refreshToken
//                });
//                const { token } = response.data;

//                localStorage.setItem("access", token.accessToken)
//                localStorage.setItem("refresh", token.refreshToken);

//                // Retry the original request with the new token
//                originalRequest.headers.Authorization = `Bearer ${token.accessToken}`;
//                return instance(originalRequest);
//            } catch (error) {
//                // Handle refresh token error or redirect to login
//                const navigate = useNavigate();
//                navigate("/login");
//            }
//        }
//    }
//)


// Add a response interceptor
instance.interceptors.response.use(
    (response) => {
        // Do something with the response data
        return response;
    },
    async (error) => {
        // If response is 401 (Unauthorized), try refreshing the token
        if (error.response && error.response.status === 401) {
            try {
                const newToken = await refreshToken();
                // Retry the original request with the new token
                const config = error.config;
                config.headers.Authorization = `Bearer ${newToken}`;
                return instance.request(config);
            } catch (refreshError) {
                // Handle error refreshing token
                return Promise.reject(refreshError);
            }
        }

        // For other errors, reject the promise
        return Promise.reject(error);
    }
);

// Function to refresh token
async function refreshToken() {
    try {
        const refreshToken = localStorage.getItem('refresh');
        if (refreshToken != null) {
            const response = await instance.post('/refresh', {
                "refreshToken": refreshToken
            });
            localStorage.setItem("access", response.data.aceessToken)
            localStorage.setItem("refresh", response.data.refreshToken)
            return response.data.aceessToken; // Assuming the new token is returned in the response
        }
        throw new Error('Refresh token not found'); // Throw error if refreshToken is null
    } catch (error) {
        throw error; // Handle error refreshing token
    }
}


export default instance;