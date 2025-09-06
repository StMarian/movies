import { Outlet } from "react-router-dom";

export default function Root() {
  return (
    <div>
      <header>
        <h1>Movies Feed</h1>
      </header>
      <main>
        <Outlet />
      </main>
    </div>
  );
}
