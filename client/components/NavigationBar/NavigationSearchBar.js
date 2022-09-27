import React, { useState, useContext, useEffect } from "react";
import { userContext } from "../../auth/auth";
import { XIcon } from "@heroicons/react/outline";
import SuggestedAccount from "../InfoSection/SuggestedAccount";
import Loader from "../Loader";

const NavigationSearchBar = () => {
  const [showModal, setShowModal] = useState(false);
  const [users, setUsers] = useState([]);
  const [searchInput, setSearchInput] = useState("");
  const [loading, setLoading] = useState(false);

  const { user, url } = useContext(userContext);

  useEffect(() => {
    searchByUsername(searchInput);
  }, [searchInput]);

  const searchByUsername = (searchTerm) => {
    const options = {
      method: "GET",
    };
    setLoading(true);
    fetch(`${url}/api/User/SearchUser/${user.id}/${searchTerm}`, options)
      .then((response) => response.json())
      .then((data) => {
        setUsers(data);
        setLoading(false);
      })
      .catch(() => {
        setLoading(false);
      });
  };

  const submitHandler = (e) => {
    e.preventDefault();
  };

  return (
    <>
      <button
        onClick={() => setShowModal(true)}
        className="border text-left text-gray-400 rounded-md w-full px-2 py-1 mr-1 sm:mr-4 focus:outline-none focus:ring-indigo-500 hover:border-indigo-500 cursor-text"
      >
        Search
      </button>
      {showModal ? (
        <>
          <div className="justify-center items-start mt-16 flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
            <div className="relative w-10/12 sm:w-6/12 my-6 mx-auto max-w-3xl">
              <div className="border-0 rounded-lg shadow-sm relative flex flex-col w-full bg-white outline-none focus:outline-none">
                <div className="relative flex-auto">
                  <form
                    method="POST"
                    onSubmit={(e) => {
                      e.preventDefault();
                    }}
                  >
                    <div className="flex items-start justify-between p-5 border-solid border-slate-200 rounded-t">
                      <input
                        autoFocus
                        className="border rounded-md w-full px-2 py-1 mr-1 sm:mr-4 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
                        type="text"
                        placeholder="Search"
                        value={searchInput}
                        onChange={(e) => {
                          setSearchInput(e.target.value);
                        }}
                      />
                      <button
                        className="px-1 p-1"
                        type="button"
                        onClick={() => setShowModal(false)}
                      >
                        <XIcon className="h-6 w-6" aria-hidden="true" />
                      </button>
                    </div>
                    <div className="flex flex-col p-5">
                      <div>
                        {loading ? (
                          <Loader />
                        ) : users.length == 0 || searchInput == "" ? (
                          <p className="text-gray-400">No search results</p>
                        ) : (
                          users.map((data, index) => {
                            return (
                              <SuggestedAccount
                                key={index}
                                name={data.username}
                                username={data.username}
                                firstname={data.firstname}
                                lastname={data.lastname}
                                id={data.userId}
                                hidefollow={true}
                              />
                            );
                          })
                        )}
                      </div>
                    </div>
                  </form>
                </div>
                <div className="flex items-center justify-end p-6 border-solid border-slate-200 rounded-b"></div>
              </div>
            </div>
          </div>
          <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
        </>
      ) : null}
    </>
  );
};

export default NavigationSearchBar;
