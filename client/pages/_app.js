import Layout from "../components/Layout";
import LandingPage from "../components/LandingPage/LandingPage";
import "../styles/globals.css";
import { useState } from "react";

function MyApp({ Component, pageProps }) {
  const [isUserLoggedIn] = useState(false);

  return (
    <>
      {isUserLoggedIn ? (
        <Layout>
          <Component {...pageProps} />
        </Layout>
      ) : (
        <LandingPage />
      )}
    </>
  );
}

export default MyApp;
