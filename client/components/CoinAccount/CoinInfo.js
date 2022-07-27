import React, { useEffect, useState } from "react";

const CoinInfo = ({ name, price }) => {
  const [amount, setAmount] = useState(0);
  const [currauncyLabel, setCurrencyLabel] = useState("USD");
  const [amountInput, setAmountInput] = useState(0);

  const handleCurrencyConversion = (have, want, amount) => {
    const options = {
      method: "GET",
      headers: new Headers({
        "X-Api-Key": "3Fii6K+evhLZN2zl7lh8Lg==WxuC4gFF5eX27Ekz",
      }),
    };

    fetch(
      `https://api.api-ninjas.com/v1/convertcurrency?have=${have}&want=${want}&amount=${amount}`,
      options
    )
      .then((response) => response.json())
      .then((data) => {
        setCurrencyLabel(want);
        setAmount(data.new_amount);
      })
      .catch(() => {});
  };

  useEffect(() => {}, []);

  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <div className="flex flex-col mb-2">
        <div className="flex flex-row justify-between">
          <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-center text-gray-700">
            {name}
          </p>
          <div className="flex flex-row justify-between">
            {/* onClick={handleCurrencyConversion("USD","ZAR",amount)} */}
            <button
              className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right p-1 px-3 rounded-md bg-gray-100 hover:bg-indigo-300 transition"
              onClick={() => {
                handleCurrencyConversion("USD", "ZAR", price);
              }}
            >
              ZAR
            </button>
            <button
              className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right p-1 px-3 rounded-md bg-gray-100 hover:bg-indigo-300 transition"
              onClick={() => {
                handleCurrencyConversion("USD", "USD", price);
              }}
            >
              USD
            </button>
            <button
              className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right p-1 px-3 rounded-md bg-gray-100 hover:bg-indigo-300 transition"
              onClick={() => {
                handleCurrencyConversion("USD", "EUR", price);
              }}
            >
              EUR
            </button>
          </div>
        </div>

        <div className="flex flex-row">
          <p className="sm:text-6xl font-bold mb-2 translate-y-1 ml-2 justify-between">
            {amount == 0 ? price : amount}
          </p>
          <p className="text-sm font-semibold ml-1 translate-y-3 text-gray-400 text-left items-center">
            {currauncyLabel}
          </p>
        </div>
        <div
          className="bg-gray-200 sm:w-12/12 mt-10 mb-3"
          style={{ height: "1px" }}
        ></div>
        <div>
          <p className="text-xl font-semibold mt-2 mb-2 translate-y-1 ml-2 text-center text-gray-700">
            Calculate the price in your currency
          </p>
          <div className="flex flex-col sm:px-24 justify-center">
            <input
              className="border text-sm mb-3 mt-3 h-10 rounded-md w-full px-2 py-1 mr-1 sm:mr-4 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
              placeholder="Amount"
              onChange={(e) => setAmountInput(e.target.value)}
            />
            <p className="self-center">{amountInput}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CoinInfo;
