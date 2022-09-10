import { createContext, useState, useEffect } from "react";
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

export const userContext = createContext({
  username: "",
  auth: false,
  id: 0,
  token: "",
  admin: false,
});

const UserProvider = ({ children }) => {
  const router = useRouter();
  const [url, setUrl] = useState(
    !process.env.NODE_ENV || process.env.NODE_ENV === "development"
      ? //"http://176.58.110.152:7215"
        "http://localhost:7215"
      : "https://seashell-app-d57zw.ondigitalocean.app"
  );
  const [user, setUser] = useState({
    username: "",
    auth: false,
    id: 0,
    token: "",
    admin: false,
  });

  const [feedstate, setFeedstate] = useState(false);
  const [show, setShow] = useState(false);
  const [alertText, setAlertText] = useState("");
  const [profilePicture, setProfilePicture] = useState(null);

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
      admin: false,
    });
    router.push("/");
  };

  const alert = (text) => {
    setAlertText(text);
    setShow(true);
  };

  const closeAlert = () => {
    setShow(false);
  };

  const authorise = (token) => {
    let user = parseJwt(token);

    if (user.roles == "Super") {
      setUser({
        username: user.username,
        auth: true,
        id: user.id,
        token: token,
        admin: true,
      });
      router.push("/");
    } else {
      setUser({
        username: user.username,
        auth: true,
        id: user.id,
        token: token,
        admin: false,
      });
      router.push("/");
    }
  };

  const refreshfeed = () => {
    setFeedstate(!feedstate);
  };

  useEffect(() => {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === "development") {
      //setUrl("http://176.58.110.152:7215");
      setUrl("http://localhost:7215");
    } else {
      setUrl("https://seashell-app-d57zw.ondigitalocean.app");
    }
  });

  return (
    <userContext.Provider
      value={{
        user,
        logout,
        authorise,
        feedstate,
        refreshfeed,
        app,
        alert,
        show,
        alertText,
        closeAlert,
        profilePicture,
        setProfilePicture,
        url,
      }}
    >
      {children}
    </userContext.Provider>
  );
};

export default UserProvider;
