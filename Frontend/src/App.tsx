import "./App.css";
import WeatherDisplay from "./components/WeatherDisplay";
import { Box } from "@mui/material";

function App() {
  return (
    <Box
      sx={{
        minHeight: "100vh",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
      }}
    >
      <WeatherDisplay />
    </Box>
  );
}

export default App;
