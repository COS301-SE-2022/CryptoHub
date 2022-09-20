import { Fragment } from "react";
import { Menu, Transition } from "@headlessui/react";
import React, { useState, useEffect, useContext } from "react";
import { userContext } from "../../auth/auth";

function classNames(...classes) {
  return classes.filter(Boolean).join(" ");
}

export default function TagDropdown() {
  const [tags, setTags] = useState([]);
  const [selectedTags, setSelectedTags] = useState([]);
  const { url } = useContext(userContext);

  const GetPostTags = () => {
    const options = {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    };

    fetch(`${url}/api/Tag/GetTags`, options)
      .then((response) => response.json())
      .then((data) => {
        setTags(data);
      })
      .catch(() => {});
  };

  useEffect(() => {
    GetPostTags();
  }, []);

  const addTag = (tag) => {
    // setSelectedTags([...selectedTags, tag]);
  };

  return (
    <div>
      <Menu as="div" className="relative inline-block text-left">
        <div>
          <Menu.Button className="inline-flex w-full justify-center rounded-md border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 focus:ring-offset-gray-100">
            Add Tags +
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
          <Menu.Items className="absolute left-0 z-10 mt-2 w-56 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none max-h-[16em] overflow-scroll">
            <div className="py-1">
              {tags.map((tag) => {
                return (
                  <Menu.Item>
                    {({ active }) => (
                      <button
                        onClick={(e) => {
                          e.preventDefault();
                          setSelectedTags([...selectedTags, tag.content]);
                        }}
                        className={classNames(
                          active
                            ? "bg-gray-100 text-gray-900"
                            : "text-gray-700",
                          "block px-4 py-2 text-sm"
                        )}
                      >
                        {tag.content}
                      </button>
                    )}
                  </Menu.Item>
                );
              })}
            </div>
          </Menu.Items>
        </Transition>
      </Menu>
      <div className="m-2">
        {selectedTags.map((tag) => {
          return <span className="text-indigo-500 mr-4">{tag}</span>;
        })}
      </div>
    </div>
  );
}
