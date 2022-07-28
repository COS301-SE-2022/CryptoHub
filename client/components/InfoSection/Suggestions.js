import React, { useState, useEffect, useContext } from "react";
import SuggestedAccount from "./SuggestedAccount";
import { userContext } from "../../auth/auth";

const Suggestions = () => {
  const { user } = useContext(userContext);
  const [accounts, setAccounts] = useState([]);
  const [followers, setFollowers] = useState([]);
  const [suggestedAccounts, setSuggestedAccounts] = useState([]);
  const [refresh, setRefresh] = useState(false);

  const handleSuggestedUsers = () => {
    const options = {
      method: "GET",
      headers: new Headers({
        "X-Api-Key": "3Fii6K+evhLZN2zl7lh8Lg==WxuC4gFF5eX27Ekz",
      }),
    };

    fetch(
      `https://api.api-ninjas.com/v1/convertcurrency?have=${have}&want=${want}&amount=${amount}`,
      options
    )
      .then((response) => response.json())
      .then((data) => {
        setCurrencyLabel(want);
        setAmount(data.new_amount);
      })
      .catch(() => {});
  };

  return (
    <div>
      <p className="text-md font-bold text-indigo-600 mb-2 overflow-auto">
        Suggestions
      </p>
      {suggestedAccounts.length == 0 ? (
        <p className="text-sm text-gray-500">No suggestions</p>
      ) : (
        suggestedAccounts.map((data, index) => {
          return (
            <SuggestedAccount
              key={index}
              name={data.username}
              id={data.userId}
              suggestions={true}
            />
          );
        })
      )}
    </div>
  );
};

export default Suggestions;
