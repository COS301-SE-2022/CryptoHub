import React, { useContext, useState, useEffect } from "react";
import Head from "next/head";
import Layout from "../components/Layout";
import { userContext } from "../auth/auth";

const coins = () => {
  const { user } = useContext(userContext);

  useEffect(() => {
    if (!user.auth) {
      router.push("/");
    }
  }, [user]);

  return (
    <div>
      <Head>
        <title>All Coins</title>
      </Head>
      <Layout></Layout>
    </div>
  );
};

export default coins;
