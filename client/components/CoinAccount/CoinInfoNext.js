import React from "react";
import { useState, useEffect } from "react";
import { HeartIcon, ChatIcon } from "@heroicons/react/outline";

const CoinInfoNext = ({ name, state, arrow }) => {
  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <div className="flex flex-col mb-2">
        <div className="flex flex-row justify-between">
          <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-center">
            {name}
          </p>
          <div className="flex flex-row justify-between">
            <button className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right bg-gray-100 px-3 p-1 rounded-md hover:bg-indigo-300 transition">
              1 day
            </button>
            <button className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right bg-gray-100 px-3 p-1 rounded-md hover:bg-indigo-300 transition">
              1 week
            </button>
            <button className="text-sm font-semibold mb-2 translate-y-1 ml-1 text-right bg-gray-100 px-3 p-1 rounded-md hover:bg-indigo-300 transition">
              1 month
            </button>
          </div>
        </div>
        <div className="flex flex-row">
          <p className="text-2xl font-bold mb-2 translate-y-1 ml-2 justify-between">
            {state}
          </p>
          {arrow == "up" ? (
            <div class="h-0 w-4.5 border-x-8 border-x-transparent translate-y-5 translate-x-2 border-b-[15px] border-b-green-600"></div>
          ) : (
            <div class="h-0 w-4.5 border-x-8 border-x-transparent translate-y-5 translate-x-2 border-b-[15px] border-b-red-600 rotate-180"></div>
          )}
        </div>
      </div>
    </div>
  );
};

export default CoinInfoNext;
