import { FaStar } from "react-icons/fa";
import { Container, Radio, Rating } from "./RatingStyles";
import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import { useRouter } from "next/router";
import { userContext } from "../../auth/auth";

function CurrentRating() {
  const [rate, setRate] = useState(0);
  const router = useRouter();
  const { id } = router.query;
  const { user } = useContext(userContext);

  const handleGetCoinRating = () => {
    const options = {
      method: "GET",
    };
    console.log(id);
    console.log(user.id);

    fetch(
      `http://localhost:7215/api/Coin/GetCoinRatingByUserId/${user.id}/${id}`,
      options
    )
      .then((response) => response.json())
      .then((data) => {
        setCoinData(data.data);
        setRate(data.rating);
      })
      .catch((error) => {});
  };

  const handleRateCoin = (givenRating) => {
    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: user.id,
        coinId: id,
        rate: givenRating,
      }),
    };

    fetch(
      `http://localhost:7215/api/Coin/RateCoin/${user.id}/${id}/${givenRating}`,
      options
    )
      .then((response) => {
        setClicked(true);
        setIsFollowing(true);
        response.json();
      })
      .then((data) => {
        setClicked(true);
        setIsFollowing(true);
        setRate(givenRating);
      })
      .catch(() => {});
  };

  return <div>CurrentRating</div>;
}

export default CurrentRating;
