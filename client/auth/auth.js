import { createContext, useContext, useState } from "react";

export const userContext = createContext({ name: "", auth: true });

const UserProvider = ({ children }) => {
  const [user, setUser] = useState({ name: "", auth: true });

  const login = (name) => {
    setUser((user) => ({
      name: name,
      auth: true,
    }));
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
