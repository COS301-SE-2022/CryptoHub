import React, { useState } from "react";
import { useRouter } from "next/router"
import Router from "next/router";

function changePassword() {
    const [error, setError] = useState(false);
    const [passwordUpdated, setPasswordUpdated] = useState(false);
    const [loading, setLoading] = useState(false);
    const [newPassword, setNewPassword] = useState("");
    const [confirmNewPassword, setConfirmNewPassword] = useState("");

    const handleCheckPassword = (e) => {
      setLoading(true);
      e.preventDefault();

      console.log(newPassword);
      console.log(confirmNewPassword);

      if(newPassword != confirmNewPassword)
        {
          setError(true);
          setLoading(false);
        } else{
          const options = {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              
              password: newPassword,
            }),
          };

          fetch("http://localhost:7215/api/Authorization/UpdatePassword", options)
            .then((response) => response.json())
            .then((data) => {
              setLoading(false);
              if (response == "password updated") {
                Router.push("/login");

              } else if(response == "new password same as old password"){
                console.log(response)
                setError(true);
                
              }
            })
            .catch(() => {
              setError(true);
              setLoading(false);
            })
        }
    };

  return (
    <>
    <div className="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-md w-full space-y-8">
        <div>
          <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
            Change Password
          </h2>
        </div>
        <form className="mt-8 space-y-6" onSubmit={handleCheckPassword} >
          <input type="hidden" name="remember" defaultValue="true" />
          <div className="rounded-md shadow-sm -space-y-px">
            <div>
              <label htmlFor="newPassword" className="sr-only">
                New Password
              </label>
              <input
                id="newPassword"
                name="newPassword"
                type="newPassword"
                autoComplete="newPassword"
                required
                className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="New Password"
                onChange={(e) => {
                  setNewPassword(e.target.value);
                  
                }}
              />
            </div>
            <div>
              <label htmlFor="confirmPassword" className="sr-only">
                Confirm New Password
              </label>
              <input
                id="confirmPassword"
                name="confirmPassword"
                type="confirmPassword"
                autoComplete="confirmPassword"
                required
                className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Confirm New Password"
                onChange={(e) => {
                  setConfirmNewPassword(e.target.value);
                }}
              />
            </div>
          </div>

          <div className="flex items-center justify-center">
            <div className="text-sm">
              <a
                href="/forgotPassword"
                className="font-medium text-indigo-600 hover:text-indigo-500"
              >
                Back to Forgot Password?
              </a>
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
                <p>Confirm</p>
              )}
            </button>
          </div>
        </form>
        
        {passwordUpdated ? (
          <h2 className="text-center text-sm font-semibold text-green-500">
          Password Updated
          </h2>
        ) :null}

        {error ? (
          <h2 className="text-center text-sm font-semibold text-red-500">
            Passwords do not match!
          </h2>
        ) : null}
      </div>
    </div>
  </>
  )
}

export default changePassword