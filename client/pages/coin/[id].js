import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import { useRouter } from "next/router";
import CoinInfo from "../../components/CoinAccount/CoinInfo";
import CoinInfoNext from "../../components/CoinAccount/CoinInfoNext";
import Layout from "../../components/Layout";
import { userContext } from "../../auth/auth";

const Coin = () => {
  const router = useRouter();
  const { id } = router.query;
  const { user } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [, setError] = useState(false);
  const [, setLoading] = useState(false);
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
        setCoinData(data.data);
      })
      .catch((error) => {});
  };
  useEffect(() => {
    handleGetCoin();
  }, []);

  return (
    <>
      <Head>
        <title>{coinData.name}</title>
      </Head>
      <Layout>
        <div className="flex flex-col sm:flex-row w-full sm:w-6/12 items-center mt-8">
          <div
            className="w-32 h-32 bg-black sm:mr-10 mb-5"
            style={{ borderRadius: "100%" }}
          ></div>
          <div className="flex flex-col">
            <p className="font-semibold text-center sm:text-left ">
              {coinData.name}
            </p>{" "}
            <br />
            <div className="flex flex-row -translate-y-5">
              {/* <button onClick={() => setShowModal(true)}>
                {" "}
                <span className="font-semibold" f>
                  {`${followers.length} `}
                </span>
                followers
              </button> */}
            </div>
          </div>
        </div>

        <div className="bg-gray-400 sm:w-6/12" style={{ height: "1px" }}></div>
        <div className="flex flex-col items-center w-full sm:w-6/12">
          <div>
            <p className="text-sm mt-4 text-gray-600">Coin Info</p>
          </div>
          <div className="w-full"></div>
          <CoinInfo
            name="Current Price"
            price={Math.round(coinData.priceUsd * 100) / 100}
          />
          <CoinInfoNext
            id={id}
            name="Current State"
            state={`${Math.round(coinData.changePercent24Hr * 100) / 100}%`}
            arrow={coinData.changePercent24Hr < 0 ? "down" : "up"}
          />
        </div>
      </Layout>
    </>
  );
};

export default Coin;
