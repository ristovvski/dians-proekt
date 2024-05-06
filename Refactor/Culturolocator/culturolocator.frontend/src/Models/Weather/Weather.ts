export class Weather {
    public temperatureC?: number;
    public temperatureF?: number;
    public date?: Date;
    public summary?: string;

    constructor(temperatureC: number, temperatureF: number, date: Date, summary: string) {
        this.temperatureC = temperatureC;
        this.temperatureF = temperatureF;
        this.date = date;
        this.summary = summary;
    }
}

export default Weather;