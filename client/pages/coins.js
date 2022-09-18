import React, { useContext, useState, useEffect } from "react";
import Head from "next/head";
import Layout from "../components/Layout";
import { userContext } from "../auth/auth";

const coins = () => {
  const { user } = useContext(userContext);
  const [data, setData] = useState([]);

  const getCoinInfo = () => {
    const options = {
      method: "GET",
    };

    fetch("https://api.coincap.io/v2/assets", options)
      .then((response) => response.json())
      .then((data) => {
        setData(data.data);
      })
      .catch(() => {});
  };

  useEffect(() => {
    getCoinInfo();
  }, []);

  //   useEffect(() => {
  //     if (!user.auth) {
  //       router.push("/");
  //     }
  //   }, [user]);

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
