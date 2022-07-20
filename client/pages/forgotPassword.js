import React, { useState } from "react";
import { useRouter } from "next/router"

function forgotPassword() {
    const router = useRouter();
    const [email, setEmail] = useState("");
    const [error, setError] = useState(false);
    const [loading, setLoading] = useState(false);

      const handleGetForgotPassword = (e) => {
        setLoading(true);
        e.preventDefault();

        const options = {
          method: "GET",
        };
    
        fetch(`http://localhost:7215/api/User/GetUserByEmail/${email}`, options)
          .then((response) => response.json())
          .then((data) => {
            setLoading(false);
            setEmail(data.email);
            {
              if(data.email == email){
                router.push("/forgotPasswordConfirmation?email="+email);
              } else{
                setError(true);
              }
            }
            
          })
          .catch((error) => {});
          
      };

    return (
        <>
          <div className="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
            <div className="max-w-md w-full space-y-8">
              <div>
                <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
                  Trouble Logging in?
                </h2>
              </div>
              <form className="mt-8 space-y-6" onSubmit={handleGetForgotPassword} >
                <input type="hidden" name="remember" defaultValue="true" />
                <div className="rounded-md shadow-sm -space-y-px">
                  <div>
                    <input
                      id="email-address"
                      name="email"
                      type="email"
                      autoComplete="email"
                      required
                      className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                      placeholder="Email address"
                      onChange={(e) => {
                        setEmail(e.target.value);
                      }}
                    />
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
                      <p >Reset Password</p>
                    )}
                  </button>
                </div>

                <div>
                  <div
                    className="group relative w-full flex justify-center py-2 px-4 text-lg font-medium rounded-md text-black focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                  >
                      <><div className="bg-gray-400 sm:w-6/12 translate-y-3" style={{ height: "1px" }}></div><p>OR</p><div className="bg-gray-400 sm:w-6/12 translate-y-3" style={{ height: "1px" }}></div></>
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
                      <a href="/signup" className = "justify-center">Create Account</a>
                    )}
                  </button>
                </div>

                <div className="flex items-center justify-center">
              <div className="text-sm">
                <a
                  href="/login"
                  className="font-medium text-indigo-600 hover:text-indigo-500"
                >
                  Back to Login
                </a>
              </div>
            </div>


              </form>
              {error ? (
                <h2 className="text-center text-sm font-semibold text-red-500">
                  invalid email address
                </h2>
              ) : null}
            </div>
          </div>
        </>
      );
}

export default forgotPassword