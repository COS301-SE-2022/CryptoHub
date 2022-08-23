import { FaStar } from "react-icons/fa";
import { Container, Radio, Rating } from "./RatingStyles";
import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import { useRouter } from "next/router";
import { userContext } from "../../auth/auth";

function CurrentRating() {
  return <div>CurrentRating</div>;
}

export default CurrentRating;
