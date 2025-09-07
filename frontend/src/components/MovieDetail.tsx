import { MovieDetail as MovieDetailType } from "../models/MovieDetail";
import { getImageUrl, getMovieImageUrl } from "../api/imageUrl";
import { useState, useMemo } from "react";
import placeholderImage from "../assets/placeholder-no-image.jpg";
import "./MovieDetail.css";

export default function MovieDetail({ movie }: { movie: MovieDetailType }) {
  // Memoize the initial image sources to prevent unnecessary re-processing
  const initialCardImageSources = useMemo(() => 
    movie.cardImages.map(img => getImageUrl(img.hash)),
    [movie.cardImages]
  );
  
  const initialKeyArtImageSources = useMemo(() => 
    movie.keyArtImages?.map(img => getImageUrl(img.hash)) || [],
    [movie.keyArtImages]
  );

  const [cardImageSources, setCardImageSources] = useState<string[]>(initialCardImageSources);
  const [keyArtImageSources, setKeyArtImageSources] = useState<string[]>(initialKeyArtImageSources);

  // For the main poster image, make sure we have same image as in MovieCard
  const [posterImageSrc, setPosterImageSrc] = useState<string>(getMovieImageUrl(movie.id));

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

  const renderImage = (
    src: string, 
    alt: string, 
    onError: () => void
  ) => {
    return (
      <img
        src={src}
        alt={alt}
        loading="lazy"
        onError={onError}
        className="gallery-image"
      />
    );
  };

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
            className="poster-image"
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
            {movie.cert && <span className="certificate">{movie.cert}</span>}
            <span className="rating">★ {movie.rating}</span>
          </div>
          
          {movie.genres?.length > 0 && (
            <p className="genres">
              <strong>Genres:</strong> {movie.genres.join(", ")}
            </p>
          )}
          
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
        {movie.cast?.length > 0 && (
          <div className="cast">
            <h2 className="section-heading">Cast</h2>
            <p>{movie.cast.map(c => c.name).join(", ")}</p>
          </div>
        )}
        
        {movie.directors?.length > 0 && (
          <div className="directors">
            <h2 className="section-heading">Directors</h2>
            <p>{movie.directors.map(d => d.name).join(", ")}</p>
          </div>
        )}
      </div>
      
      <div className="images-section">
        <h2 className="images-heading">Movie Images</h2>
        
        {movie.keyArtImages?.length > 0 && (
          <div className="keyart-images">
            <h3 className="section-heading">Key Art Images</h3>
            <div className="image-gallery">
              {movie.keyArtImages.map((_img, index) => {                
                return (
                  <div key={index} className="image-card">
                    {renderImage(
                      keyArtImageSources[index] || placeholderImage,
                      `${movie.headline} - Key Art ${index}`,
                      () => handleKeyArtImageError(index)
                    )}
                  </div>
                );
              }).filter(Boolean)}
            </div>
          </div>
        )}
        
        {movie.cardImages.length > 0 && (
          <>
            <h3 className="section-heading">Card Images</h3>
            <div className="image-gallery">
              {movie.cardImages.map((_img, index) => (
                <div key={index} className="image-card">
                  {renderImage(
                    cardImageSources[index] || placeholderImage,
                    `${movie.headline} - Image ${index + 1}`,
                    () => handleCardImageError(index)
                  )}
                </div>
              ))}
            </div>
          </>
        )}
        
        {movie.cardImages.length === 0 && movie.keyArtImages?.length === 0 && (
          <p className="no-images">No additional images available</p>
        )}
      </div>
    </div>
  );
}
