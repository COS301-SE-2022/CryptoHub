import React from "react";
import Carousel from "./Carousel";
import News from "./News";
import Suggestions from "./Suggestions";

const InfoSection = () => {
  return (
    <div className="bg-white sm:w-6/12 m-4 p-4 rounded-lg sm:fixed right-10">
      <Carousel />
      <News />
      <Suggestions />
    </div>
  );
};

export default InfoSection;
