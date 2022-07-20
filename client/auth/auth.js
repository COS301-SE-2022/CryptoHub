import { createContext, useState } from "react";
import { useRouter } from "next/router";
import { initializeApp } from "firebase/app";

const firebaseConfig = {
  apiKey: "AIzaSyAs6bxiM71e6LE4E8-pGzUNL3OGeyE8iTA",
  authDomain: "cryptohub-12abc.firebaseapp.com",
  projectId: "cryptohub-12abc",
  storageBucket: "cryptohub-12abc.appspot.com",
  messagingSenderId: "727091318041",
  appId: "1:727091318041:web:9d918df3015cc4ffb30988",
  measurementId: "G-49KJ5M594Q",
};

// Initialize Firebase

export const userContext = createContext({
  username: "",
  auth: false,
  id: 0,
  token: "",
});

const UserProvider = ({ children }) => {
  const router = useRouter();
  const [user, setUser] = useState({
    username: "",
    auth: false,
    id: 0,
    token: "",
  });
  const [feedstate, setFeedstate] = useState(false);

  const app = initializeApp(firebaseConfig);

  const parseJwt = (token) => {
    var base64Url = token.split(".")[1];
    var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    var jsonPayload = decodeURIComponent(
      window
        .atob(base64)
        .split("")
        .map(function (c) {
          return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join("")
    );

    return JSON.parse(jsonPayload);
  };

  const logout = () => {
    setUser({
      username: "",
      auth: false,
      id: 0,
      token: "",
    });
    router.push("/");
  };

  const authorise = (token) => {
    let user = parseJwt(token);
    setUser({ username: user.username, auth: true, id: user.id, token: token });
    router.push("/");
  };

  const refreshfeed = () => {
    setFeedstate(!feedstate);
  };

  return (
    <userContext.Provider
      value={{ user, logout, authorise, feedstate, refreshfeed, app }}
    >
      {children}
    </userContext.Provider>
  );
};

export default UserProvider;
