import { Autocomplete, Box, Paper, TextField, Typography } from "@mui/material";
import { useRef } from "react";
import type { City } from "../types";
import { useCityWeather } from "../hooks/useCityWeather";
import { CityInfo } from "./CityInfo";
import { WeatherStats } from "./WeatherStats";
import { WeatherSummary } from "./WeatherSummary";

const WeatherDisplay = () => {
  const inputRef = useRef<HTMLInputElement | null>(null);

  const {
    selectedCity,
    setSelectedCity,
    weather,
    inputValue,
    setInputValue,
    filteredCities,
  } = useCityWeather();

  return (
    <Box
      sx={{
        position: "relative",
        display: "flex",
        justifyContent: "center",
        mt: 8,
      }}
    >
      <Box
        component="img"
        src="src\assets\aleisha_umbrella.png"
        sx={{
          position: "absolute",
          top: "-90px",
          left: "75%",
          width: 100,
          height: 100,
          animation: "float 2s ease-in-out infinite alternate",
          "@keyframes float": {
            "0%": { transform: "translateX(-50%) translateY(0px)" },
            "100%": { transform: "translateX(-50%) translateY(-5px)" },
          },
        }}
      />
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          justifyContent: "center",
          alignItems: "left",
          backgroundColor: "white",
          padding: "70px",
          borderRadius: "32px",
          width: "550px",
          boxShadow: 2,
          zIndex: 2,
        }}
      >
        <Typography variant="h4" fontWeight={400}>
          <b>Weather</b> App
        </Typography>

        <Typography
          variant="subtitle1"
          color="text.primary"
          fontWeight={400}
          sx={{ pb: "24px" }}
        >
          Created by <b>Aleisha Sabin</b>
        </Typography>

        <Typography variant="body1" fontWeight={400} sx={{ pb: "24px" }}>
          A simple, minimalist weather companion. Get current weather
          conditions, including temperature, wind and humidity, all in one
          place.
        </Typography>

        <Autocomplete
          options={filteredCities}
          groupBy={() => (inputValue.length === 0 ? "Recent Searches" : "")}
          inputValue={inputValue}
          onInputChange={(_event, newInputValue) =>
            setInputValue(newInputValue)
          }
          onChange={(event, value: City | null) => {
            const target = event.target as HTMLElement;
            if (target.closest('button[aria-label="Clear"]')) {
              setSelectedCity(value);
              setTimeout(() => inputRef.current?.blur(), 0);
              return;
            }
            if (value) setSelectedCity(value);
          }}
          getOptionLabel={(option) => `${option.name}, ${option.country}`}
          noOptionsText={null}
          slots={{
            paper: (props) => {
              if (!filteredCities || filteredCities.length === 0) return null;
              return <Paper {...props} />;
            },
          }}
          slotProps={{
            listbox: {
              sx: {
                maxHeight: 300,
                overflowY: "auto",
              },
            },
          }}
          renderInput={(params) => (
            <TextField
              {...params}
              label="Search City"
              variant="filled"
              onBlur={() => inputValue === "" && setSelectedCity(null)}
              inputRef={inputRef}
              sx={{ backgroundColor: "#f3f4f6" }}
            />
          )}
        />

        {selectedCity && weather && (
          <>
            <CityInfo weather={weather} />
            <WeatherSummary weather={weather} />
            <WeatherStats weather={weather} />
          </>
        )}
      </Box>
    </Box>
  );
};

export default WeatherDisplay;
