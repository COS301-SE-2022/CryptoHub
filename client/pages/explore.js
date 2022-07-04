import Layout from "../components/Layout";
import Feed from "../components/Feed/Feed";
import Head from "next/head";
import Posts from "../components/Posts/Posts";
import { useRouter } from "next/router";

const Explore = () => {
  const router = useRouter();

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout>
        <Posts />
      </Layout>
    </>
  );
};

export default Explore;
