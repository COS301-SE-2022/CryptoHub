import React, { useState, useEffect } from "react";
import { mockCoinInfo } from "../../mocks/mockCoinInfo";
import Link from "next/link";

const Carousel = () => {
  const [data, setData] = useState([]);

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
  }, []);

  return (
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
            />
          );
        })
      )}
    </div>
  );
};

export default Carousel;

const CoinInfo = ({ name, price, difference, color }) => {
  return (
    <div className="flex flex-col p-4 m-1 items-center">
      <Link href="/coin">
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
