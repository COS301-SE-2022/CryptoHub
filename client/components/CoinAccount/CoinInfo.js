import React from "react";
import { HeartIcon, ChatIcon } from "@heroicons/react/outline";

const CoinInfo = ({ name, price }) => {
  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <div className="flex flex-col items-center mb-2">
        <p className="text-sm font-semibold mb-2 translate-y-1 ml-2">{name}</p>
        <p className="text-3xl font-semibold mb-2 translate-y-1 ml-2">
          {price}
        </p>
      </div>
    </div>
  );
};

export default CoinInfo;
