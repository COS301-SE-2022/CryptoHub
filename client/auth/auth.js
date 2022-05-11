import { createContext, useState } from "react";
import { useRouter } from "next/router";

export const userContext = createContext({ auth: false });

const UserProvider = ({ children }) => {
  const router = useRouter();
  const [user, setUser] = useState({ auth: false });

  const formatEmail = (email) => {
    let formattedName = email.split("@")[0];
    return formattedName;
  };

  const login = (name) => {
    setUser((user) => ({
      name: formatEmail(name),
      email: name,
      auth: true,
    }));
    router.push("/");
  };

  const logout = () => {
    setUser({
      auth: false,
    });
  };

  const authorise = () => {
    setUser({ auth: true });
    router.push("/");
  };

  return (
    <userContext.Provider value={{ user, login, logout, authorise }}>
      {children}
    </userContext.Provider>
  );
};

export default UserProvider;
