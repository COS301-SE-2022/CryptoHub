import { useState, useContext } from "react";
import Layout from "../components/Layout";
import LandingPage from "../components/LandingPage/LandingPage";
import { userContext } from "../auth/auth";
import Feed from "../components/Feed/Feed";

export default function Home({ Component, pageProps }) {
  const [isUserLoggedIn] = useState(false);
  const { user } = useContext(userContext);

  return (
    <>
      {user.auth ? (
        <Layout>
          <Feed />
        </Layout>
      ) : (
        <LandingPage />
      )}
    </>
  );
}
