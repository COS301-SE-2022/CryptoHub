import { createContext, useState } from "react";
import { useRouter } from "next/router";

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
