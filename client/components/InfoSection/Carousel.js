import React, { useState, useEffect } from "react";
import { mockCoinInfo } from "../../mocks/mockCoinInfo";
import Link from "next/link";

const Carousel = () => {
  const [data, setData] = useState([]);
  const [count, setCount] = useState(5);

  const getCoinInfo = () => {
    const options = {
      method: "GET",
    };

    fetch("https://api.coincap.io/v2/assets", options)
      .then((response) => response.json())
      .then((data) => {
        setData(data.data.slice(0, 6));
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
      <div className="flex flew-row sm:justify-center flex-wrap">
        {data.length == 0 ? (
          <p className="text-sm text-gray-500">Loading...</p>
        ) : (
          data.map((data, index) => {
            return (
              <CoinInfo
                key={index}
                name={data.name}
                price={data.priceUsd}
                difference={data.changePercent24Hr}
                color={
                  data.changePercent24Hr < 0 ? "text-red-600" : "text-green-600"
                }
                id={data.id}
              />
            );
          })
        )}
      </div>
      <Link href="/coins">
        <p className="text-sm cursor-pointer text-blue-800 -translate-y-2 -translate-x-7 text-right">
          See more coins
        </p>
      </Link>
    </div>
  );
};

export default Carousel;

const CoinInfo = ({ name, price, difference, color, id }) => {
  return (
    <div className="flex flex-col p-4 m-1 items-center">
      <Link href={`/coin/${id}`}>
        <p className="text-sm font-semibold cursor-pointer">{name}</p>
      </Link>
      <p className="text-md font-semibold text-indigo-600">{`$${
        Math.round(price * 10) / 10
      }`}</p>
      <p className={`${color} text-sm font-semibold`}>{` ${
        color === "text-green-600" ? "+" : ""
      }${Math.round(difference * 100) / 100}%`}</p>
    </div>
  );
};
