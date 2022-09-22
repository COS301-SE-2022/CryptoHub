import React, { useContext, useState, useEffect } from "react";
import Head from "next/head";
import Layout from "../components/Layout";
import { userContext } from "../auth/auth";
import Link from "next/link";
import Loader from "../components/Loader";

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
      <Layout>
        <WatchList />
      </Layout>
    </div>
  );
};

const WatchList = () => {
  return (
    <div className="bg-white sm:w-8/12 w-11/12 m-4 p-4 rounded-lg sm:fixed overflow-auto max-h-[40rem]">
      <p className="text-left font-semibold text-indigo-600 text-2xl mb-4">
        Current Trending Cryptocurrencies
      </p>
      <MyCoins />
    </div>
  );
};

const MyCoins = () => {
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
    const interval = setInterval(() => {
      getCoinInfo();
    }, 10000);
    return () => clearInterval(interval);
  }, []);

  return (
    <div>
      <div className="flex flex-row my-2 items-center">
        <p className="text-sm font-semibold cursor-pointer ml-5 sm:mr-24 mr-3 w-32 text-gray-500 translate-x-4">
          Name
        </p>
        <p className="text-sm font-semibold cursor-pointer ml-3 sm:mr-24 mr-3 w-24 text-gray-500 translate-x-2">
          Price
        </p>
        <p className="text-sm font-semibold cursor-pointer ml-3 sm:mr-24 mr-3 w-24 text-gray-500 translate-x-7">
          Change
        </p>
        <p className="text-sm font-semibold cursor-pointer ml-3 sm:mr-24 mr-3 w-24 text-gray-500 translate-x-7">
          Sentiment
        </p>
      </div>
      {data.length == 0 ? (
        <Loader />
      ) : (
        data.map((data, index) => {
          return <CoinInfo key={index} id={data.id} index={index + 1} />;
        })
      )}
    </div>
  );
};

const CoinInfo = ({ index, id }) => {
  const [coin, setCoin] = useState({});
  const [color, setColor] = useState("");
  const [sentiment, setSentiment] = useState(null);
  const { url } = useContext(userContext);

  const getCoinInfo = () => {
    fetch(`https://api.coincap.io/v2/assets/${id}`)
      .then((response) => response.json())
      .then((data) => {
        console.warn(data.data);
        setColor(
          data.data.changePercent24Hr < 0 ? "text-red-600" : "text-green-600"
        );
        setCoin(data.data);
      })
      .catch(() => {});
  };

  const handleSentimentScore = (sentiment) => {
    if (sentiment >= 0.07) {
      return (
        <p className="bg-green-400 rounded-md w-28 text-center">
          Very Positive
        </p>
      );
    } else if (sentiment <= 0.05 && sentiment < 0.07) {
      return (
        <p className="bg-green-200 rounded-md w-28 text-center">Positive</p>
      );
    } else if (sentiment <= -0.05 && sentiment >= -0.07) {
      return <p className="bg-red-200 rounded-md w-28 text-center">Negative</p>;
    } else if (sentiment <= -0.07) {
      return (
        <p className="bg-red-400 rounded-md w-28 text-center">Very Negative</p>
      );
    } else {
      return <p className="bg-gray-200 rounded-md w-28 text-center">Neutral</p>;
    }
  };

  const handleGetCoinSentiment = () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/Coin/GetCoinSentiment/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        console.warn("sentiment:", data);
        setSentiment(data.average);
        setNumberOfPosts(data.postsInTheLastweek);
      })
      .catch(() => {});
  };

  useEffect(() => {
    getCoinInfo();
    handleGetCoinSentiment();
  }, []);

  return (
    <div className="flex flex-row p-1 my-2 items-center bg-gray-100 rounded-md">
      <p className="text-xs text-gray-400 m-2">{index}</p>
      <Link href={`/coin/${id}`} className="flex flex-row">
        <p className="text-sm font-semibold cursor-pointer ml-3 sm:mr-24 mr-3 w-32">
          {coin.name}
        </p>
      </Link>
      <p className="text-md font-semibold text-indigo-600 sm:mr-24 mr-3 w-32">{`$${
        Math.round(coin.priceUsd * 10) / 10
      }`}</p>
      <p className={`${color} text-sm font-semibold sm:mr-24 mr-3 w-32`}>{` ${
        color === "text-green-600" ? "+" : ""
      }${Math.round(coin.changePercent24Hr * 100) / 100}%`}</p>
      <p className="ml-2 text-base sm:mr-24 mr-3 w-32 -translate-x-7">
        {sentiment == null ? "No sentiment" : handleSentimentScore(sentiment)}
      </p>
    </div>
  );
};

export default coins;
