import React, { useState } from "react";
import { mockCoinInfo } from "../../mocks/mockCoinInfo";

const Carousel = () => {
  return (
    <div className="flex flew-row sm:justify-center flex-wrap">
      {mockCoinInfo.map((data, index) => {
        return (
          <CoinInfo
            name={data.name}
            price={data.price}
            difference={data.difference}
            color={data.difference < 0 ? "text-red-600" : "text-green-600"}
          />
        );
      })}
    </div>
  );
};

export default Carousel;

const CoinInfo = ({ name, price, difference, color }) => {
  return (
    <div className="flex flex-col p-4 m-1 items-center">
      <p className="text-sm font-semibold">{name}</p>
      <p className="text-md font-semibold text-indigo-600">{`$${price}`}</p>
      <p className={`${color} text-sm font-semibold`}>{` ${
        color === "text-green-600" ? "+" : ""
      }${difference}%`}</p>
    </div>
  );
};
