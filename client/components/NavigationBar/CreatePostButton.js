import React, { useState, useContext } from "react";
import { XIcon } from "@heroicons/react/outline";
import { userContext } from "../../auth/auth";
import Image from "next/image";

const CreatePostButton = () => {
  const { user, refreshfeed } = useContext(userContext);
  const [showModal, setShowModal] = useState(false);
  const [post, setPost] = useState(""); //the text
  const [, setError] = useState(false);
  const [, setLoading] = useState(false);
  const [image, setImage] = useState(null);
  const [clientImage, setClientImage] = useState(undefined);

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
  };

  const checkHastag = (text) => {};

  const handleCreatePost = (e) => {
    e.preventDefault();

    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        post: post,
        userId: user.id,
        imageDTO: image == null ? null : { name: "", blob: image },
      }),
    };

    fetch("http://localhost:7215/api/Post/AddPost", options)
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
      });
  };

  return (
    <>
      <button
        className="mx-1 sm:mx-5 justify-center flex w-40 border px-1 p-1 rounded-md bg-indigo-600 text-white"
        type="button"
        onClick={() => setShowModal(true)}
      >
        Post
      </button>
      {showModal ? (
        <>
          <div className="justify-center items-start mt-16 flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
            <div className="relative sm:w-6/12 my-6 mx-auto max-w-3xl">
              <div className="border-0 rounded-lg shadow-sm relative flex flex-col w-full bg-white outline-none focus:outline-none">
                <div className="flex items-start justify-between p-5 border-solid border-slate-200 rounded-t">
                  <h2>Post about the latest crypto news</h2>
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
                      <div>
                        <div className="mt-1">
                          <textarea
                            id="about"
                            name="about"
                            rows={4}
                            className="shadow-sm focus:ring-indigo-500 focus:border-indigo-500 mt-1 block w-full sm:text-sm border border-gray-300 rounded-md p-2"
                            placeholder="Share your crypto thoughts"
                            defaultValue={""}
                            onChange={(e) => {
                              setPost(e.target.value);
                            }}
                          />
                        </div>
                      </div>

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
                        Share
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
    </>
  );
};

export default CreatePostButton;
