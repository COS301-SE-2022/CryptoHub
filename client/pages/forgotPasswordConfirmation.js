import React, { useState } from "react";
import { LockClosedIcon } from "@heroicons/react/solid";
import { userContext } from "../auth/auth";
import { useContext } from "react";
import { useRouter } from "next/router"

function forgotPasswordConfirmation() {
    const { authorise } = useContext(userContext);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(false);
    const [loading, setLoading] = useState(false);
    const router = useRouter();
    
  return (
    <div>forgotPasswordConfirmation</div>
  )
}

export default forgotPasswordConfirmation