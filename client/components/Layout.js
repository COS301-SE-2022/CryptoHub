import React, { useContext } from "react";
import NavigationBar from "./NavigationBar/NavigationBar";
import styles from "../styles/Layout.module.css";
import Toast from "./Toast/Toast";
import { userContext } from "../auth/auth";

const Layout = ({ children }) => {
  const { alert } = useContext(userContext);

  return (
    <div className={styles.container}>
      <NavigationBar />
      <main className={styles.main}>{children}</main>
      <Toast />
    </div>
  );
};

export default Layout;
