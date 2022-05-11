import React, { useState, useContext } from "react";
import { userContext } from "../auth/auth";

export default function Signup() {
  const { authorise } = useContext(userContext);

  const [username, setUsername] = useState("")
  const [firstname, setFirstname] = useState("");
  const [lastname, setLastname] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleSignup = (e) => {
    setLoading(true);
    e.preventDefault();

    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userName: username,
        firstName: firstname,
        lastName: lastname,
        email: email,
        password: password,
      }),
    };

    // fetch("https:localhost:3000/signup", options)
    // fetch("https://627bcb89b54fe6ee008f5488.mockapi.io/users", options)
    fetch("http://localhost:8082/api/authorization/register", options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        console.warn(data);
        if (data.authorized) {
          authorise(data.userId);
        } else {
          setError(true);
        }
      })
      .catch((error) => {
        console.warn("Error", error);
        setError(true);
        setLoading(false);
      });
  };

  return (
    <>
      <div className="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
        <div className="max-w-md w-full space-y-8">
          <div>
            <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
              Create your account
            </h2>
          </div>
          <form
            className="mt-8 space-y-6"
            onSubmit={handleSignup}
            method="POST"
          >
            <input type="hidden" name="remember" defaultValue="true" />
            <div className="rounded-md shadow-sm -space-y-px">
            <div>
                <label htmlFor="username" className="sr-only">
                  Username
                </label>
                <input
                  id="username"
                  name="username"
                  type="text"
                  required
                  className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                  placeholder="Username"
                  onChange={(e) => {
                    setUsername(e.target.value);
                  }}
                />
              </div>
              <div>
                <label htmlFor="firstname" className="sr-only">
                  Firstname
                </label>
                <input
                  id="firstname"
                  name="firstname"
                  type="text"
                  required
                  className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                  placeholder="Firstname"
                  onChange={(e) => {
                    setFirstname(e.target.value);
                  }}
                />
              </div>
              <div>
                <label htmlFor="lastname" className="sr-only">
                  Lastname
                </label>
                <input
                  id="lastname"
                  name="lastname"
                  type="text"
                  required
                  className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                  placeholder="Lastname"
                  onChange={(e) => {
                    setLastname(e.target.value);
                  }}
                />
              </div>
              <div>
                <label htmlFor="email-address" className="sr-only">
                  Email address
                </label>
                <input
                  id="email-address"
                  name="email"
                  type="email"
                  autoComplete="email"
                  required
                  className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                  placeholder="Email address"
                  onChange={(e) => {
                    setEmail(e.target.value);
                  }}
                />
              </div>
              <div>
                <label htmlFor="password" className="sr-only">
                  Password
                </label>
                <input
                  id="password"
                  name="password"
                  type="password"
                  autoComplete="current-password"
                  required
                  className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                  placeholder="Password"
                  onChange={(e) => {
                    setPassword(e.target.value);
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
                  <p>Create account</p>
                )}
              </button>
            </div>
          </form>
          {error ? (
            <h2 className="text-center text-sm font-semibold text-red-500">
              Failed to sign up
            </h2>
          ) : null}
        </div>
      </div>
    </>
  );
}
