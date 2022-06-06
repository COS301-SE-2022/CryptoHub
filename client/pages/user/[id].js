import { useRouter } from "next/router";
import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import Layout from "../../components/Layout";
import { userContext } from "../../auth/auth";

const User = () => {
  const router = useRouter();
  const { id } = router.query;
  //   const { user } = useContext(userContext);
  const [user, setUser] = useState({});

  const handleGetUser = () => {
    const options = {
      method: "GET",
    };

    fetch(`http://localhost:7215/api/User/GetUserById/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setUser(data);
        console.warn(data);
      })
      .catch((error) => {});
  };

  useEffect(() => {
    handleGetUser();
  }, []);

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout>
        <div className="flex flex-col sm:flex-row w-full sm:w-6/12 items-center mt-8">
          <div
            className="w-32 h-32 bg-black sm:mr-10 mb-5"
            style={{ borderRadius: "100%" }}
          ></div>
          <div className="flex flex-col">
            <p className="font-semibold text-center sm:text-left">
              {user.username}
            </p>{" "}
            <br />
            {/* <div className="flex flex-row -translate-y-5">
              <button
                className="mr-3"
                onClick={() => setFollowingShowModal(true)}
              >
                <span className="font-semibold">{`${following.length} `}</span>{" "}
                following
              </button>
              <button onClick={() => setShowModal(true)}>
                {" "}
                <span className="font-semibold" f>
                  {`${followers.length} `}
                </span>
                followers
              </button>
            </div> */}
          </div>
        </div>
      </Layout>
    </>
  );
};

export default User;
