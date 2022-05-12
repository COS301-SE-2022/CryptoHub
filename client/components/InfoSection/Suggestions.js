import React, { useState, useEffect, useContext } from "react";
import SuggestedAccount from "./SuggestedAccount";
import { mockSuggestedAccounts } from "../../mocks/mockSuggestedAccounts";
import { userContext } from "../../auth/auth";

const Suggestions = () => {
  const { user } = useContext(userContext);
  const [accounts, setAccounts] = useState([]);
  const [followers, setFollowers] = useState([]);
  const [suggestedAccounts, setSuggestedAccounts] = useState([]);
  const [refresh, setRefresh] = useState(false);

  useEffect(() => {
    const options = {
      method: "GET",
    };

    fetch("http://localhost:8082/api/user/getallusers", options)
      .then((response) => response.json())
      .then((data) => {
        console.warn("all users", data);
        setAccounts(data);
        fetch(`http://localhost:8082/api/user/getfollowing/${user.id}`, options)
          .then((response) => response.json())
          .then((data) => {
            console.warn("following", data);
            setFollowers(data);
            setRefresh(true);
          })
          .catch((error) => {
            console.warn("Error", error);
          });
      })
      .catch((error) => {
        console.warn("Error", error);
      });
  }, []);

  useEffect(() => {
    let suggested = accounts.filter((account) => {
      return !followers.find((acc) => {
        console.warn(account);
        // console.warn(acc);
        return acc.userId == account.userId;
      });
    });

    let final = suggested.filter((acc) => {
      return acc.userId != user.id;
    });

    setSuggestedAccounts(final);
    console.warn("suggested", suggestedAccounts);
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
              name={data.userName}
              id={data.userId}
            />
          );
        })
      )}
    </div>
  );
};

export default Suggestions;
