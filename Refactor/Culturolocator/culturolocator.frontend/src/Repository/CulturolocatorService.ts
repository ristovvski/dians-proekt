import axios from '../axios-config/axios';
import LoginDTO from '../Models/DTO/LoginDTO'
import RegisterDTO from '../Models/DTO/RegisterDTO';


const AppService = {
    login: (loginDTO: LoginDTO) => {
        return axios.post("/login", {
            "email": loginDTO.email,
            "password": loginDTO.password,
            "twoFactorCode": "string",
            "twoFactorRecoveryCode": "string"
        });
    },
    register: (registerDTO: RegisterDTO) => {
        return axios.post("/register", {
            "email": registerDTO.email,
            "password": registerDTO.password
        });
    },
    getWeather: () => {
        return axios.get('/weatherforecast/user');
    }
}

export default AppService;