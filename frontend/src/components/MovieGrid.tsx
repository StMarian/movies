import { useEffect, useState } from "react";
import { getMovies } from "../api/movieApi";
import { MovieSummary } from "../models/MovieSummary";
import MovieCard from "./MovieCard";

export default function MovieGrid() {
  const [movies, setMovies] = useState<MovieSummary[]>([]);

  useEffect(() => {
    getMovies().then(setMovies);
  }, []);

  return (
    <div className="grid-container">
      {movies.map((movie) => (
        <MovieCard key={movie.id} movie={movie} />
      ))}
    </div>
  );
}
