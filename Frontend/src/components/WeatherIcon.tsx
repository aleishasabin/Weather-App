import type { JSX } from "react";
import React from "react";
import {
  BsCloudFog2,
  BsCloudRainFill,
  BsCloudsFill,
  BsFillCloudLightningRainFill,
  BsFillSunFill,
  BsSnow2,
} from "react-icons/bs";

const weatherIcons: Record<string, JSX.Element> = {
  thunderstorm: <BsFillCloudLightningRainFill />,
  rain: <BsCloudRainFill />,
  drizzle: <BsCloudRainFill />,
  snow: <BsSnow2 />,
  mist: <BsCloudFog2 />,
  smoke: <BsCloudFog2 />,
  haze: <BsCloudFog2 />,
  dust: <BsCloudFog2 />,
  fog: <BsCloudFog2 />,
  sand: <BsCloudFog2 />,
  ash: <BsCloudFog2 />,
  squall: <BsCloudFog2 />,
  tornado: <BsCloudFog2 />,
  clear: <BsFillSunFill />,
  clouds: <BsCloudsFill />,
};

export const WeatherIcon = ({
  summary,
  size,
  color,
}: {
  summary: string;
  size: number;
  color?: string;
}) => {
  return React.cloneElement(
    weatherIcons[summary.toLowerCase()] || weatherIcons["clear"],
    { size, color }
  );
};
