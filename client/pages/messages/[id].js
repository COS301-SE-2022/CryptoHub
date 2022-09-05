import React, { useState, useEffect } from "react";
import Layout from "../../components/Layout";
import Signals from "../../pages/messages/SignalRClient";
import { useRouter } from "next/router";
import { useContext } from "react";
import { userContext } from "../../auth/auth";
import { getFirestore, serverTimestamp } from "@firebase/firestore";
import { collection, getDocs, addDoc } from "firebase/firestore";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const Messages = () => {
  const router = useRouter();
  const { user, app } = useContext(userContext);
  const { id } = router.query;
  const [thisUser, setUser] = useState({});
  const [username, setUsername] = useState("");
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState([]);
  const [connection, setConnection] = useState(null);

  const db = getFirestore(app);
  const messagesRef = collection(db, "messages");

  const getMessages = async () => {
    const data = await getDocs(messagesRef);
    let final = data.docs
      .map((doc) => ({ ...doc.data(), id: doc.id }))
      .sort((a, b) => {
        return a.timestamp - b.timestamp;
      });
    console.warn("Final messages: ", messages);
    setMessages(final);
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

  const handleGetConnection = async () => {
    console.log("Getting connection");
    setConnection(
      new HubConnectionBuilder()
        .withUrl(`http://localhost:7215/messagehub?userId=${user.id}`)
        .configureLogging(LogLevel.Information)
        .build()
    );

    // connection.on("RecievedID", function (connectionID, id) {
    //   console.log("connection id: " + connectionID);
    //   connect.innerHTML += "<p>" + connectionID + "</p>";
    // });

    // if (connection != null) {
    //   console.log("Connection is not null");
    //   connection.start().then(function () {
    //     console.log("do this");
    //   });

    // connection.on("RecieveMessage", (connectionid, id) => {
    //   console.log(connectionid);
    // });
  };

  const handleOnRecieved = () => {
    console.log("Recieved message");
    if (connection != null) {
      connection.on("RecieveMessage", (message) => {
        console.log(message);
      });
    }
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
    setMessage("");
  };

  useEffect(() => {
    !user.auth && router.push("/");
    console.log("use effect for DMS");

    handleGetConnection();
    //handleGetUser();
    //getMessages();

    // const interval = setInterval(() => {
    //   getMessages();
    // }, 3000);
    // return () => clearInterval(interval);
  }, []);

  useEffect(() => {
    if (connection) {
      if (connection.state === "Disconnected") {
        connection.start().then(function () {
          console.log("do this");
        });

        connection.on("RecievedMessage", (message) => {
          console.log(message);
        });
      }
    }
  }, [connection]);

  return (
    <Layout>
      <div className="fixed w-11/12 sm:w-8/12 bg-white p-4 rounded-md h-4/5 overflow-scroll scroll">
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
                  time={message.time}
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
                  time={message.time}
                />
              );
            }
          })}
        </div>

        <div className="fixed bottom-6 z-20 w-7/12">
          <form onSubmit={handleSendMessage}>
            <div className="flex flex-row sm:w-full">
              <input
                id="message"
                name="message"
                type="text"
                required
                autoFocus
                className="appearance-none rounded-xl relative block sm:w-11/12 px-2 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Type message here..."
                onChange={(e) => {
                  setMessage(e.target.value);
                }}
              />
              <button className="ml-6 bg-indigo-600 px-4 text-white rounded-xl hover:bg-indigo-500 transition">
                Send
              </button>
            </div>
          </form>
        </div>
      </div>
    </Layout>
  );
};

export default Messages;

const SenderMessage = ({ message, sender, receiver, time }) => {
  const router = useRouter();
  const { user } = useContext(userContext);
  const { id } = router.query;

  return (
    <div className="bg-indigo-300 text-right m-3 rounded-xl px-3 py-1 min:w-4/12 self-end">
      <p>{message}</p>
      {/* <p>{time}</p> */}
    </div>
  );
};

const ReceiverMessage = ({ message, sender, receiver, time }) => {
  const router = useRouter();
  const { user } = useContext(userContext);
  const { id } = router.query;
  return (
    <div className="bg-gray-200  m-3 rounded-xl px-3 py-1 min:w-4/12 self-start">
      <p>{message}</p>
      {/* <p>{time}</p> */}
    </div>
  );
};
