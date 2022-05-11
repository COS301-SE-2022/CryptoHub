import { createContext, useState } from "react";
import { useRouter } from "next/router";

export const userContext = createContext({ username: "", auth: false, id: 0 });

const UserProvider = ({ children }) => {
  const router = useRouter();
  const [user, setUser] = useState({ username: "", auth: false, id: 0 });

  const logout = () => {
    setUser({
      username: "",
      auth: false,
      id: 0
    });
  };

  const authorise = (username, id) => {
    setUser({ username: username, auth: true, id: id });
    console.warn("User id: ", id)
    router.push("/");
  };

  return (
    <userContext.Provider value={{ user, logout, authorise }}>
      {children}
    </userContext.Provider>
  );
};

export default UserProvider;
