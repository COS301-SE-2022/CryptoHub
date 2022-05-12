import React from "react";

const SuggestedAccount = ({ name, hidefollow }) => {
  return (
    <div className="flex flex-row p-2 w-full justify-between bg-gray-100 mb-2 rounded-md">
      <div className="flex flex-row">
        <div className="w-6 h-6 bg-black rounded-3xl"></div>
        <p className="text-sm font-semibold translate-y-1 ml-2">{name}</p>
      </div>
      {hidefollow ? null : (
        <button>
          <p className="text-sm font-bold text-indigo-600 mr-2">Follow</p>
        </button>
      )}
    </div>
  );
};

export default SuggestedAccount;
