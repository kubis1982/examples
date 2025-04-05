import { emptySplitApi as api } from "../emptyApi";
export const addTagTypes = ["Weather1"] as const;
const injectedRtkApi = api
  .enhanceEndpoints({
    addTagTypes,
  })
  .injectEndpoints({
    endpoints: (build) => ({
      getWeatherForecast: build.query<
        GetWeatherForecastApiResponse,
        GetWeatherForecast
      >({
        query: () => ({ url: `/weatherforecast` }),
        providesTags: ["Weather1"],
      }),
    }),
    overrideExisting: false,
  });
export { injectedRtkApi as kubis1982Api };
export type GetWeatherForecastApiResponse =
  /** status 200 OK */ WeatherForecast[];
export type GetWeatherForecast = void;
export type WeatherForecast = {
  date: string;
  temperatureC: number;
  summary: string | null;
  temperatureF?: number;
};
