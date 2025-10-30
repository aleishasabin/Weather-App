import { Box, Grid, Typography } from "@mui/material";
import type { Weather } from "../types";
import { WeatherIcon } from "./WeatherIcon";

export const WeatherSummary = ({ weather }: { weather: Weather }) => (
  <Box
    sx={{
      backgroundColor: "#D1E2F5",
      mx: "-70px",
      px: "70px",
      mt: "24px",
      py: "16px",
    }}
  >
    <Grid container sx={{ justifyContent: "left" }}>
      <Grid>
        <WeatherIcon summary={weather.summary} size={67} color={"#373C51"} />
      </Grid>
      <Grid sx={{ ml: "24px" }}>
        <Typography
          variant="h3"
          fontWeight={600}
          sx={{ mb: "-8px", color: "#373C51" }}
        >
          {Math.round(weather.temperature)}°C
        </Typography>
        <Typography variant="body1" fontWeight={400} sx={{ color: "#373C51" }}>
          {weather.summary}
        </Typography>
      </Grid>
    </Grid>
  </Box>
);
