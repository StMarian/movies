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
  cardImages: { url: string; w: number; h: number }[];
  keyArtImages: { url: string; w: number; h: number }[];
  viewingWindow?: { startDate: string; endDate: string; wayToWatch: string };
}
