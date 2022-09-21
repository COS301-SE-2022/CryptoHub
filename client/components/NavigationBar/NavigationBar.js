import React, { useContext } from "react";
import NavigationSearchBar from "./NavigationSearchBar";
import CreatePostButton from "./CreatePostButton";
import NavigationProfile from "./NavigationProfile";
import { useRouter } from "next/router";
import { userContext } from "../../auth/auth";

const NavigationBar = () => {
  const router = useRouter();
  const { user } = useContext(userContext);

  return (
    <nav className="fixed bg-white w-full h-16 flex flex-row justify-between items-center px-3 sm:px-14 py-1 z-50 shadow-sm">
      <button
        onClick={() => {
          router.push("/");
        }}
        className="hidden sm:flex text-3xl font-medium"
      >
        CryptoHub
      </button>
      <div className="flex flex-row justify-between w-full sm:w-6/12 items-center">
        <NavigationSearchBar />
        {user.auth ? (
          <>
            <CreatePostButton />
            <button
              onClick={() => {
                router.push("/explore");
              }}
              className="text-indigo-600 px-4 p-1 rounded-md font-semibold"
            >
              Explore
            </button>
          </>
        ) : null}
      </div>
      {user.auth ? (
        <NavigationProfile />
      ) : (
        <button
          className="font-semibold ml-10"
          onClick={() => {
            router.push("/AboutPage");
          }}
        >
          About
        </button>
      )}
    </nav>
  );
};

export default NavigationBar;
