import Link from "next/link";
import React from "react";
import NavigationSearchBar from "./NavigationSearchBar";
import CreatePostButton from "./CreatePostButton";
import NavigationProfile from "./NavigationProfile";

const NavigationBar = () => {
  return (
    <nav className="fixed bg-white w-full h-16 flex flex-row justify-between items-center px-14 py-1 z-50">
      <h1>CryptoHub</h1>
      <div className="flex flex-row justify-between w-6/12 items-center">
        <NavigationSearchBar />
        <CreatePostButton />
      </div>
      <NavigationProfile />
    </nav>
  );
};

export default NavigationBar;
