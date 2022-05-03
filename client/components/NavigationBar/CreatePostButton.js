import React, { useState } from "react";

const CreatePostButton = () => {
  const [showModal, setShowModal] = useState(false);

  return (
    <>
      <button
        className="w-40 border px-1 p-1 rounded-md bg-indigo-600 text-white"
        type="button"
        onClick={() => setShowModal(true)}
      >
        Create Post
      </button>
      {showModal ? (
        <>
          <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
            <div className="relative w-6/12 my-6 mx-auto max-w-3xl">
              {/*content*/}
              <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                {/*header*/}
                <div className="flex items-start justify-between p-5 border-solid border-slate-200 rounded-t">
                  <h2>Post about the latest crypto news</h2>
                </div>
                {/*body*/}
                <div className="relative p-6 flex-auto">
                  <p className="my-4 text-slate-500 text-lg leading-relaxed">
                    Add form to handle uploading here
                  </p>
                </div>
                {/*footer*/}
                <div className="flex items-center justify-start p-6 border-solid border-slate-200 rounded-b">
                  <button
                    className="w-40 border px-1 p-1 rounded-md bg-indigo-600 text-white"
                    type="button"
                    onClick={() => setShowModal(false)}
                  >
                    Post
                  </button>
                  <button
                    className="w-40 px-1 p-1 text-red-600"
                    type="button"
                    onClick={() => setShowModal(false)}
                  >
                    Cancel
                  </button>
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
