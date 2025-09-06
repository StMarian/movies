import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import MovieDetail from "../../components/MovieDetail";
import { getMovieById } from "../../api/movieApi";
import { MovieDetail as MovieDetailType } from "../../models/MovieDetail";

const MoviePage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [movie, setMovie] = useState<MovieDetailType | null>(null);

  useEffect(() => {
    if (id) getMovieById(id).then(setMovie);
  }, [id]);

  if (!id) return <div>No movie selected</div>;
  if (!movie) return <div>Loading...</div>;

  return <MovieDetail movie={movie} />;
};

export default MoviePage;
