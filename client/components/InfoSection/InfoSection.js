import React, { useContext } from "react";
import Carousel from "./Carousel";
import News from "./News";
import Suggestions from "./Suggestions";
import { userContext } from "../../auth/auth";
import { useRouter } from "next/router";

const InfoSection = () => {
  const { user } = useContext(userContext);
  const router = useRouter();

  return (
    <div className="bg-white sm:w-6/12 m-4 p-4 rounded-lg sm:fixed right-10 overflow-auto max-h-[40rem]">
      {user.auth ? null : (
        <div>
          <p className="font-bold text-center">Create a free account now</p>
          <div className="rounded-md justify-center items-center sm:flex mt-4 text-center">
            <div className="inline-flex rounded-md shadow mb-5">
              <a
                href="/signup"
                className="inline-flex items-center justify-center px-5 py-3 border border-transparent text-base font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700"
              >
                Sign up
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
        </div>
      )}
      <Carousel />
      <News />
      {user.auth ? <Suggestions /> : null}
    </div>
  );
};

export default InfoSection;
