import React, { useState, useEffect, useContext } from "react";
import SuggestedAccount from "./SuggestedAccount";
import { userContext } from "../../auth/auth";

const Suggestions = () => {
  const { user } = useContext(userContext);
  const [accounts, setAccounts] = useState([]);
  const [followers, setFollowers] = useState([]);
  const [suggestedAccounts, setSuggestedAccounts] = useState([]);
  const [refresh, setRefresh] = useState(false);

  useEffect(() => {
    try {
      let suggested = accounts.filter((account) => {
        return !followers.find((acc) => {
          return acc.userId == account.userId;
        });
      });

      let final = suggested.filter((acc) => {
        return acc.userId != user.id;
      });

      setSuggestedAccounts(final.slice(0, 4));
    } catch {}
  }, [refresh]);

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
