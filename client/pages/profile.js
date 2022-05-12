import React, { useState, useEffect, useContext } from "react";
import Layout from "../components/Layout";
import Head from "next/head";
import { userContext } from "../auth/auth";

const Profile = () => {
  const { user } = useContext(userContext);

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout>
        <div className="flex flex-col sm:flex-row w-8/12 items-center mt-8">
          <div
            className="w-20 h-20 bg-black sm:mr-10 mb-5"
            style={{ borderRadius: "50px" }}
          ></div>
          <div className="flex flex-col">
            <p className="font-semibold text-center sm:text-left">
              {user.username}
            </p>{" "}
            <br />
            <div className="flex flex-row -translate-y-5">
              <p className="mr-3">
                <span className="font-semibold">10 </span> following
              </p>
              <p>
                {" "}
                <span className="font-semibold" f>
                  4{" "}
                </span>
                followers
              </p>
            </div>
          </div>
        </div>
      </Layout>
    </>
  );
};

export default Profile;
