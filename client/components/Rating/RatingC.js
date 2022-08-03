import { FaStar } from "react-icons/fa";
import { Container, Radio, Rating } from "./RatingStyles";
import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import { useRouter } from "next/router";
import { userContext } from "../../auth/auth";

const Rate = () => {
  const [rate, setRate] = useState(0);
  const router = useRouter();
  const { id } = router.query;
  const { user } = useContext(userContext);

  const handleGetCoinRating = () => {
    const options = {
      method: "GET",
    };

    fetch(`http://localhost:7215/api/Coin/GetCoinRating/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setCoinData(data.data);
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

    fetch(`http://localhost:7215/api/Coin/FollowCoin/${user.id}/${id}`, options)
      .then((response) => {
        setClicked(true);
        setIsFollowing(true);
        response.json();
      })
      .then((data) => {
        setRate(givenRating);
        setClicked(true);
        setIsFollowing(true);
      })
      .catch(() => {});

    console.log(givenRating);

    console.log(rate);
  };

  useEffect(() => {
    handleGetCoinRating();
  }, []);

  return (
    <Container>
      {[...Array(5)].map((item, index) => {
        const givenRating = index + 1;

        return (
          <label>
            <Radio
              type="radio"
              value={givenRating}
              onClick={() => {
                console.log(givenRating);
                handleRateCoin(givenRating);
              }}
            />
            <Rating>
              <FaStar
                color={givenRating === rate ? "000" : "rgb(192,192,192)"}
              />
            </Rating>
          </label>
        );
      })}
    </Container>
  );
};

export default Rate;
