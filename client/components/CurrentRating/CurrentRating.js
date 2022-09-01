import { FaStar } from "react-icons/fa";
import { Container, Radio, Rating } from "./CurrentRatingStyles";
import React, { useState, useEffect, useContext } from "react";
import { useRouter } from "next/router";
import { userContext } from "../../auth/auth";

function CurrentRating() {
  const [rate, setRate] = useState(0);
  const [testrate, setTestrate] = useState(0);
  const router = useRouter();
  const { id } = router.query;
  const { user } = useContext(userContext);
  const [rated, setRated] = useState(false);

  const handleGetCoinRating = () => {
    const options = {
      method: "GET",
    };
    // console.log(id);
    // console.log(user.id);

    fetch(
      `http://localhost:7215/api/Coin/GetCoinRatingByUserId/${user.id}/${id}`,
      options
    )
      .then((response) => {
        console.log("Rating " + response.status);
        if (response.status == 200) {
          setRated(true);
        } else {
          setRated(false);
        }

        return response.json();
      })

      .then((data) => {
        setRate(data.rating);
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

  useEffect(() => {
    handleGetCoinRating();
  }, []);

  return (
    <div className={`${rated == false && "hidden"}`}>
      <p className="text-xl font-semibold mb-2 ml-2 -translate-x-2 text-left text-gray-700">
        Your current rating is:
      </p>
      <p className="text-5xl -translate-y-2">{rate}</p>
      <p className="text-xl font-semibold mb-2 ml-2 -translate-x-2 text-left text-gray-700">
        Change rating here:
      </p>

      <Container className={`${rated == false && "hidden"}`}>
        {[...Array(5)].map((item, index) => {
          const givenRating = index + 1;

          return (
            <label>
              <Radio
                type="radio"
                value={givenRating}
                onClick={() => {
                  setRate(givenRating);
                  handleRateCoin(givenRating);
                }}
              />
              <Rating>
                <FaStar
                  color={
                    givenRating < rate || givenRating === rate
                      ? "000"
                      : "rgb(192,192,192)"
                  }
                />
              </Rating>
            </label>
          );
        })}
      </Container>
    </div>
  );
}

export default CurrentRating;
