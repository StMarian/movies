import { MovieDetail as MovieDetailType } from "../models/MovieDetail";
import { getImageUrl } from "../api/imageUrl";

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
      
      <h2>Card Images</h2>
      <div className="image-gallery" style={{ display: 'flex', flexWrap: 'wrap', gap: '10px' }}>
        {movie.cardImages.map((img, index) => (
          <img
            key={index}
            src={getImageUrl(img.hash)}
            width={img.width}
            height={img.height}
            alt={`${movie.headline} - Image ${index + 1}`}
            loading="lazy"
            style={{ maxWidth: '300px', objectFit: 'contain' }}
          />
        ))}
      </div>
    </div>
  );
}
