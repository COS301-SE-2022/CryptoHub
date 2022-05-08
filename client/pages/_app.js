import "../styles/globals.css";
import { useState } from "react";
import "../components/LandingPage/Carousel.css";

function MyApp({ Component, pageProps }) {
  return <Component {...pageProps} />;
}

export default MyApp;
