import React, { useState, useContext } from "react";
import { userContext } from "../../auth/auth";
import { XIcon } from "@heroicons/react/outline";

const Toast = () => {
  const { show, closeAlert, alertText } = useContext(userContext);

  return (
    <div
      class="flex space-x-2 justify-center"
      style={{ visibility: `${show ? "visibile" : "hidden"}` }}
    >
      <div
        class="bg-white shadow-lg mx-auto w-96 max-w-full text-sm pointer-events-auto bg-clip-padding rounded-lg block fixed bottom-10 z-50"
        id="static-example"
        role="alert"
        aria-live="assertive"
        aria-atomic="true"
        data-mdb-autohide="false"
      >
        <div class=" bg-white flex justify-between items-center py-2 px-3 bg-clip-padding border-b border-gray-200 rounded-t-lg">
          <p class="font-bold text-gray-500">Alert</p>
          <div class="flex items-center">
            <button
              type="button"
              class=" btn-close box-content w-4 h-4 ml-3 text-gray-600 border-none rounded-none opacity-50 focus:shadow-none focus:outline-none focus:opacity-100 hover:text-black hover:opacity-75 hover:no-underline -translate-y-2"
              data-mdb-dismiss="toast"
              aria-label="Close"
              onClick={() => {
                closeAlert();
              }}
            >
              <XIcon className="h-6 w-6" aria-hidden="true" />
            </button>
          </div>
        </div>
        <div class="p-3 text-left bg-white rounded-b-lg break-words text-gray-700">
          {alertText}
        </div>
      </div>
    </div>
  );
};

export default Toast;
