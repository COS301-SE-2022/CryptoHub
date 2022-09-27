import React, { useState, useEffect, useRef } from "react";
import Layout from "../../components/Layout";
import { useRouter } from "next/router";
import { useContext } from "react";
import { userContext } from "../../auth/auth";
import { getFirestore, serverTimestamp } from "@firebase/firestore";
import { collection, getDocs, addDoc } from "firebase/firestore";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const Messages = () => {
  const router = useRouter();
  const { user, app, url } = useContext(userContext);
  const { id } = router.query;
  const [thisUser, setUser] = useState({});
  const [username, setUsername] = useState("");
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState([]);
  const [connection, setConnection] = useState(null);
  const latestMessage = useRef(null);
  const messageRef = useRef();

  latestMessage.current = messages;

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
    //setMessages(final);
  };

  const handleGetUser = () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/User/GetUserById/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setUser(data);
        setUsername(data.username);
      })
      .catch((error) => {});
  };

  const handleGetMessages = async () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/Message/GetMessages/${user.id}/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        console.log("Data: ", data[0]);
        console.log("Messages: ", messages);
        setMessages(data);
      })
      .catch((error) => {});
  };

  const handleGetConnection = async () => {
    console.log("Getting connection");
    setConnection(
      new HubConnectionBuilder()
        .withUrl(`${url}/messagehub?userId=${user.id}`)
        .configureLogging(LogLevel.Information)
        .build()
    );
  };

  const handleRecievedMessage = () => {
    console.log("Recieved message");
    if (connection) {
      connection.on("RecieveMessage", (message) => {});
    }
  };

  const handleSendMessage = async (e) => {
    e.preventDefault();

    // await addDoc(messagesRef, {
    //   sender: user.id.toString(),
    //   receiver: id.toString(),
    //   message: message,
    //   timestamp: serverTimestamp(),
    // });
    // getMessages();
    // setMessage("");

    const msg = {
      userId: user.id.toString(),
      recieverId: id.toString(),
      content: message,
    };

    //msg as json string
    const msgString = JSON.stringify(msg);

    if (connection) {
      connection
        .invoke("SendMessage", msgString)
        .catch((err) => console.error(err));
    }
  };

  useEffect(() => {
    !user.auth && router.push("/");
    console.log("use effect for DMS");

    handleGetConnection();
    handleGetMessages();
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
          console.log("Recieved message: ", message);
          const msg = JSON.parse(message);
          console.log("recivedMSG", msg);

          msg.TimeDelivered = new Date(msg.TimeDelivered).getTime();

          const updatedMessages = [...latestMessage.current];
          updatedMessages.push(msg);
          console.log(updatedMessages);
          setMessages(updatedMessages);
          console.log(messages);
        });
      }
    }
  }, [connection]);

  useEffect(() => {
    if (messageRef && messageRef.current) {
      const { scrollHeight, clientHeight } = messageRef.current;
      console.log("scrollHeight: ", scrollHeight);
      console.log("clientHeight: ", clientHeight);
      messageRef.current.scrollTo({
        left: 0,
        top: scrollHeight,
        behavior: "smooth",
      });
    }
  }, [messages]);

  return (
    <Layout>
      <div
        ref={messageRef}
        className="fixed w-11/12 sm:w-8/12 bg-white p-4 rounded-md h-4/5 overflow-scroll scroll"
      >
        <div>
          <h1>{username}</h1>
        </div>

        <div>
          <p className="font-semibold">messages</p>
        </div>

        <div className="flex flex-col">
          {messages.map((message) => {
            if (
              message.userId == user.id.toString() &&
              message.recieverId == id.toString()
            ) {
              return (
                <SenderMessage
                  key={message.timeDelivered}
                  message={message.content}
                  sender={message.userId}
                  receiver={message.recieverId}
                  time={message.timeDelivered}
                />
              );
            } else if (
              message.userId == id.toString() &&
              message.recieverId == user.id.toString()
            ) {
              return (
                <ReceiverMessage
                  key={message.timeDelivered}
                  message={message.content}
                  sender={message.userId}
                  receiver={message.recieverId}
                  time={message.timeDelivered}
                />
              );
            } else {
              <p>{message}</p>;
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
