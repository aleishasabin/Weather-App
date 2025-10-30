import type { City, Weather } from "../types";
import { normalize } from "../utils";

const API_BASE_URL = "https://localhost:5001/api";

export async function searchCities(prefix: string): Promise<City[]> {
  const response = await fetch(
    `${API_BASE_URL}/city/search?prefix=${normalize(prefix)}`
  );

  if (!response.ok) {
    if (response.status === 404) return [];

    throw new Error(`Failed to search cities. Status: ${response.status}`);
  }

  return response.json();
}

export async function getRecentCities(count: number): Promise<City[]> {
  const response = await fetch(`${API_BASE_URL}/city/recent?count=${count}`);

  if (!response.ok) {
    if (response.status === 404) return [];

    throw new Error(
      `Failed to fetch recent cities. Status: ${response.status}`
    );
  }

  return response.json();
}

export async function getWeatherByCity(cityId: number): Promise<Weather> {
  const response = await fetch(`${API_BASE_URL}/weather/${cityId}`);

  if (!response.ok) {
    throw new Error(`Failed to fetch weather. Status: ${response.status}`);
  }

  return response.json();
}
