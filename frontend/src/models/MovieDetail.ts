export interface MovieDetail {
  id: string;
  headline: string;
  body: string;
  synopsis: string;
  duration: number;
  genres: string[];
  rating: number;
  cast: { name: string }[];
  directors: { name: string }[];
  cardImages: { hash: string; width: number; height: number }[];
  keyArtImages: { hash: string; width: number; height: number }[];
  viewingWindow?: { startDate: string; endDate: string; wayToWatch: string };
  
  // Additional fields from backend MovieDetailDto
  year?: string;
  cert?: string;
  quote?: string;
  reviewAuthor?: string;
  skyGoId?: string;
  skyGoUrl?: string;
  sum?: string;
}
