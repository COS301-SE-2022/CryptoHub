import React, { useState, useEffect, useContext } from "react";
import Layout from "../components/Layout";
import Head from "next/head";
import { userContext } from "../auth/auth";
import Post from "../components/Posts/Post";
import { useRouter } from "next/router";
import { XIcon } from "@heroicons/react/outline";
import SuggestedAccount from "../components/InfoSection/SuggestedAccount";
import CoinInfo from "../components/CoinAccount/CoinInfo";
import CoinInfoNext from "../components/CoinAccount/CoinInfoNext";

const Coin = () => {
  const { id } = router.query;
  const { user } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [, setError] = useState(false);
  const [, setLoading] = useState(false);
  const router = useRouter();
  const [followers, setFollowers] = useState([]);
  const [following, setFollowing] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [showFollowingModal, setFollowingShowModal] = useState(false);
  const [coinData, setCoinData] = useState({});

  const handleGetCoin = () => {
    const options = {
      method: "GET",
    };

    fetch(`https://api.coincap.io/v2/assets/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        console.warn(data);
        setCoinData(data);
      })
      .catch((error) => {});
  };
  useEffect(() => {
    handleGetCoin();
  }, []);
  return (
    <>
      <Head>
        <title>Coin Account</title>
      </Head>
      <Layout>
        <div className="flex flex-col sm:flex-row w-full sm:w-6/12 items-center mt-8">
          <div
            className="w-32 h-32 bg-black sm:mr-10 mb-5"
            style={{ borderRadius: "100%" }}
          ></div>
          <div className="flex flex-col">
            <p className="font-semibold text-center sm:text-left ">Bitcoin</p>{" "}
            <br />
            <div className="flex flex-row -translate-y-5">
              <button onClick={() => setShowModal(true)}>
                {" "}
                <span className="font-semibold" f>
                  {`${followers.length} `}
                </span>
                followers
              </button>
            </div>
          </div>
        </div>

        <div className="bg-gray-400 sm:w-6/12" style={{ height: "1px" }}></div>
        <div className="flex flex-col items-center w-full sm:w-6/12">
          <div>
            <p className="text-sm mt-4 text-gray-600">Coin Info</p>
          </div>
          <div className="w-full"></div>
          <CoinInfo name="Current Price" price="100" />
          <CoinInfoNext name="Current State" state="13.56%" arrow="up" />
        </div>
      </Layout>
    </>
  );
};

export default Coin;
