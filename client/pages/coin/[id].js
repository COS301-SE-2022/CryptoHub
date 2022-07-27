import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import { useRouter } from "next/router";
import CoinInfo from "../../components/CoinAccount/CoinInfo";
import CoinInfoNext from "../../components/CoinAccount/CoinInfoNext";
import Layout from "../../components/Layout";
import { userContext } from "../../auth/auth";

const Coin = () => {
  const router = useRouter();
  const { id } = router.query;
  const { user } = useContext(userContext);
  const [coinData, setCoinData] = useState({});
  const [isFollowing, setIsFollowing] = useState(false);
  const [clicked, setClicked] = useState(false);

  const handleGetCoin = () => {
    const options = {
      method: "GET",
    };

    fetch(`https://api.coincap.io/v2/assets/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setCoinData(data.data);
      })
      .catch((error) => {});
  };

  const handleFollowCoin = () => {
    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: user.id,
        followerId: id,
      }),
    };

    fetch(`http://localhost:7215/api/Coin/FollowCoin/${user.id}/${id}`, options)
      .then((response) => {
        setClicked(true);
        setIsFollowing(true);
        response.json();
      })
      .then((data) => {
        setClicked(true);
        setIsFollowing(true);
      })
      .catch(() => {});
  };

  const checkFollowing = () => {
    fetch(`http://localhost:7215/api/Coin/GetCoinsFollowers/${id}`)
      .then((response) => response.json())
      .then((data) => {
        //console.warn("dataaaa", data);
        data.map((d) => {
          if (d.userId == user.id) {
            setIsFollowing(true);
          }
        });
      });
  };

  useEffect(() => {
    id == undefined && router.push("/");
  }, []);

  useEffect(() => {
    checkFollowing();
    handleGetCoin();
    const interval = setInterval(() => {
      handleGetCoin();
    }, 10000);
    return () => clearInterval(interval);
  }, []);

  return (
    <>
      <Head>
        <title>{coinData.name}</title>
      </Head>
      <Layout>
        <div className="flex flex-col sm:flex-row w-full sm:w-6/12 items-center mt-8">
          <div
            className="w-32 h-32 bg-black sm:mr-10 mb-5"
            style={{ borderRadius: "100%" }}
          ></div>
          <div className="flex flex-row">
            <p className="font-semibold text-center sm:text-left mr-4">
              {coinData.name}
            </p>{" "}
            {/* ==================================================================== */}
            <div className="flex flex-row">
              <p className="font-semibold text-center sm:text-left"></p>{" "}
              {console.warn("Is following: ", isFollowing)}
              {user.auth ? (
                isFollowing ? (
                  <>
                    <p className="text-sm ml-5 text-black bg-gray-400 rounded-md px-3 py-1">
                      Following
                    </p>
                  </>
                ) : (
                  <>
                    <button onClick={handleFollowCoin}>
                      <p className="text-sm text-white ml-5 bg-indigo-600 rounded-md px-3 py-1 hover:bg-indigo-500 transition -translate-x-5">
                        Follow
                      </p>
                    </button>
                  </>
                )
              ) : null}
              <br />
            </div>
            {/* ========================================================================= */}
            <br />
            <div className="flex flex-row -translate-y-10"></div>
          </div>
        </div>

        <div className="bg-gray-400 sm:w-6/12" style={{ height: "1px" }}></div>
        <div className="flex flex-col items-center w-10/12 sm:w-6/12">
          <div>
            <p className="text-sm mt-4 text-gray-600">Coin Info</p>
          </div>
          <div className="w-full"></div>
          <CoinInfo
            name="Price"
            price={Math.round(coinData.priceUsd * 100) / 100}
          />
          <CoinInfoNext
            id={id}
            name="State"
            state={`${Math.round(coinData.changePercent24Hr * 100) / 100}%`}
            arrow={coinData.changePercent24Hr < 0 ? "down" : "up"}
          />
        </div>
      </Layout>
    </>
  );
};

export default Coin;
