export function getImageUrl(hash: string): string {
  return `${import.meta.env.VITE_API_URL}/images/${hash}`;
}
