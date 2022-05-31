import React, { useState, useEffect, useContext } from "react";
import Layout from "../components/Layout";
import Head from "next/head";
import { userContext } from "../auth/auth";
import Post from "../components/Posts/Post";
import { useRouter } from "next/router";
import { XIcon } from "@heroicons/react/outline";
import SuggestedAccount from "../components/InfoSection/SuggestedAccount";
import CoinInfo from "../components/CoinAccount/CoinInfo";

const Coin = () => {
  const { user } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [, setError] = useState(false);
  const [, setLoading] = useState(false);
  const router = useRouter();
  const [followers, setFollowers] = useState([]);
  const [following, setFollowing] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [showFollowingModal, setFollowingShowModal] = useState(false);

  return (
    <>
      <Head>
        <title>Coin Account</title>
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
            <div className="flex flex-row -translate-y-5">
              <button onClick={() => setShowModal(true)}>
                {" "}
                <span className="font-semibold" f>
                  {`${followers.length} `}
                </span>
                followers
              </button>
            </div>
          </div>
        </div>

        <div className="bg-gray-400 sm:w-6/12" style={{ height: "1px" }}></div>
        <div className="flex flex-col items-center w-full sm:w-6/12">
          <div>
            <p className="text-sm mt-4 text-gray-600">Coin Info</p>
          </div>
          <div className="w-full"></div>
          <CoinInfo name="Sino" price="$100" />
        </div>
      </Layout>
    </>
  );
};

export default Coin;
