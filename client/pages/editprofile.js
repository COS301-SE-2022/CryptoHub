import React, { useState, useContext, useEffect } from "react";
import Layout from "../components/Layout";
import { XIcon } from "@heroicons/react/outline";
import Image from "next/image";
import { userContext } from "../auth/auth";
import { useRouter } from "next/router";

const editprofile = () => {
  const { user, refreshfeed, profilePicture } = useContext(userContext);
  const [showModal, setShowModal] = useState(false);
  const [post, setPost] = useState("");
  const [, setError] = useState(false);
  const [, setLoading] = useState(false);
  const [image, setImage] = useState(null);
  const [clientImage, setClientImage] = useState(undefined);

  const router = useRouter();

  const convertToBase64 = (file) => {
    return new Promise((resolve, reject) => {
      const fileReader = new FileReader();
      fileReader.readAsDataURL(file);
      fileReader.onload = () => {
        resolve(fileReader.result);
      };
      fileReader.onerror = (error) => {
        reject(error);
      };
    });
  };

  const handleImageUpload = async (e) => {
    let file = e.target.files[0];
    setClientImage(URL.createObjectURL(file));
    const base64 = await convertToBase64(file);
    let base64Image = base64.split(",").pop();
    setImage(base64Image);
    // console.warn(base64);
    // console.warn(base64Image);
  };

  const handleCreatePost = (e) => {
    e.preventDefault();

    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "bearer " + user.token,
      },
      body: JSON.stringify({
        name: "",
        blob: image,
      }),
    };

    fetch("http://localhost:7215/api/User/UpdateProfileImage", options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        setShowModal(false);
        refreshfeed();
        if (data.userId == user.id) {
        } else {
          setError(true);
        }
      })
      .catch(() => {
        setError(true);
        setLoading(false);
        setShowModal(false);
        refreshfeed();
      });
  };

  useEffect(() => {
    !user.auth && router.push("/");
  });

  return (
    <Layout>
      <>
        <div className="mt-5 md:mt-0 md:col-span-2 w-11/12 sm:w-5/12">
          <form action="#" method="POST">
            <div className="shadow sm:rounded-md sm:overflow-hidden">
              <div className="px-4 py-5 bg-white space-y-6 sm:p-6">
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Photo
                  </label>
                  <div className="mt-1 flex items-center">
                    {profilePicture == null ? (
                      <span className="inline-block h-10 w-10 rounded-full overflow-hidden bg-gray-100">
                        <svg
                          className="h-full w-full text-gray-300"
                          fill="currentColor"
                          viewBox="0 0 24 24"
                        >
                          <path d="M24 20.993V24H0v-2.996A14.977 14.977 0 0112.004 15c4.904 0 9.26 2.354 11.996 5.993zM16.002 8.999a4 4 0 11-8 0 4 4 0 018 0z" />
                        </svg>
                      </span>
                    ) : (
                      <div
                        className="rounded-full overflow-hidden"
                        style={{
                          width: "40px",
                          height: "40px",
                          position: "relative",
                        }}
                      >
                        <Image src={profilePicture} layout="fill" />
                      </div>
                    )}
                    <button
                      type="button"
                      className="ml-5 bg-white py-2 px-3 border border-gray-300 rounded-md shadow-sm text-sm leading-4 font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                      onClick={() => setShowModal(true)}
                    >
                      Change
                    </button>
                  </div>
                </div>
              </div>
              <div className="px-4 py-3 bg-gray-50 text-right sm:px-6">
                {/* <button
                  type="submit"
                  className="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                >
                  Save
                </button> */}
              </div>
            </div>
          </form>
        </div>
      </>
      {showModal ? (
        <>
          <div className="justify-center items-start mt-16 flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
            <div className="relative sm:w-6/12 my-6 mx-auto max-w-3xl">
              <div className="border-0 rounded-lg shadow-sm relative flex flex-col w-full bg-white outline-none focus:outline-none">
                <div className="flex items-start justify-between p-5 border-solid border-slate-200 rounded-t">
                  <h2>Change profile picture</h2>
                  <button
                    className="px-1 p-1"
                    type="button"
                    onClick={() => setShowModal(false)}
                  >
                    <XIcon className="h-6 w-6" aria-hidden="true" />
                  </button>
                </div>
                <div className="relative flex-auto">
                  <form method="POST" onSubmit={handleCreatePost}>
                    <div className="px-4 py-5 bg-white space-y-6 sm:p-6">
                      <div></div>

                      <div>
                        <label className="block text-sm font-medium text-gray-700">
                          Upload a photo
                        </label>
                        <div className="mt-1 flex justify-center px-6 pt-5 pb-6 border-2 border-gray-300 border-dashed rounded-md">
                          <div className="space-y-1 text-center">
                            {clientImage == undefined ? (
                              <svg
                                className="mx-auto h-12 w-12 text-gray-400"
                                stroke="currentColor"
                                fill="none"
                                viewBox="0 0 48 48"
                                aria-hidden="true"
                              >
                                <path
                                  d="M28 8H12a4 4 0 00-4 4v20m32-12v8m0 0v8a4 4 0 01-4 4H12a4 4 0 01-4-4v-4m32-4l-3.172-3.172a4 4 0 00-5.656 0L28 28M8 32l9.172-9.172a4 4 0 015.656 0L28 28m0 0l4 4m4-24h8m-4-4v8m-12 4h.02"
                                  strokeWidth={2}
                                  strokeLinecap="round"
                                  strokeLinejoin="round"
                                />
                              </svg>
                            ) : (
                              <Image
                                src={clientImage}
                                width="200"
                                height="200"
                              />
                            )}

                            <div className="flex text-sm text-gray-600 justify-center">
                              <label
                                htmlFor="file-upload"
                                className="relative cursor-pointer bg-white rounded-md font-medium text-indigo-600 hover:text-indigo-500 focus-within:outline-none focus-within:ring-2 focus-within:ring-offset-2 focus-within:ring-indigo-500"
                              >
                                <span className="text-center">
                                  Upload a file
                                </span>
                                <input
                                  id="file-upload"
                                  name="file-upload"
                                  type="file"
                                  className="sr-only"
                                  onChange={handleImageUpload}
                                />
                              </label>
                            </div>
                            <p className="text-xs text-gray-500">
                              PNG, JPG, GIF up to 10MB
                            </p>
                          </div>
                        </div>
                      </div>
                      <button
                        type="submit"
                        className="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                      >
                        Save
                      </button>
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
    </Layout>
  );
};

export default editprofile;
