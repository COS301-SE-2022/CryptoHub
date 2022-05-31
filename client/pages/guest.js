import Layout from "../components/Layout";
import Feed from "../components/Feed/Feed";
import Head from "next/head";

const Guest = () => {
  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout>
        <Feed />
      </Layout>
    </>
  );
};

export default Guest;
