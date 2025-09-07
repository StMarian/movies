export interface MovieSummary {
  id: string;
  headline: string;
  year: string;
  rating: number;
  cardImages: { hash: string; width: number; height: number }[];
}
