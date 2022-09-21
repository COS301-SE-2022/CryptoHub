import react, { useState, CSSProperties } from "react";
import ScaleLoader from "react-spinners/ClipLoader";

const Loader = () => {
  return (
    <div class="flex justify-center items-center">
      <ScaleLoader color="#717171" />
    </div>
  );
};

export default Loader;
