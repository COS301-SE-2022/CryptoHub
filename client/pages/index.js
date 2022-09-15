import { useContext } from "react";
import Layout from "../components/Layout";
import LandingPage from "../components/LandingPage/LandingPage";
import { userContext } from "../auth/auth";
import Feed from "../components/Feed/Feed";
import Head from "next/head";

const Home = () => {
  const { user, url } = useContext(userContext);

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      {user.auth ? (
        <Layout>
          <Feed />
        </Layout>
      ) : (
        <LandingPage />
      )}
    </>
  );
};

export default Home;
