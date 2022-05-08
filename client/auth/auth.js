import { createContext, useState } from "react";
import { useRouter } from "next/router";

export const userContext = createContext({ name: "", auth: false });

const UserProvider = ({ children }) => {
  const router = useRouter();
  const [user, setUser] = useState({ name: "", auth: false });

  const login = (name) => {
    setUser((user) => ({
      name: name,
      auth: true,
    }));
    router.push("/");
  };

  const logout = () => {
    setUser((user) => ({
      name: "",
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
