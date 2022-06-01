import React from "react";
import { useState, useEffect } from "react";
import { HeartIcon, ChatIcon } from "@heroicons/react/outline";

const CoinInfoNext = ({ name, state, arrow }) => {
  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <div className="flex flex-col mb-2">
        <div className="flex flex-row justify-between">
          <p className="text-3xl font-semibold mb-2 translate-y-1 ml-2 text-center text-decoration-line: underline">
            {name}
          </p>
        </div>
        <div className="flex flex-row">
          <p className="text-3xl font-bold mb-2 translate-y-1 ml-2 justify-between">
            {state}
          </p>
          {arrow == "down" ? (
            <div class="h-0 w-4.5 border-x-8 border-x-transparent translate-y-3.5 translate-x-2 border-b-[15px] border-b-green-600"></div>
          ) : (
            <div class="h-0 w-4.5 border-x-8 border-x-transparent translate-y-3.5 translate-x-2 border-b-[15px] border-b-red-600 rotate-180"></div>
          )}
        </div>
      </div>
    </div>
  );
};

export default CoinInfoNext;
