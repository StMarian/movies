import axios from "axios";
import { MovieSummary } from "../models/MovieSummary";
import { MovieDetail } from "../models/MovieDetail";

const api = axios.create({ baseURL: import.meta.env.VITE_API_URL });

export const getMovies = async (): Promise<MovieSummary[]> => {
  const res = await api.get("/movies");
  return res.data;
};

export const getMovieById = async (id: string): Promise<MovieDetail> => {
  const res = await api.get(`/movies/${id}`);
  return res.data;
};
