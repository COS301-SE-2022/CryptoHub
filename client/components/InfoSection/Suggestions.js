import React from "react";
import SuggestedAccount from "./SuggestedAccount";
import { mockSuggestedAccounts } from "../../mocks/mockSuggestedAccounts";

const Suggestions = () => {
  return (
    <div>
      <p className="text-md font-bold text-indigo-600 mb-2 overflow-auto">
        Suggestions
      </p>
      {mockSuggestedAccounts.map((data, index) => {
        return <SuggestedAccount key={index} name={data.name} />;
      })}
    </div>
  );
};

export default Suggestions;
