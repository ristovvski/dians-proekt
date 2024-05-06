import { useNavigate } from "react-router-dom";
import WeathersList from "../../Component/weathers-list/weather-list.component"
import { WeathersListContainer } from "../../Component/weathers-list/weathers-list.styles"
import Weather from "../../Models/Weather/Weather";
import AppService from "../../Repository/CulturolocatorService";
import { useEffect, useState } from 'react'

const Weathers = () => {

    const [weathers, setWeathers] = useState<Weather[]>([]);
    const service = AppService;
    const navigate = useNavigate();

    useEffect(() => {
        getData();
        document.title = "Library App - Authors"
    }, []);

    const getData = async () => {
        try {
            const response = await service.getWeather();
            setWeathers(response.data);
        } catch (error) {
            console.error('Error fetching weather data:', error);
            if (localStorage.getItem("access") == null) {
                navigate("/login")
            }
        }
    };

    return (
        <WeathersListContainer>
            <WeathersList weathers={weathers}/>
        </WeathersListContainer>
    )
}

export default Weathers