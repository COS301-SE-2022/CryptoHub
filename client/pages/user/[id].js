import { useRouter } from "next/router";
import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import Layout from "../../components/Layout";
import { userContext } from "../../auth/auth";

const User = () => {
  const router = useRouter();
  const { id } = router.query;

  const handleGetUser = () => {};

  useEffect(() => {}, []);

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout></Layout>
    </>
  );
};

export default User;
