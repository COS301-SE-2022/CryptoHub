// import React from "react";
import React, { useState, useEffect } from "react";
import Layout from "../../components/Layout";
import { useRouter } from "next/router";
import { useContext } from "react";
import { userContext } from "../../auth/auth";
import { getFirestore, serverTimestamp } from "@firebase/firestore";
import { collection, getDocs, addDoc } from "firebase/firestore";

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

function directMessges() {
  return <div>directMessges</div>;
}

export default directMessges;
