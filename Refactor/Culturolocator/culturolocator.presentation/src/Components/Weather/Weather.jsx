import React from 'react';
import { useNavigate } from 'react-router-dom';
import AppService from '../../Repository/AppRepository';

function Weather() {

    const [weathers, setWeather] = React.useState([]);

    const service = AppService;

    React.useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        const response = await AppService.getWeather();
        const responseData = JSON.parse(response.request.response);
        setWeather(responseData);
        console.log(responseData);
    };


  return (
      <div>
            <h2>Weather Forecast</h2>
            {weathers.map((weather, index) => (
                <div key={index}>
                    <p>Date: {weather.date}</p>
                    <p>Temperature: {weather.temperatureC} celsius / {weather.temperatureF} farenheit</p>
                    <p>Summary: {weather.summary}</p>
                </div>
            ))}
        </div>
  );
}

export default Weather;