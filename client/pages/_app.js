import "../styles/globals.css";
import "../components/LandingPage/Carousel.css";
import UserProvider from "../auth/auth";

function MyApp({ Component, pageProps }) {
  return (
    <UserProvider>
      <Component {...pageProps} />
    </UserProvider>
  );
}

export default MyApp;
