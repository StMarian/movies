export interface MovieSummary {
  id: string;
  headline: string;
  year: string;
  rating: number;
  image: { url: string; w: number; h: number };
}
