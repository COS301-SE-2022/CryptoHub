import { createContext, useState } from "react";
import { useRouter } from "next/router";

export const userContext = createContext({ name: "", email: "", auth: false });

const UserProvider = ({ children }) => {
  const router = useRouter();
  const [user, setUser] = useState({ name: "", email: "", auth: false });

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
    setUser((user) => ({
      name: "",
      email: "",
      auth: false,
    }));
  };

  return (
    <userContext.Provider value={{ user, login, logout }}>
      {children}
    </userContext.Provider>
  );
};

export default UserProvider;
