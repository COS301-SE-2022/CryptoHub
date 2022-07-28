import React from "react";
import { useState, useEffect } from "react";

const CoinCalculator = ({ id, name, state, arrow }) => {
  useEffect(() => {}, []);

  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <div className="flex flex-col mb-2">
        <div className="flex flex-row justify-between">
          <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-center text-gray-700">
            Convert
          </p>
        </div>
        <div className="flex flex-row">
          <p className="text-2xl font-bold mb-2 translate-y-1 ml-2 justify-between">
            {state}
          </p>
        </div>
      </div>
    </div>
  );
};

export default CoinCalculator;
