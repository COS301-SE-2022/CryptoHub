import React, { useState, useEffect, useContext } from "react";
import Layout from "../components/Layout";
import Head from "next/head";
import { userContext } from "../auth/auth";
import Post from "../components/Posts/Post";
import { useRouter } from "next/router";
import { XIcon } from "@heroicons/react/outline";
import SuggestedAccount from "../components/InfoSection/SuggestedAccount";

const Coin = () => {
  const { user } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [, setError] = useState(false);
  const [, setLoading] = useState(false);
  const router = useRouter();
  const [followers, setFollowers] = useState([]);
  const [following, setFollowing] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [showFollowingModal, setFollowingShowModal] = useState(false);

  return (
    <>
      <Head>
        <title>Coin</title>
      </Head>
      <Layout></Layout>
    </>
  );
};

export default Coin;
