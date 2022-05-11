import React, { useState, useContext } from "react";
import { XIcon } from "@heroicons/react/outline";
import { userContext } from "../../auth/auth";


const CreatePostButton = () => {
  const { user } = useContext(userContext)
  const [showModal, setShowModal] = useState(false);
  const [post, setPost] = useState("")
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleCreatePost = (e) => {
    e.preventDefault()

    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userid: user.id,
        post: post,
      }),
    };

    fetch("http://localhost:8082/api/post/createpost", options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        console.warn(data);
        if (data.userId == user.id) {
          console.warn("Posted");
        } else {
          setError(true);
        }
      })
      .catch((error) => {
        console.warn("Error", error);
        setError(true);
        setLoading(false);
      });

  }

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
          <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
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
                            rows={5}
                            className="shadow-sm focus:ring-indigo-500 focus:border-indigo-500 mt-1 block w-full sm:text-sm border border-gray-300 rounded-md p-2"
                            placeholder="Share your crypto thoughts"
                            defaultValue={""}
                            onChange={(e) => {
                              setPost(e.target.value);
                            }}
                          />
                        </div>
                      </div>
                    </div>
                    <button
                    type="submit"
                    className="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                  >
                    Share
                  </button>
                  </form>
                </div>
                <div className="flex items-center justify-end p-6 border-solid border-slate-200 rounded-b">
                  
                </div>
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
