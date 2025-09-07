import { MovieSummary } from "../models/MovieSummary";
import { useNavigate } from "react-router-dom";
import { getImageUrl } from "../api/imageUrl";

export default function MovieCard({ movie }: { movie: MovieSummary }) {
  const navigate = useNavigate();
  return (
    <div className="movie-card" onClick={() => navigate(`/movie/${movie.id}`)}>
      <div className="movie-image-container">
        <img
          src={getImageUrl(movie.cardImages[0].hash)}
          width={movie.cardImages[0].width}
          height={movie.cardImages[0].height}
          alt={movie.headline}
          loading="lazy"
        />
      </div>
      <div className="movie-info">
        <h3>{movie.headline}</h3>
        <div className="movie-metadata">
          <span className="movie-year">{movie.year}</span>
          <span className="movie-rating">★ {movie.rating.toFixed(1)}</span>
        </div>
      </div>
    </div>
  );
}
