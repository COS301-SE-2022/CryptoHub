import React, { useState, useEffect } from "react";
import Layout from "../../components/Layout";
import { useRouter } from "next/router";
import { useContext } from "react";
import { userContext } from "../../auth/auth";

const Messages = () => {
  const router = useRouter();
  const { user } = useContext(userContext);
  const { id } = router.query;
  const [thisUser, setUser] = useState({});
  const [username, setUsername] = useState("");
  const [message, setMessage] = useState("");

  const handleGetUser = () => {
    const options = {
      method: "GET",
    };

    fetch(`http://localhost:7215/api/User/GetUserById/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setUser(data);
        setUsername(data.username);
      })
      .catch((error) => {});
  };

  useEffect(() => {
    handleGetUser();
  }, []);

  return (
    <Layout>
      <div className="w-11/12 sm:w-7/12 bg-white p-4 rounded-md">
        <div>
          <h1>{username}</h1>
        </div>

        <div>
          <p className="font-semibold">messages</p>
        </div>

        <div>
          <p>bubbles here</p>
        </div>

        <div>
          <form
            onClick={(e) => {
              e.preventDefault();
              console.log({
                message: message,
                sender: user.id,
                receiver: id.toString(),
              });
            }}
          >
            <div className="flex flex-row">
              <input
                id="message"
                name="message"
                type="text"
                required
                autoFocus
                className="appearance-none rounded-xl relative block w-11/12 px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Type message here..."
                onChange={(e) => {
                  setMessage(e.target.value);
                }}
              />
              <button className="px-5">Send</button>
            </div>
          </form>
        </div>
      </div>
    </Layout>
  );
};

export default Messages;
