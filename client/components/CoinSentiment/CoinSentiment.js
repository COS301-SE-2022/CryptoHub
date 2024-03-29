import React, { useState, useEffect } from "react";
import { userContext } from "../../auth/auth";
import { useContext } from "react";
import { useRouter } from "next/router";
import Link from "next/link";

const CoinSentiment = ({ id }) => {
  const [sentiment, setSentiment] = useState(null);
  const [numberOfPosts, setNumberOfPosts] = useState(0);
  const { url } = useContext(userContext);

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
    handleGetCoinSentiment();
  }, []);

  const handleSentimentScore = (sentiment) => {
    if (sentiment >= 0.075) {
      return (
        <p className="bg-green-400 rounded-md w-28 text-center">
          Very Positive
        </p>
      );
    } else if (sentiment >= 0.05 && sentiment < 0.075) {
      return (
        <p className="bg-green-200 rounded-md w-28 text-center">Positive</p>
      );
    } else if (sentiment <= -0.05 && sentiment >= -0.075) {
      return <p className="bg-red-200 rounded-md w-28 text-center">Negative</p>;
    } else if (sentiment < -0.075) {
      return (
        <p className="bg-red-400 rounded-md w-28 text-center">Very Negative</p>
      );
    } else {
      return <p className="bg-gray-200 rounded-md w-28 text-center">Neutral</p>;
    }
  };

  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <p className="text-xl font-semibold mb-2  ml-2 text-left text-gray-700">
        Sentiment
      </p>
      <div className="flex flex-row mb-2">
        <p className="ml-2 text-base">
          {sentiment == null ? "No sentiment" : handleSentimentScore(sentiment)}
        </p>
        <p className="flex flex-row justify-center align-items bg-indigo-500 rounded-md w-2/6 text-center ml-3 font-light text-base text-white text-center">
          <Link href={`/sentiment/${id}`}>See Related Posts</Link>
        </p>
      </div>
    </div>
  );
};

export default CoinSentiment;
