import Layout from "../components/Layout";
import "../styles/globals.css";
import { useState } from "react";

function MyApp({ Component, pageProps }) {
  const [isUserLoggedIn] = useState(true);

  return (
    <>
      {isUserLoggedIn ? (
        <Layout>
          <Component {...pageProps} />
        </Layout>
      ) : (
        <h1 className="flex justify-center">Please login</h1>
      )}
    </>
  );
}

export default MyApp;
