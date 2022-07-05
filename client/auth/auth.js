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
const app = initializeApp(firebaseConfig);

export const userContext = createContext({ username: "", auth: false, id: 0 });

const UserProvider = ({ children }) => {
  const router = useRouter();
  const [user, setUser] = useState({ username: "", auth: false, id: 0 });
  const [feedstate, setFeedstate] = useState(false);

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
      value={{ user, logout, authorise, feedstate, refreshfeed }}
    >
      {children}
    </userContext.Provider>
  );
};

export default UserProvider;
