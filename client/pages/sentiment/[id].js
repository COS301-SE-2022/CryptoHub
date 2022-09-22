import React, { useContext, useState, useEffect } from "react";
import Layout from "../../components/Layout";
import { userContext } from "../../auth/auth";
import Head from "next/head";
import { useRouter } from "next/router";
import Link from "next/link";
import TagPosts from "../../components/Posts/TagPosts";

const Sentiment = () => {
  const router = useRouter();
  const { id } = router.query;

  useEffect(() => {}, []);

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout>
        <TagPosts tag={id} />
      </Layout>
    </>
  );
};

export default Sentiment;
