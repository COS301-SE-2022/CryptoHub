import React, { useState } from "react";
import { useRouter } from "next/router"
import Router from "next/router";

function changePasword() {
    const router = useRouter();
    const [email, setEmail] = useState("");
    const [error, setError] = useState(false);
    const [loading, setLoading] = useState(false);
  return (
    <div>changePasword</div>
  )
}

export default changePasword