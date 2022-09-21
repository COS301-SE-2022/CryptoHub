import React, { useState, useContext } from "react";
import { userContext } from "../auth/auth";
import { useRouter } from "next/router";
import Loader from "../components/Loader";

const Signup = () => {
  const { authorise, url } = useContext(userContext);
  const router = useRouter();
  const [username, setUsername] = useState("");
  const [firstname, setFirstname] = useState("");
  const [lastname, setLastname] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);
  const [validPassword, setValidPassword] = useState(false);

  const checkPassword = (value) => {
    let expression = new RegExp(
      "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})"
    );
    return expression.test(value);
  };

  const validator = (value) => {
    if (checkPassword(value)) {
      setValidPassword(true);
    } else {
      setValidPassword(false);
    }
  };

  const handleSignup = (e) => {
    setLoading(true);
    e.preventDefault();

    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        firstname: firstname,
        lastname: lastname,
        username: username,
        email: email,
        password: password,
      }),
    };

    fetch(`${url}/api/Authorization/Register`, options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        if (data.token) {
          authorise(data.token);
        } else {
          setError(true);
        }
      })
      .catch((error) => {
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
                  data-tip="hello world"
                  className={`appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 ${
                    validPassword
                      ? "focus:border-green-700"
                      : "focus:border-red-500"
                  } focus:z-10 sm:text-sm`}
                  placeholder="Password"
                  onChange={(e) => {
                    setPassword(e.target.value);
                    validator(e.target.value);
                  }}
                />
              </div>
            </div>
            {!validPassword ? (
              <h2 className="text-center text-xs text-gray-500 -translate-y-3">
                Password must contain min 8 characters, at least one lowercase
                letter, uppercase letter, special character & number
              </h2>
            ) : (
              <h2 className="text-center text-sm text-green-700 -translate-y-3">
                Great password!
              </h2>
            )}

            <div>
              <button
                type="submit"
                className={`group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white ${
                  validPassword
                    ? "bg-indigo-600 hover:bg-indigo-700"
                    : "bg-gray-400 cursor-not-allowed"
                } focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500`}
                disabled={!validPassword}
              >
                {loading ? <Loader /> : <p>Create account</p>}
              </button>
              <div className="text-gray-400 text-center font-sm mt-5">
                Already signed up ?{" "}
                <span className="text-indigo-700 focus:underline">
                  <button
                    onClick={() => {
                      router.push("/login");
                    }}
                  >
                    Log in here
                  </button>
                </span>
              </div>
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
};

export default Signup;
