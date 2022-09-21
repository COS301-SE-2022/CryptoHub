import React, { useState, useEffect } from "react";
import { userContext } from "../../auth/auth";
import { useContext } from "react";
import { useRouter } from "next/router";

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

  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <p className="text-xl font-semibold mb-2  ml-2 text-left text-gray-700">
        Sentiment
      </p>
      <div className="flex flex-col mb-2">
        {/* <p className="ml-2  mb-2 text-base">Based off {numberOfPosts} posts</p> */}
        <p className="ml-2 text-base">
          {sentiment == null ? "No sentiment" : handleSentimentScore(sentiment)}
        </p>
      </div>
    </div>
  );
};

export default CoinSentiment;
