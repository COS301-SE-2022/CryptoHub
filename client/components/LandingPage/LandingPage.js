import React from "react";
import Carousel from "../Carousel";

const LandingPage = () => {
  return (
    <div className="flex flex-col h-40 p-24 items-center h-8/12 justify-between">
      <div className="flex flex-col items-center">
        <h1 className="text-5xl">
          Welcome to{" "}
          <span className="text-indigo-600 font-semibold">CrytoHub</span>
        </h1>
        <p className="text-md text-gray-600 mt-4">
          Explore the world of cryptocurrencies
        </p>
      </div>
      <div className="bg-indigo-600 w-8/12 rounded-md justify-center flex mt-16">
        <Carousel />
      </div>
    </div>
  );
};

export default LandingPage;
