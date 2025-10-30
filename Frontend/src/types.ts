export interface City {
    id: number;
    name: string;
    nameAscii: string;
    country: string;
}

export interface Weather {
    cityName: string;
    country: string;
    summary: string;
    description: string;
    temperature: number;
    humidity: number;
    wind: WindMetrics;
}

export interface WindMetrics {
    speed: number;
    direction: number;
}