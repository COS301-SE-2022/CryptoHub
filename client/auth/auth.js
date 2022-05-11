import { createContext, useState } from "react";
import { useRouter } from "next/router";

export const userContext = createContext({ auth: false, id: 0 });

const UserProvider = ({ children }) => {
  const router = useRouter();
  const [user, setUser] = useState({ auth: false, id: 0 });

  const logout = () => {
    setUser({
      auth: false,
      id: 0
    });
  };

  const authorise = (id) => {
    setUser({ auth: true, id: id });
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
