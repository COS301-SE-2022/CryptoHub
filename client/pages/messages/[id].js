import React, { useState, useEffect } from "react";
import Layout from "../../components/Layout";
import { useRouter } from "next/router";
import { useContext } from "react";
import { userContext } from "../../auth/auth";
import { getFirestore, serverTimestamp } from "@firebase/firestore";
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
      sender: user.id.toString(),
      receiver: id.toString(),
      message: message,
      timestamp: serverTimestamp(),
    });
    getMessages();
  };

  useEffect(() => {
    console.warn("Sender", user.id);
    console.warn("Receiver", id.toString());
    handleGetUser();
    getMessages();

    const interval = setInterval(() => {
      getMessages();
    }, 1000);
    return () => clearInterval(interval);
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

        <div className="flex flex-col">
          {messages.map((message) => {
            if (
              message.sender == user.id.toString() &&
              message.receiver == id.toString()
            ) {
              return (
                <SenderMessage
                  message={message.message}
                  sender={message.sender}
                  receiver={message.receiver}
                />
              );
            } else if (
              message.sender == id.toString() &&
              message.receiver == user.id.toString()
            ) {
              return (
                <ReceiverMessage
                  message={message.message}
                  sender={message.sender}
                  receiver={message.receiver}
                />
              );
            }
          })}
        </div>

        <div>
          <form onSubmit={handleSendMessage}>
            <div className="flex flex-row w-full">
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

const SenderMessage = ({ message, sender, receiver }) => {
  const router = useRouter();
  const { user } = useContext(userContext);
  const { id } = router.query;

  return (
    <div className="bg-indigo-300 text-right m-3 rounded-xl px-3 py-1 min:w-4/12 self-end">
      {message}
    </div>
  );
};

const ReceiverMessage = ({ message, sender, receiver }) => {
  const router = useRouter();
  const { user } = useContext(userContext);
  const { id } = router.query;
  return (
    <div className="bg-gray-200  m-3 rounded-xl px-3 py-1 min:w-4/12 self-start">
      {message}
    </div>
  );
};
