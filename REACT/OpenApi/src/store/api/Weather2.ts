import { emptySplitApi as api } from "../emptyApi";
export const addTagTypes = ["Weather2"] as const;
const injectedRtkApi = api
  .enhanceEndpoints({
    addTagTypes,
  })
  .injectEndpoints({
    endpoints: (build) => ({
      getWeatherForecast2: build.query<
        GetWeatherForecast2ApiResponse,
        GetWeatherForecast2
      >({
        query: () => ({ url: `/weatherforecast2` }),
        providesTags: ["Weather2"],
      }),
    }),
    overrideExisting: false,
  });
export { injectedRtkApi as kubis1982Api };
export type GetWeatherForecast2ApiResponse =
  /** status 200 OK */ WeatherForecast[];
export type GetWeatherForecast2 = void;
export type WeatherForecast = {
  date: string;
  temperatureC: number;
  summary: string | null;
  temperatureF?: number;
};
