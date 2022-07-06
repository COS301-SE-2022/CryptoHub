import Layout from "../components/Layout";
import Feed from "../components/Feed/Feed";
import Head from "next/head";
import ExplorePosts from "../components/Posts/ExplorePosts";
import { useRouter } from "next/router";
import { userContext } from "../auth/auth";
import { useContext } from "react";
import Suggestions from "../components/InfoSection/Suggestions";

const Explore = () => {
  const router = useRouter();
  const { user } = useContext(userContext);

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout>
        <ExplorePosts />
      </Layout>
    </>
  );
};

export default Explore;
