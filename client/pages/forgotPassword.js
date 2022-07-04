import React, { useState } from "react";
import { LockClosedIcon } from "@heroicons/react/solid";
import { userContext } from "../auth/auth";
import { useContext } from "react";
import { useRouter } from "next/router"

function forgotPassword() {
    const { authorise } = useContext(userContext);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(false);
    const [loading, setLoading] = useState(false);
    const router = useRouter();

    return (
        <>
          <div className="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
            <div className="max-w-md w-full space-y-8">
              <div>
                <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
                  Trouble Logging in?
                </h2>
              </div>
              <form className="mt-8 space-y-6" >
                <input type="hidden" name="remember" defaultValue="true" />
                <div className="rounded-md shadow-sm -space-y-px">
                  <div>
                    <label htmlFor="email-address" className="sr-only">
                      Email address
                    </label>
                    <input
                    //   id="email-address"
                    //   name="email"
                    //   type="email"
                    //   autoComplete="email"
                    //   required
                      className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                      placeholder="Email address"
                    //   onChange={(e) => {
                    //     setEmail(e.target.value);
                    //   }}
                    />
                  </div>
                </div>
    
                <div>
                  <button
                    type="submit"
                    className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                  >
                    <span className="absolute left-0 inset-y-0 flex items-center pl-3">
                      <LockClosedIcon
                        className="h-5 w-5 text-indigo-500 group-hover:text-indigo-400"
                        aria-hidden="true"
                      />
                    </span>
                    {loading ? (
                      <p className="text-indigo-200">Loading...</p>
                    ) : (
                      <p>Reset Password</p>
                    )}
                  </button>
                </div>

                <div>
                  <div
                    className="group relative w-full flex justify-center py-2 px-4 text-lg font-medium rounded-md text-black focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                  >
                    {loading ? (
                      <p className="text-indigo-200">Loading...</p>
                    ) : (
                      <><div className="bg-gray-400 sm:w-6/12 translate-y-3" style={{ height: "1px" }}></div><p>OR</p><div className="bg-gray-400 sm:w-6/12 translate-y-3" style={{ height: "1px" }}></div></>
                    )}
                  </div>
                </div>

                <div>
                  <button
                    type="submit"
                    className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                  >
                    {loading ? (
                      <p className="text-indigo-200">Loading...</p>
                    ) : (
                      <p className = "justify-center">Create Account</p>
                    )}
                  </button>
                </div>

                <div>
                  <button
                    type="submit"
                    className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                  >
                    {loading ? (
                      <p className="text-indigo-200">Loading...</p>
                    ) : (
                      <p className = "justify-center">Back to Lgin Page</p>
                    )}
                  </button>
                </div>


              </form>
              {/* {error ? (
                <h2 className="text-center text-sm font-semibold text-red-500">
                  invalid login credentials
                </h2>
              ) : null} */}
            </div>
          </div>
        </>
      );
}

export default forgotPassword