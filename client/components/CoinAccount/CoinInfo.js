import React from "react";
import { HeartIcon, ChatIcon } from "@heroicons/react/outline";

const CoinInfo = ({ name, price }) => {
  return (
    <div className="bg-white m-4 p-4 rounded-lg w-full">
      <div className="flex flex-col mb-2">
        <div className="flex flex-row justify-between">
          <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-center">
            {name}
          </p>
          <div className="flex flex-row justify-between">
            <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-right">
              Rand
            </p>
            <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-right">
              USD
            </p>
            <p className="text-xl font-semibold mb-2 translate-y-1 ml-2 text-right">
              UAE
            </p>
          </div>
        </div>

        <p className="text-3xl font-bold mb-2 translate-y-1 ml-2">{price}</p>
      </div>
    </div>
  );
};

export default CoinInfo;
