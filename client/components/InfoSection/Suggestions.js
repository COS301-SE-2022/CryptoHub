import React, { useState, useEffect, useContext } from "react";
import SuggestedAccount from "./SuggestedAccount";
import { mockSuggestedAccounts } from "../../mocks/mockSuggestedAccounts";

const Suggestions = () => {
  const [accounts, setAccounts] = useState([]);

  useEffect(() => {
    const options = {
      method: "GET",
    };

    fetch("http://localhost:8082/api/user/getallusers", options)
      .then((response) => response.json())
      .then((data) => {
        console.warn(data);
        setAccounts(data);
      })
      .catch((error) => {
        console.warn("Error", error);
      });
  }, []);
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
