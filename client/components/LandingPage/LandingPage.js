import React from "react";
import NavigationBar from "../NavigationBar/NavigationBar";
import Image from "next/image";
import { useRouter } from "next/router";

const LandingPage = () => {
  return (
    <>
      <NavigationBar />
      <MainContent />
    </>
  );
};

export default LandingPage;

const MainContent = () => {
  const router = useRouter();

  return (
    <>
      <div className="flex flex-col h-40 px-10 py-24 sm:p-24 sm:pt-36 items-center h-8/12 justify-between">
        <div className="flex flex-col items-center">
          <h1 className="sm:text-6xl text-4xl font-bold text-center text-gray-700">
            Welcome to <span className="text-indigo-600 ">CryptoHub</span>
          </h1>
          <p className=" text-lg sm:text-md text-gray-400 font-semibold mt-10 w-10/12 text-center">
            An interactive and social application that allows you to explore the
            world of cryptocurrencies and related news.
          </p>
        </div>
        <div className="w-8/12 rounded-md justify-center items-center sm:flex mt-12 text-center">
          <div className="inline-flex rounded-md shadow mb-5">
            <a
              href="/signup"
              className="inline-flex items-center justify-center px-5 py-3 border border-transparent text-base font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700"
            >
              Create an account
            </a>
          </div>
          <div className="sm:ml-3 inline-flex rounded-md shadow mb-5">
            <a
              href="/login"
              className="inline-flex items-center justify-center px-5 py-3 border border-transparent text-base font-medium rounded-md text-indigo-600 bg-white hover:bg-indigo-50"
            >
              Log in
            </a>
          </div>
        </div>

        <div className="mt-4 sm:mt-20 w-11/12 sm:w-12/12 justify-center flex">
          <Image
            src="https://firebasestorage.googleapis.com/v0/b/cryptohub-12abc.appspot.com/o/landing.png?alt=media&token=be95a74b-c81f-4d6a-8084-c74ea08d64f6"
            layout="intrinsic"
            width={300}
            height={600}
          />
          <div className="flex flex-col ml-2 mt-2">
            <div className="bg-indigo-300 h-30 sm:h-7 px-3 w-5/12 rounded-lg text-indigo-800 text-lg mb-3">
              to the moon 🚀
            </div>
            <button
              onClick={() => {
                router.push("/guest");
              }}
              className="bg-indigo-300 text-wrap hover:bg-indigo-400 h-30 text-left sm:h-7 px-3 rounded-lg text-indigo-800 text-lg"
            >
              not ready to sign up? see some posts here 💰
            </button>
          </div>
        </div>
      </div>
    </>
  );
};
