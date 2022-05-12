import React, { useState, useEffect, useContext } from "react";
import SuggestedAccount from "./SuggestedAccount";
import { mockSuggestedAccounts } from "../../mocks/mockSuggestedAccounts";
import { userContext } from "../../auth/auth";

const Suggestions = () => {
  const { user } = useContext(userContext);
  const [accounts, setAccounts] = useState([]);
  const [followers, setFollowers] = useState([]);
  const [suggestedAccounts, setSuggestedAccounts] = useState([]);

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

            let suggested = accounts.filter((account) => {
              return !followers.find((acc) => {
                console.warn(account);
                console.warn(acc);
                return acc.userId == account.userId;
              });
            });

            setSuggestedAccounts(suggested);
            console.warn("suggested", suggestedAccounts);
          })
          .catch((error) => {
            console.warn("Error", error);
          });
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
