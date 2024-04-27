import axios from '../Axios/Axios';


const AppService = {
    login: (loginDTO) => {
        return axios.post("/login", {
            "email": loginDTO.email,
            "password": loginDTO.password,
            "twoFactorCode": "string",
            "twoFactorRecoveryCode": "string"
        });
    },
    register: (registerDTO) => {
        return axios.post("/register", {
            "email": registerDTO.email,
            "password": registerDTO.password
        });
    },
    getWeather: () => {
        var token = localStorage.getItem("JWT");
        return axios.get('/weatherforecast/user');
    }
}

export default AppService;