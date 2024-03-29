import React, { useState, useEffect } from "react";
import Layout from "../../components/Layout";
import { useRouter } from "next/router";
import { useContext } from "react";
import { userContext } from "../../auth/auth";
import { getFirestore, serverTimestamp } from "@firebase/firestore";
import { collection, getDocs, addDoc } from "firebase/firestore";

function directMessges() {
  const router = useRouter();
  const { user, app, url } = useContext(userContext);
  const { id } = router.query;
  const [thisUser, setUser] = useState({});
  const [username, setUsername] = useState("");
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState([]);

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
    console.log("use effect for DMS");
    handleGetConnection();
  }, []);
  return <div>directMessges</div>;
}

export default directMessges;
