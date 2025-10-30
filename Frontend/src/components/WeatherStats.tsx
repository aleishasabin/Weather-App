import { Box, Grid, Typography } from "@mui/material";
import type { Weather } from "../types";
import { LuWaves, LuWind } from "react-icons/lu";

export const WeatherStats = ({ weather }: { weather: Weather }) => (
  <Box sx={{ pt: "24px" }}>
    <Grid container sx={{ justifyContent: "space-between" }}>
      <Grid container sx={{ width: "150px" }}>
        <Grid sx={{ pr: "10px" }}>
          <LuWind size={30} />
        </Grid>
        <Grid>
          <Typography
            variant="body1"
            color="text.secondary"
            fontWeight={400}
            sx={{ mb: "-5px" }}
          >
            Wind
          </Typography>
          <Typography variant="body1" fontWeight={500}>
            {weather.wind.speed} km/h
          </Typography>
        </Grid>
      </Grid>

      <Grid container sx={{ width: "150px" }}>
        <Grid sx={{ pr: "10px" }}>
          <LuWaves size={30} />
        </Grid>
        <Grid>
          <Typography
            variant="body1"
            color="text.secondary"
            fontWeight={400}
            sx={{ mb: "-5px" }}
          >
            Humidity
          </Typography>
          <Typography variant="body1" fontWeight={500}>
            {weather.humidity}%
          </Typography>
        </Grid>
      </Grid>
    </Grid>
  </Box>
);
