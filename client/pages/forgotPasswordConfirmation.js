import React, { useState } from "react";
import { LockClosedIcon } from "@heroicons/react/solid";
import { userContext } from "../auth/auth";
import { useContext } from "react";
import { useRouter } from "next/router";

function forgotPasswordConfirmation() {
  const { authorise, url } = useContext(userContext);
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);
  const [otp, setOtp] = useState(0);
  const router = useRouter();

  const handleValidOtp = (e) => {
    setLoading(true);
    e.preventDefault();

    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: router.query.email,
        otp: otp,
      }),
    };
    fetch(
      `${url}/api/Authorization/ValidateOTP/${router.query.email}/${otp}`,
      options
    )
      .then((response) => {
        if (response.status === 200) {
          router.push("/forgotPasswordConfirmation?email=" + email);
        } else setError(true);
      })

      .then((data) => {
        setLoading(false);
      })
      .catch(() => {
        setError(true);
        setLoading(false);
      });
  };

  return (
    <>
      <div className="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
        <div className="max-w-md w-full space-y-8">
          <div>
            <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900 colour-black">
              Confirm it is YOU
            </h2>
          </div>
          <form className="mt-8 space-y-6" onSubmit={handleValidOtp}>
            <div className="flex items-center justify-center">
              <div className="text-sm">
                <p className=" colour-black text-center font-medium">
                  There has been a confirmation code sent to your email to
                  verfiy that it is you.
                </p>
              </div>
            </div>

            <input type="hidden" name="remember" defaultValue="true" />
            <div className="rounded-md shadow-sm -space-y-px">
              <div>
                <input
                  id="confirmation-Code"
                  name="Code"
                  type="Code"
                  autoComplete="Code"
                  required
                  className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                  placeholder="Please enter code"
                  onSubmit={(e) => {
                    setOtp(e.target.value);
                    console.log(otp);
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
                  <a href={`/changePassword?email=${router.query.email}`}>
                    Confirm
                  </a>
                )}
              </button>
            </div>

            <div className="flex items-center justify-center">
              <div className="text-sm">
                <a
                  href="/forgotPassword"
                  className="font-medium text-indigo-600 hover:text-indigo-500"
                >
                  Back to Forgot Password
                </a>
              </div>
            </div>
          </form>
          {error ? (
            <h2 className="text-center text-sm font-semibold text-red-500">
              invalid Code
            </h2>
          ) : null}
        </div>
      </div>
    </>
  );
}

export default forgotPasswordConfirmation;
