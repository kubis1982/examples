import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

const baseQuery = async (args: any, api: any, extraOptions: any) => {

  const rawBaseQuery = fetchBaseQuery({
    baseUrl: import.meta.env.VITE_API_URL
  })
  return rawBaseQuery(args, api, extraOptions);
}

export const emptySplitApi = createApi({
  baseQuery: baseQuery,
  endpoints: () => ({}),
})