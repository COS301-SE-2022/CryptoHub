import { createContext, useState } from "react";
import { useRouter } from "next/router";

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

  const logout = () => {
    setUser({
      username: "",
      auth: false,
      id: 0,
      token: "",
    });
    router.push("/");
  };

  const authorise = (username, id, token) => {
    setUser({ username: username, auth: true, id: id, token: token });
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
