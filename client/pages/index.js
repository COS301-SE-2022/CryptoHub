import { useState } from "react";
import Layout from "../components/Layout";
import LandingPage from "../components/LandingPage/LandingPage";

export default function Home({ Component, pageProps }) {
  const [isUserLoggedIn] = useState(false);
  return <>{isUserLoggedIn ? <Layout></Layout> : <LandingPage />}</>;
}
