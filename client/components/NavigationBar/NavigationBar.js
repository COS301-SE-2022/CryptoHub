import Link from "next/link";
import React from "react";

const NavigationBar = () => {
  return (
    <nav className="w-full h-16 flex flex-row justify-between items-center px-14 py-1">
      <h1>CryptoHub</h1>
      <div>search and post</div>
      <div>profile</div>
    </nav>
  );
};

export default NavigationBar;
