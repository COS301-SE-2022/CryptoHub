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
      value={{ user, logout, authorise, feedstate, refreshfeed }}
    >
      {children}
    </userContext.Provider>
  );
};

export default UserProvider;
