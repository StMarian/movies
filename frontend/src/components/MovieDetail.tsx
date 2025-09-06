import { MovieDetail as MovieDetailType } from "../models/MovieDetail";

export default function MovieDetail({ movie }: { movie: MovieDetailType }) {
  return (
    <div className="movie-detail">
      <h1>{movie.headline}</h1>
      <p>{movie.synopsis}</p>
      <p>Genres: {movie.genres.join(", ")}</p>
      <p>Duration: {movie.duration} sec</p>
      <p>Rating: {movie.rating}</p>
      <p>Cast: {movie.cast.map((c) => c.name).join(", ")}</p>
      <p>Directors: {movie.directors.map((d) => d.name).join(", ")}</p>
      {movie.cardImages[0] && <img src={movie.cardImages[0].url} alt={movie.headline} />}
    </div>
  );
}
