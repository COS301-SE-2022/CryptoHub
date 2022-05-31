import React from "react";
import { HeartIcon, ChatIcon } from "@heroicons/react/outline";

const CoinInfo = ({ name, price }) => {
  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <div className="flex flew-row items-center mb-2">
        <div className="w-8 h-8 bg-black rounded-3xl"></div>
        <p className="text-sm font-semibold mb-2 translate-y-1 ml-2">
          Hello there
        </p>
      </div>
    </div>
  );
};

export default CoinInfo;
