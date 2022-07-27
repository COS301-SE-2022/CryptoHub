import React, { Fragment, useContext } from "react";
import { Menu, Transition } from "@headlessui/react";
import { userContext } from "../../auth/auth";
import { useRouter } from "next/router";

function classNames(...classes) {
  return classes.filter(Boolean).join(" ");
}

const NavigationProfile = () => {
  const router = useRouter();
  const { user, logout } = useContext(userContext);

  return (
    <Menu as="div" className="ml-1 sm:ml-3 relative">
      <div>
        <Menu.Button className=" flex text-sm rounded-full focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-indigo-400 focus:ring-white">
          <span className="sr-only">Open user menu</span>
          <span className="inline-block h-10 w-10 rounded-full overflow-hidden bg-gray-100">
            <svg
              className="h-full w-full text-gray-300"
              fill="currentColor"
              viewBox="0 0 24 24"
            >
              <path d="M24 20.993V24H0v-2.996A14.977 14.977 0 0112.004 15c4.904 0 9.26 2.354 11.996 5.993zM16.002 8.999a4 4 0 11-8 0 4 4 0 018 0z" />
            </svg>
          </span>
        </Menu.Button>
      </div>
      <Transition
        as={Fragment}
        enter="transition ease-out duration-100"
        enterFrom="transform opacity-0 scale-95"
        enterTo="transform opacity-100 scale-100"
        leave="transition ease-in duration-75"
        leaveFrom="transform opacity-100 scale-100"
        leaveTo="transform opacity-0 scale-95"
      >
        <Menu.Items className="origin-top-right absolute right-0 mt-2 w-48 rounded-md shadow-lg py-1 bg-white ring-1 ring-black ring-opacity-5 focus:outline-none">
          <Menu.Item>
            {({ active }) => (
              <>
                {user.admin ? (
                  <button
                    onClick={() => {
                      router.push("/admin");
                    }}
                    className={classNames(
                      active ? "bg-gray-100" : "",
                      "block px-4 py-2 text-sm text-gray-700 w-full"
                    )}
                  >
                    <p>Admin Dashboard</p>
                  </button>
                ) : null}
                <button
                  onClick={() => {
                    router.push("/profile");
                  }}
                  className={classNames(
                    active ? "bg-gray-100" : "",
                    "block px-4 py-2 text-sm text-gray-700 w-full"
                  )}
                >
                  Profile
                </button>
                <button
                  onClick={() => {
                    router.push("/");
                  }}
                  className={classNames(
                    active ? "bg-gray-100" : "",
                    "block px-4 py-2 text-sm text-gray-700 w-full"
                  )}
                >
                  Feed
                </button>

                <button
                  onClick={logout}
                  className={classNames(
                    active ? "bg-gray-100" : "",
                    "block px-4 py-2 text-sm text-gray-700 w-full"
                  )}
                >
                  Sign out
                </button>
              </>
            )}
          </Menu.Item>
        </Menu.Items>
      </Transition>
    </Menu>
  );
};

export default NavigationProfile;
