import { useRouter } from "next/router";
import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import Layout from "../../components/Layout";
import { userContext } from "../../auth/auth";
import Post from "../../components/Posts/Post";
import Image from "next/image";

const User = () => {
  const router = useRouter();
  const { id } = router.query;
  const [thisUser, setUser] = useState({});
  const [posts, setPosts] = useState([]);
  const [clicked, setClicked] = useState(false);
  const [isFollowing, setIsFollowing] = useState(false);

  const { user, url } = useContext(userContext);
  const [profilePicture, setProfilePicture] = useState(null);

  const handleGetUser = () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/User/GetUserById/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setUser(data);
        setProfilePicture(data.imageUrl);
      })
      .catch((error) => {});
  };

  const handleGetAllPosts = () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/Post/GetAllPosts`, options)
      .then((response) => response.json())
      .then((data) => {
        let posts = data.reverse();
        let myPosts = posts.filter((post) => {
          return post.userId == id;
        });
        setPosts(myPosts);
      })
      .catch(() => {});
  };

  const handleFollowUser = () => {
    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: user.id,
        followerId: id,
      }),
    };

    fetch(`${url}/api/UserFollower/FollowUser/${id}/${user.id}`, options)
      .then((response) => {
        setClicked(true);
        response.json();
      })
      .then((data) => {
        setClicked(true);
        setIsFollowing(true);
      })
      .catch(() => {});
  };

  const checkIfSameUser = () => {
    return user.id == id;
  };

  const checkFollowing = () => {
    fetch(`${url}/api/UserFollower/GetUserFollowing/${user.id}`)
      .then((response) => response.json())
      .then((data) => {
        data.map((d) => {
          if (d.userId == id) {
            setIsFollowing(true);
          }
        });
      });
  };

  const handleViewFollowers = () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/UserFollower/GetUserFollower/${user.id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        setFollowers(data);
      })
      .catch((error) => {
        setError(true);
        setLoading(false);
      });
  };

  const handleViewFollowing = () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/UserFollower/GetUserFollowing/${user.id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        setFollowing(data);
      })
      .catch((error) => {
        setError(true);
        setLoading(false);
      });
  };

  useEffect(() => {
    handleGetUser();
    handleGetAllPosts();
    checkFollowing();
    id == undefined && router.push("/");
  }, []);

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout>
        <div className="flex flex-col sm:flex-row w-full sm:w-7/12 items-center mt-8">
          {profilePicture == null ? (
            <span className="inline-block h-40 w- m-4 rounded-full overflow-hidden bg-gray-100">
              <svg
                className="h-full w-full text-gray-300"
                fill="currentColor"
                viewBox="0 0 24 24"
              >
                <path d="M24 20.993V24H0v-2.996A14.977 14.977 0 0112.004 15c4.904 0 9.26 2.354 11.996 5.993zM16.002 8.999a4 4 0 11-8 0 4 4 0 018 0z" />
              </svg>
            </span>
          ) : (
            <div
              className="rounded-full overflow-hidden m-4"
              style={{
                width: "170px",
                height: "170px",
                position: "relative",
              }}
            >
              <Image src={profilePicture} layout="fill" />
            </div>
          )}
          <div className="flex flex-row">
            <p className="font-semibold text-center sm:text-left">
              {thisUser.username}
            </p>{" "}
            {user.auth ? (
              isFollowing ? (
                <>
                  <p className="text-sm ml-5 text-black bg-gray-400 rounded-md px-3 py-1">
                    Following
                  </p>
                  <button
                    onClick={() => {
                      router.push(`/messages/${id}`);
                    }}
                  >
                    <p className="text-sm text-white ml-5 bg-indigo-600 rounded-md px-3 py-1 hover:bg-indigo-500 transition">
                      Message
                    </p>
                  </button>
                </>
              ) : (
                <>
                  <button onClick={handleFollowUser}>
                    <p className="text-sm text-white ml-5 bg-indigo-600 rounded-md px-3 py-1 hover:bg-indigo-500 transition">
                      Follow
                    </p>
                  </button>
                  <button
                    onClick={() => {
                      router.push(`/messages/${id}`);
                    }}
                  >
                    <p className="text-sm text-white ml-5 bg-indigo-600 rounded-md px-3 py-1 hover:bg-indigo-500 transition">
                      Message
                    </p>
                  </button>
                </>
              )
            ) : null}
            <br />
          </div>
        </div>
        <div className="bg-gray-400 sm:w-7/12" style={{ height: "1px" }}></div>
        <div className="flex flex-col items-center w-full sm:w-5/12">
          <div>
            <p className="text-sm mt-4 text-gray-600">Posts</p>
          </div>
          <div className="w-full">
            {posts.map((data, index) => {
              return (
                <Post
                  key={index}
                  name={data.username}
                  content={data.content}
                  userId={data.userId}
                  imageId={data.imageUrl}
                  postId={data.postId}
                />
              );
            })}
          </div>
        </div>
      </Layout>
    </>
  );
};

export default User;
