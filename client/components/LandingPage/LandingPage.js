import React from "react";
import NavigationBar from "../NavigationBar/NavigationBar";
import Image from "next/image";
import PhoneImage from "../../public/images/landing.png";

const LandingPage = () => {
  return (
    <>
      <NavigationBar />
      <div className="flex flex-col h-40 px-10 py-24 sm:p-24 sm:pt-36 items-center h-8/12 justify-between">
        <div className="flex flex-col items-center">
          <h1 className="sm:text-6xl text-4xl font-bold text-center">
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
        <div className="mt-20 w-11/12 sm:w-8/12 justify-center flex">
          <Image src={PhoneImage} />
          <div className="bg-indigo-300 h-16 sm:h-7 px-2 rounded-lg text-indigo-800 text-lg">
            to the moon 🚀
          </div>
        </div>
      </div>
    </>
  );
};

export default LandingPage;
