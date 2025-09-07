import { MovieSummary } from "../models/MovieSummary";
import { useNavigate } from "react-router-dom";
import { getMovieImageUrl } from "../api/imageUrl";
import { useState } from "react";

import placeholderImage from "../assets/placeholder-no-image.jpg";

export default function MovieCard({ movie }: { movie: MovieSummary }) {
  const [imageSrc, setImageSrc] = useState<string>(getMovieImageUrl(movie.id));
  const navigate = useNavigate();
  return (
    <div className="movie-card" onClick={() => navigate(`/movie/${movie.id}`)}>
      <div className="movie-image-container">
        <img
          src={imageSrc}
          width={300}
          height={450}
          alt={movie.headline}
          loading="lazy"
          onError={() => setImageSrc(placeholderImage)}
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
