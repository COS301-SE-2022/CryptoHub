import React, { useState, useEffect } from "react";

const CoinInfo = ({ name, price }) => {
  const handleCurrencyConversion = (have, want, amount) => {
    const options = {
      method: "GET",
      headers: new Headers({
        "X-Api-Key": "3Fii6K+evhLZN2zl7lh8Lg==WxuC4gFF5eX27Ekz",
      }),
    };

    fetch(`https://api.api-ninjas.com/v1/convertcurrency?have=${have}&want=${want}&amount=${amount}`, options)
    .then((response) => response.json())
    .then((data) => {
      console.log(data);
    })
  };
  
  useEffect(() => {
    handleCurrencyConversion("ZAR","USD","1000");
  }, []);

  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <div className="flex flex-col mb-2">
        <div className="flex flex-row justify-between">
          <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-center text-gray-700">
            {name}
          </p>
          <div className="flex flex-row justify-between">
            <button className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right p-1 px-3 rounded-md bg-gray-100 hover:bg-indigo-300 transition">
              ZAR
            </button>
            <button className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right p-1 px-3 rounded-md bg-gray-100 hover:bg-indigo-300 transition">
              USD
            </button>
            <button className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right p-1 px-3 rounded-md bg-gray-100 hover:bg-indigo-300 transition">
              EUR
            </button>
          </div>
        </div>

        <div className="flex flex-row">
          <p className="text-6xl font-bold mb-2 translate-y-1 ml-2 justify-between">
            {price}
          </p>
          <p className="text-sm font-semibold ml-1 translate-y-3 text-gray-400 text-left items-center">
            USD
          </p>
        </div>
      </div>
    </div>
  );
};

export default CoinInfo;
