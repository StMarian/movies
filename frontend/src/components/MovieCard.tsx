import { MovieSummary } from "../models/MovieSummary";
import { useNavigate } from "react-router-dom";

export default function MovieCard({ movie }: { movie: MovieSummary }) {
  const navigate = useNavigate();
  return (
    <div className="movie-card" onClick={() => navigate(`/movie/${movie.id}`)}>
      <img src={movie.image.url} alt={movie.headline} />
      <h3>{movie.headline}</h3>
      <p>{movie.year}</p>
    </div>
  );
}
