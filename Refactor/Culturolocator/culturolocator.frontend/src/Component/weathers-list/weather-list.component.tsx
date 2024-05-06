import { Weather } from '../../Models/Weather/Weather';
import { IWeathersListProps } from './weather-list.props';
import { WeathersListContainer } from './weathers-list.styles';
import { TableRow, TableColumn, Table, TableBody, TableHead, TableHeader } from '../common/common.styles'


const WeathersList = (props: IWeathersListProps) => {

    const Weather = (weather: Weather) => {
        return (
            <TableRow >
                <TableColumn>
                    {weather.date}
                </TableColumn>
                <TableColumn>
                    {weather.temperatureC}
                </TableColumn>
                <TableColumn>
                    {weather.temperatureF}
                </TableColumn>
                <TableColumn>
                    {weather.summary}
                </TableColumn>
            </TableRow>
        )
    }


    return (
        <WeathersListContainer>
            {props.weathers.length > 0 && <Table>
                <TableHead>
                    <TableRow>
                        <TableHeader>#</TableHeader>
                        <TableHeader>Name</TableHeader>
                        <TableHeader>Country</TableHeader>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {props.weathers.map((t) => Weather(t))}
                </TableBody>
            </Table>}
        </WeathersListContainer>
    );
}

export default WeathersList;