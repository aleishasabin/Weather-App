import { useEffect, useRef, useState } from "react";
import type { City, Weather } from "../types";
import {
  getRecentCities,
  getWeatherByCity,
  searchCities,
} from "../api/WeatherApi";
import { normalize } from "../utils";

export const useCityWeather = (initialValue = "") => {
  const [selectedCity, setSelectedCity] = useState<City | null>(null);
  const [weather, setWeather] = useState<Weather | null>(null);
  const [inputValue, setInputValue] = useState(initialValue);
  const [cities, setCities] = useState<City[]>([]);
  const [filteredCities, setFilteredCities] = useState<City[]>([]);

  const lastFetchedPrefix = useRef("");
  const lastSelectedCityRef = useRef<City | null>(null);

  const deduplicate = (citiesArray: City[]) =>
    Array.from(
      new Map(
        citiesArray.map((city) => [`${city.nameAscii}-${city.country}`, city])
      ).values()
    );

  const filterCities = (cityList: City[]) =>
    cityList.filter((city) =>
      `${city.nameAscii.toLowerCase()}, ${city.country.toLowerCase()}`.startsWith(
        normalize(inputValue.toLowerCase())
      )
    );

  const fetchWeather = async () => {
    if (selectedCity) {
      if (selectedCity.id !== lastSelectedCityRef.current?.id) {
        const results = await getWeatherByCity(selectedCity.id);
        setWeather(results);
      }
      lastSelectedCityRef.current = selectedCity;
    }
  };

  const fetchCities = async () => {
    if (!inputValue) {
      const results = await getRecentCities(5);
      const uniqueCities = deduplicate(results);
      setCities(uniqueCities);
      setFilteredCities(filterCities(uniqueCities));
      lastFetchedPrefix.current = "";
      return;
    }

    const prefix = inputValue.slice(0, 3).toLowerCase();

    if (inputValue.length >= 3 && prefix !== lastFetchedPrefix.current) {
      const results = await searchCities(prefix);
      const uniqueCities = deduplicate(results);
      setCities(uniqueCities);
      setFilteredCities(filterCities(uniqueCities));
      lastFetchedPrefix.current = prefix;
      return;
    }

    setFilteredCities(filterCities(cities));
  };

  useEffect(() => {
    fetchCities();
  }, [inputValue]);

  useEffect(() => {
    fetchWeather();
  }, [selectedCity]);

  return {
    selectedCity,
    setSelectedCity,
    weather,
    inputValue,
    setInputValue,
    filteredCities,
  };
};
