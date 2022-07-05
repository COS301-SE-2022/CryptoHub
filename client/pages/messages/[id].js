import React, { useState, useEffect } from "react";
import Layout from "../../components/Layout";
import { useRouter } from "next/router";
import { useContext } from "react";
import { userContext } from "../../auth/auth";
import { getFirestore } from "@firebase/firestore";
import { collection, getDocs, addDoc } from "firebase/firestore";

const Messages = () => {
  const router = useRouter();
  const { user, app } = useContext(userContext);
  const { id } = router.query;
  const [thisUser, setUser] = useState({});
  const [username, setUsername] = useState("");
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState([]);

  const db = getFirestore(app);
  const messagesRef = collection(db, "messages");

  const getMessages = async () => {
    const data = await getDocs(messagesRef);
    setMessages(data.docs.map((doc) => ({ ...doc.data(), id: doc.id })));
  };

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

  const handleSendMessage = async (e) => {
    e.preventDefault();

    await addDoc(messagesRef, {
      sender: user.id,
      receiver: id.toString(),
      message: message,
    });

    getMessages();
  };

  useEffect(() => {
    handleGetUser();
    getMessages();
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
          {messages.map((message) => {
            if (message.sender == user.id) {
              return <SenderMessage message={message.message} />;
            } else {
              return <ReceiverMessage message={message.message} />;
            }
          })}
        </div>

        <div>
          <form onSubmit={handleSendMessage}>
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

const SenderMessage = ({ message }) => {
  return <div className="bg-red-100 rounded-md p-1 px-3 my-4">{message}</div>;
};

const ReceiverMessage = ({ message }) => {
  return (
    <div className="bg-blue-100 rounded-md p-1 px-3 my-4 text-right">
      {message}
    </div>
  );
};
