import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import { useRouter } from "next/router";
import CoinInfo from "../../components/CoinAccount/CoinInfo";
import CoinInfoNext from "../../components/CoinAccount/CoinInfoNext";
import Layout from "../../components/Layout";
import { userContext } from "../../auth/auth";
import { coinHistory } from "../../data/coin-history";
import Rate from "../../components/Rating/RatingC.js";
import CurrentRating from "../../components/CurrentRating/CurrentRating";
import CoinSentiment from "../../components/CoinSentiment/CoinSentiment";
import { FaChevronCircleLeft } from "react-icons/fa";
import { XIcon } from "@heroicons/react/outline";
import SuggestedAccount from "../../components//InfoSection/SuggestedAccount";

const Coin = () => {
  const router = useRouter();
  const { id } = router.query;
  const { user, url } = useContext(userContext);
  const [coinData, setCoinData] = useState({});
  const [isFollowing, setIsFollowing] = useState(false);
  const [clicked, setClicked] = useState(false);
  const [amount, setAmount] = useState(0);
  const [amountInput, setAmountInput] = useState(0);
  const [AverageRate, setAverageRate] = useState(0);
  const [AverageCount, setAverageCount] = useState(0);
  const [, setError] = useState(false);
  const [, setLoading] = useState(false);
  const [followers, setFollowers] = useState([]);
  const [showFollowingModal, setFollowingShowModal] = useState(false);
  const [following, setFollowing] = useState([]);
  const [showModal, setShowModal] = useState(false);

  const handleGetCoin = () => {
    const options = {
      method: "GET",
    };

    fetch(`https://api.coincap.io/v2/assets/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setCoinData(data.data);
        console.log("coinData", id);
        console.log("followers", followers);
      })
      .catch((error) => {});
  };

  const handleGetCoinRating = () => {
    const options = {
      method: "GET",
    };
    fetch(`${url}/api/Coin/GetCoinRating/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setAverageRate(data.rating);
        setAverageCount(data.count);
      })
      .catch((error) => {});
  };

  const handleUnfollowCoin = () => {
    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: user.id,
        followerId: id,
      }),
    };

    fetch(`${url}/api/Coin/UnfollowCoin/${user.id}/${id}`, options)
      .then((response) => {
        setClicked(true);
        setIsFollowing(false);
        response.json();
      })
      .then((data) => {
        setClicked(true);
        setIsFollowing(false);
      })
      .catch(() => {});
  };

  const checkFollowing = () => {
    fetch(`${url}/api/Coin/GetCoinsFollowers/${id}`)
      .then((response) => response.json())
      .then((data) => {
        data.map((d) => {
          if (d.userId == user.id) {
            setIsFollowing(true);
          }
        });
      })
      .catch((error) => {});
  };

  const handleFollowCoin = () => {
    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: user.id,
        followerId: id,
      }),
    };

    fetch(`${url}/api/Coin/FollowCoin/${user.id}/${id}`, options)
      .then((response) => {
        setClicked(true);
        setIsFollowing(true);
        response.json();
      })
      .then((data) => {
        setClicked(true);
        setIsFollowing(true);
      })
      .catch(() => {});
  };

  const handleViewFollowers = () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/Coin/GetCoinsFollowers/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        setFollowers(data);
      })
      .catch((error) => {
        setError(true);
        setLoading(false);
      });
  };

  useEffect(() => {
    id == undefined && router.push("/");
  }, []);

  useEffect(() => {
    checkFollowing();
    handleGetCoin();
    handleGetCoinRating();
    handleViewFollowers();
    const interval = setInterval(() => {
      handleGetCoin();
    }, 10000);
    return () => clearInterval(interval);
  }, []);

  return (
    <>
      <Head>
        {/* FIX */}
        <title>{coinData != null && coinData.name}</title>
      </Head>
      <Layout>
        <div className="flex flex-col sm:flex-row w-full sm:w-6/12 items-center mt-8">
          <div
            className="w-32 h-32 bg-black sm:mr-10 mb-5"
            style={{ borderRadius: "100%" }}
          ></div>
          <div className="flex flex-row">
            <p className="font-semibold text-center sm:text-left mr-4">
              {coinData.name != undefined && coinData.name}
            </p>{" "}
            {/* ==================================================================== */}
            <div className="flex flex-row">
              <p className="font-semibold text-center sm:text-left"></p>{" "}
              {user.auth ? (
                isFollowing ? (
                  <>
                    <button onClick={handleUnfollowCoin}>
                      <p className="text-sm ml-5 text-black bg-gray-400 rounded-md px-3 py-1">
                        Following
                      </p>
                    </button>
                  </>
                ) : (
                  <>
                    <button onClick={handleFollowCoin}>
                      <p className="text-sm text-white ml-5 bg-indigo-600 rounded-md px-3 py-1 hover:bg-indigo-500 transition -translate-x-5">
                        Follow
                      </p>
                    </button>
                  </>
                )
              ) : null}
              <br />
            </div>
            {/* ========================================================================= */}
            <br />
            <div className="flex flex-row -translate-y-10"></div>
          </div>
        </div>
        <div className="flex flex-row -translate-y-10 -translate-x-20">
          <button
            className="mr-3 -translate-x-14"
            onClick={() => setShowModal(true)}
          >
            {" "}
            <span className="font-semibold" f>
              {`${followers.length} `}
            </span>
            followers
          </button>
        </div>

        <div className="bg-gray-400 sm:w-6/12" style={{ height: "1px" }}></div>
        <div className="flex flex-col items-center w-10/12 sm:w-6/12">
          <div>
            <p className="text-sm mt-4 text-gray-600">Info</p>
          </div>
          <CoinSentiment id={id} />
          <CoinInfo
            name="Price"
            price={Math.round(coinData.priceUsd * 100) / 100}
          />

          <div className="bg-white m-4 p-4 rounded-lg w-full">
            <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-left text-gray-700">
              Users Rating:
            </p>
            <div className="flex flex-col mb-2">
              <p className="ml-2 text-3xl">{AverageRate}</p>
            </div>
            <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-left text-gray-700">
              Total number of Ratings:
            </p>
            <div className="flex flex-col mb-2">
              <p className="ml-2 text-3xl">{AverageCount}</p>
            </div>
          </div>

          <div className="bg-white m-4 p-4 rounded-lg w-full">
            <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-left text-gray-700">
              Calculate Price
            </p>
            <div className="flex flex-col mb-2">
              <div className="flex flex-col sm:px-24 text-left sm:-translate-x-24 ml-1">
                <input
                  className="border text-sm mb-3 mt-3 h-10 rounded-md sm:w-full px-2 py-1 mr-1 sm:mr-4 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
                  placeholder="Amount"
                  onChange={(e) =>
                    setAmountInput(
                      (e.target.value * Math.round(coinData.priceUsd * 100)) /
                        100
                    )
                  }
                />
                <p className="text-3xl font-semibold ml-1">{amountInput} USD</p>
              </div>
            </div>
          </div>
          <CoinInfoNext
            id={id}
            name="Change"
            state={`${Math.round(coinData.changePercent24Hr * 100) / 100}%`}
            arrow={coinData.changePercent24Hr < 0 ? "down" : "up"}
          />

          <div className="bg-white m-4 p-4 rounded-lg w-full">
            <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-left text-gray-700">
              About {coinData.name}
            </p>
            <div className="flex flex-col mb-2">
              <p className="ml-2 text-base">
                {coinHistory.map((coin) => {
                  if (coin.name == id) {
                    return coin.history;
                  }
                })}
              </p>
            </div>
          </div>

          <div className="bg-white m-4 p-4 rounded-lg w-full">
            {/* remember IF statement */}

            <div className="flex flex-col mb-2 translate-x-1">
              <CurrentRating />
            </div>
            <div className="flex flex-col mb-2 translate-x-1">
              <Rate />
            </div>

            {showModal ? (
              <>
                <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
                  <div className="relative w-11/12 sm:w-6/12 my-6 mx-auto max-w-3xl">
                    <div className="border-0 rounded-lg shadow-sm relative flex flex-col w-full bg-white outline-none focus:outline-none">
                      <div className="flex items-start justify-between p-5 border-solid border-slate-200 rounded-t">
                        <h2>Followers</h2>
                        <button
                          className="px-1 p-1"
                          type="button"
                          onClick={() => setShowModal(false)}
                        >
                          <XIcon className="h-6 w-6" aria-hidden="true" />
                        </button>
                      </div>
                      <div className="relative flex-auto">
                        <form method="POST">
                          <div className="px-4 py-5 bg-white space-y-6 sm:p-6">
                            <div>
                              <div className="mt-1">
                                {followers.map((data, index) => {
                                  return (
                                    <SuggestedAccount
                                      key={index}
                                      name={data.username}
                                      hidefollow={true}
                                      id={data.userId}
                                      suggestions={true}
                                    />
                                  );
                                })}
                              </div>
                            </div>
                          </div>
                        </form>
                      </div>
                      <div className="flex items-center justify-end p-6 border-solid border-slate-200 rounded-b"></div>
                    </div>
                  </div>
                </div>
                <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
              </>
            ) : null}
          </div>
        </div>
      </Layout>
    </>
  );
};

export default Coin;
