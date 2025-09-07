import { MovieDetail as MovieDetailType } from "../models/MovieDetail";
import { getImageUrl, getMovieImageUrl } from "../api/imageUrl";
import { useState } from "react";
import placeholderImage from "../assets/placeholder-no-image.jpg";
import "./MovieDetail.css";

export default function MovieDetail({ movie }: { movie: MovieDetailType }) {
  const [cardImageSources, setCardImageSources] = useState<string[]>(
    movie.cardImages.map(img => getImageUrl(img.hash))
  );
  const [keyArtImageSources, setKeyArtImageSources] = useState<string[]>(
    movie.keyArtImages?.map(img => getImageUrl(img.hash)) || []
  );

  // Handle image error by updating the specific image source to the placeholder
  const handleCardImageError = (index: number) => {
    setCardImageSources(prevSources => {
      const newSources = [...prevSources];
      newSources[index] = placeholderImage;
      return newSources;
    });
  };

  const handleKeyArtImageError = (index: number) => {
    setKeyArtImageSources(prevSources => {
      const newSources = [...prevSources];
      newSources[index] = placeholderImage;
      return newSources;
    });
  };

  // For the main poster image, make sure we have same image as in MovieCard
  const [posterImageSrc, setPosterImageSrc] = useState<string>(getMovieImageUrl(movie.id));

  return (
    <div className="movie-detail">
      <div className="movie-detail-header">
        <div className="movie-detail-poster">
          <img
            src={posterImageSrc}
            width={300}
            height={450}
            alt={`${movie.headline} - Poster`}
            onError={() => setPosterImageSrc(placeholderImage)}
          />
        </div>
        
        <div className="movie-detail-info">
          <h1>{movie.headline}</h1>
          <div className="movie-meta">
            {movie.year && <span className="year">{movie.year}</span>}
            {movie.duration && (
              <span className="duration">
                {Math.floor(movie.duration / 60)}h {movie.duration % 60}m
              </span>
            )}
            {movie.cert && (
              <span className="certificate">
                {movie.cert}
              </span>
            )}
            <span className="rating">★ {movie.rating}</span>
          </div>
          
          {movie.genres && <p className="genres"><strong>Genres:</strong> {movie.genres.join(", ")}</p>}
          
          {movie.synopsis && (
            <div className="synopsis">
              <h3>Synopsis</h3>
              <p>{movie.synopsis}</p>
            </div>
          )}
          
          {movie.quote && (
            <div className="quote">
              <p>"{movie.quote}"</p>
              {movie.reviewAuthor && <p>— {movie.reviewAuthor}</p>}
            </div>
          )}
          
          {movie.skyGoUrl && (
            <div className="watch-now">
              <a 
                href={movie.skyGoUrl} 
                target="_blank" 
                rel="noopener noreferrer"
                className="watch-button"
              >
                Watch Now on SkyGo
              </a>
            </div>
          )}
        </div>
      </div>
      
      {movie.body && (
        <div className="body">
          <h2 className="section-heading">Description</h2>
          <p className="movie-body">{movie.body}</p>
        </div>
      )}
      
      <div className="movie-credits">
        {movie.cast && movie.cast.length > 0 && (
          <div className="cast">
            <h2 className="section-heading">Cast</h2>
            <p>{movie.cast.map(c => c.name).join(", ")}</p>
          </div>
        )}
        
        {movie.directors && movie.directors.length > 0 && (
          <div className="directors">
            <h2 className="section-heading">Directors</h2>
            <p>{movie.directors.map(d => d.name).join(", ")}</p>
          </div>
        )}
      </div>
      
      <div className="images-section">
        <h2 className="images-heading">Movie Images</h2>
        
        {movie.keyArtImages && movie.keyArtImages.length > 1 && (
          <div className="keyart-images">
            <h3 className="section-heading">Key Art Images</h3>
            <div className="image-gallery">
              {movie.keyArtImages.slice(1).map((img, index) => (
                <div key={index} className="image-card">
                  <img
                    src={keyArtImageSources[index + 1] || keyArtImageSources[index]}
                    width={img.width}
                    height={img.height}
                    alt={`${movie.headline} - Key Art ${index + 1}`}
                    loading="lazy"
                    onError={() => handleKeyArtImageError(index + 1)}
                  />
                </div>
              ))}
            </div>
          </div>
        )}
        
        <h3 className="section-heading">Card Images</h3>
        <div className="image-gallery">
          {movie.cardImages.map((img, index) => (
            <div key={index} className="image-card">
              <img
                src={cardImageSources[index]}
                width={img.width}
                height={img.height}
                alt={`${movie.headline} - Image ${index + 1}`}
                loading="lazy"
                onError={() => handleCardImageError(index)}
              />
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
