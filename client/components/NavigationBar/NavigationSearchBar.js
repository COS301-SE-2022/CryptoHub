import React from "react";

const NavigationSearchBar = () => {
  return (
    <input
      className="border rounded-md w-full px-2 py-1 mr-1 sm:mr-4 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
      type="text"
      placeholder="Search"
    />
  );
};

export default NavigationSearchBar;
