import { createContext, useState } from "react";
import { useRouter } from "next/router";
import { initializeApp } from "firebase/app";

const firebaseConfig = {
  apiKey: `${process.env.FIREBASE_APIKEY}`,
  authDomain: `${process.env.FIREBASE_AUTHDOMAIN}`,
  projectId: `${process.env.FIREBASE_PROJECTID}`,
  storageBucket: `${process.env.FIREBASE_STORAGEBUCKET}`,
  messagingSenderId: `${process.env.FIREBASE_MESSAGINGSENDERID}`,
  appId: `${process.env.FIREBASE_APPID}`,
  measurementId: `${process.env.FIREBASE_MEASUREMENTID}`,
};

// Initialize Firebase

export const userContext = createContext({ username: "", auth: false, id: 0 });

const UserProvider = ({ children }) => {
  const router = useRouter();
  const [user, setUser] = useState({ username: "", auth: false, id: 0 });
  const [feedstate, setFeedstate] = useState(false);

  const app = initializeApp(firebaseConfig);

  const logout = () => {
    setUser({
      username: "",
      auth: false,
      id: 0,
    });
    router.push("/");
  };

  const authorise = (username, id) => {
    setUser({ username: username, auth: true, id: id });
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
