import React, { useState, useEffect, useContext } from "react";
import Layout from "../components/Layout";
import Head from "next/head";
import { userContext } from "../auth/auth";
import Post from "../components/Posts/Post";
import { useRouter } from "next/router";
import { XIcon } from "@heroicons/react/outline";
import SuggestedAccount from "../components/InfoSection/SuggestedAccount";

const Coin = () => {
    return(<>
    <Layout></Layout>
    </>)
}

export default Coin;