import { useContext } from "react";
import Layout from "../components/Layout";
import LandingPage from "../components/LandingPage/LandingPage";
import { userContext } from "../auth/auth";
import Feed from "../components/Feed/Feed";
import Head from "next/head";
import AdminFeed from "../components/Admin/AdminFeed";

const Admin = () => {
  const { user } = useContext(userContext);

  return (
    <>
      <Head>
        <title>CryptoHub - Admin</title>
      </Head>
      {user.auth && user.admin ? (
        <Layout>
          <AdminFeed />
        </Layout>
      ) : (
        <LandingPage />
      )}
    </>
  );
};

export default Admin;
