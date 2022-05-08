import { useState, useContext } from "react";
import Layout from "../components/Layout";
import LandingPage from "../components/LandingPage/LandingPage";
import { userContext } from "../auth/auth";

export default function Home({ Component, pageProps }) {
  const [isUserLoggedIn] = useState(false);
  const { user } = useContext(userContext);

  return <>{user.auth ? <Layout></Layout> : <LandingPage />}</>;
}
