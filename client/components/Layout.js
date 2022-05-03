import React from "react";
import NavigationBar from "./NavigationBar/NavigationBar";
import styles from "../styles/Layout.module.css";

const Layout = ({ children }) => {
  return (
    <div className={styles.container}>
      <NavigationBar />
      <main className={styles.main}>{children}</main>
    </div>
  );
};

export default Layout;
