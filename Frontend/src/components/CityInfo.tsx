import { Box, Grid, Typography } from "@mui/material";
import { PiMapPinFill } from "react-icons/pi";
import type { Weather } from "../types";

export const CityInfo = ({ weather }: { weather: Weather }) => (
  <Box sx={{ pt: "24px" }}>
    <Grid container>
      <Grid sx={{ pt: "10px", pr: "8px" }}>
        <PiMapPinFill size={24} color="text.secondary" />
      </Grid>
      <Grid>
        <Typography variant="h6" fontWeight={500} sx={{ mb: "-6px" }}>
          {weather.cityName}
        </Typography>
        <Typography variant="body2" color="text.secondary" fontWeight={400}>
          {weather.country}
        </Typography>
      </Grid>
    </Grid>
  </Box>
);
