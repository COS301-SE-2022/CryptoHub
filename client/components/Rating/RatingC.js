import React, { useState } from "react";
import { FaStar } from "react-icons/fa";
import { Container, Radio, Rating } from "./RatingStyles";

const Rate = () => {
  const [rate, setRate] = useState(0);
  const router = useRouter();
  const { id } = router.query;
  const { user } = useContext(userContext);
  const [coinData, setCoinData] = useState({});
  const [isFollowing, setIsFollowing] = useState(false);
  const [clicked, setClicked] = useState(false);

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
                setRate(givenRating);
                // console.log(givenRating); //This is to get the rating of the coing

                //alert(`Are you sure you want to give ${givenRating} stars ?`);
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
  );
};

export default Rate;
