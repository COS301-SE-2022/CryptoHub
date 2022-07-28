import { Fragment } from "react";
import { Menu, Transition } from "@headlessui/react";
import { ChevronDownIcon } from "@heroicons/react/solid";
import React, { useState, useEffect, useContext } from "react";
import { useRouter } from "next/router";
import { userContext } from "../../auth/auth";

const Rating = () => {
  const router = useRouter();
  const [rating, setRating] = useState(0);
  const { id } = router.query;
  const { user } = useContext(userContext);

  const handleRateCoin = () => {
    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: user.id,
        coinId: id,
        rating: rating,
      }),
    };

    console.log(user.id);
    console.log(id);
    console.log(rating);

    fetch(
      `http://localhost:7215/api/Coin/RateCoin/${user.id}/${id}/${rating}`,
      options
    )
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

  function classNames(...classes) {
    return classes.filter(Boolean).join(" ");
  }

  return (
    <Menu as="div" className="relative inline-block text-left z-50 w-full">
      <div>
        <Menu.Button className=" inline-flex justify-center w-full rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-100 focus:ring-indigo-500">
          Rate coin
          <ChevronDownIcon className="-mr-1 ml-2 h-5 w-5" aria-hidden="true" />
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
        <Menu.Items className="origin-top-right absolute right-0 mt-2 w-10 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none ">
          <div className="py-1">
            <Menu.Item>
              {({ active }) => (
                <a
                  onClick={(e) => {
                    setRating(1);
                    handleRateCoin();
                  }}
                  className={classNames(
                    active ? "bg-gray-100 text-gray-900" : "text-gray-700",
                    "block px-4 py-2 text-sm"
                  )}
                >
                  1
                </a>
              )}
            </Menu.Item>
            <Menu.Item>
              {({ active }) => (
                <a
                  onClick={(e) => {
                    setRating(2);
                    handleRateCoin();
                  }}
                  className={classNames(
                    active ? "bg-gray-100 text-gray-900" : "text-gray-700",
                    "block px-4 py-2 text-sm"
                  )}
                >
                  2
                </a>
              )}
            </Menu.Item>
            <Menu.Item>
              {({ active }) => (
                <a
                  onClick={(e) => {
                    setRating(3);
                    handleRateCoin();
                  }}
                  className={classNames(
                    active ? "bg-gray-100 text-gray-900" : "text-gray-700",
                    "block px-4 py-2 text-sm"
                  )}
                >
                  3
                </a>
              )}
            </Menu.Item>
            <Menu.Item>
              {({ active }) => (
                <a
                  onClick={(e) => {
                    setRating(4);
                    handleRateCoin();
                  }}
                  className={classNames(
                    active ? "bg-gray-100 text-gray-900" : "text-gray-700",
                    "block px-4 py-2 text-sm"
                  )}
                >
                  4
                </a>
              )}
            </Menu.Item>
            <Menu.Item>
              {({ active }) => (
                <a
                  onClick={(e) => {
                    setRating(5);
                    handleRateCoin();
                  }}
                  className={classNames(
                    active ? "bg-gray-100 text-gray-900" : "text-gray-700",
                    "block px-4 py-2 text-sm"
                  )}
                >
                  5
                </a>
              )}
            </Menu.Item>
          </div>
        </Menu.Items>
      </Transition>
    </Menu>
  );
};
export default Rating;
