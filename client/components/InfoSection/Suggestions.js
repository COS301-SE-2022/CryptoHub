import React, { useState, useEffect, useContext } from "react";
import SuggestedAccount from "./SuggestedAccount";
import { userContext } from "../../auth/auth";

const Suggestions = () => {
  const { user } = useContext(userContext);
  const [accounts, setAccounts] = useState([]);
  const [followers, setFollowers] = useState([]);
  const [suggestedAccounts, setSuggestedAccounts] = useState([]);
  const [refresh, setRefresh] = useState(false);

  const handleSuggestedUser = () => {
    const options = {
      method: "GET",
    };

    fetch(`http://localhost:7215/api/User/SuggestedUsers/${user.id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        setFollowing(data);
        setSuggestedAccounts(data);
        console.log(suggestedAccounts);
      })
      .catch((error) => {
        setError(true);
        setLoading(false);
      });
  };

  return (
    <div>
      <p className="text-md font-bold text-indigo-600 mb-2 overflow-auto">
        Suggestions
      </p>
      {suggestedAccounts.length == 0 ? (
        <p className="text-sm text-gray-500">No suggestions</p>
      ) : (
        suggestedAccounts.map((data) => {
          return <SuggestedAccount id={data.userId} name={data.username} />;
        })
      )}
    </div>
  );
};

export default Suggestions;
