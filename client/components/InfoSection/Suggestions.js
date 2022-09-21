import React, { useState, useEffect, useContext } from "react";
import SuggestedAccount from "./SuggestedAccount";
import { userContext } from "../../auth/auth";
import Loader from "../Loader";

const Suggestions = () => {
  const { user, url } = useContext(userContext);
  const [suggestedAccounts, setSuggestedAccounts] = useState([]);
  const [loading, setLoading] = useState(false);
  const [, setError] = useState(false);

  const handleSuggestedUser = () => {
    const options = {
      method: "GET",
    };
    setLoading(true);
    fetch(`${url}/api/User/SuggestedUsers/${user.id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setSuggestedAccounts(data);
        setLoading(false);
        setFollowing(data);
      })
      .catch((error) => {
        setError(true);
        setLoading(false);
      });
  };

  useEffect(() => {
    handleSuggestedUser();
  }, []);

  return (
    <div>
      <p className="text-md font-bold text-indigo-600 mb-2 overflow-auto">
        Suggestions
      </p>
      {loading ? (
        <Loader />
      ) : suggestedAccounts.length == 0 ? (
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
