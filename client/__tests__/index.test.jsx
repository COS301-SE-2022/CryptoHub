// __tests__/index.test.jsx

import { render, screen } from "@testing-library/react";
import { Component } from "react";
import Home from "../pages/index";
import "@testing-library/jest-dom";
import { userContext } from "../auth/auth";
import Landing from "../components/LandingPage/LandingPage";
import Login from "../pages/login";
import Signup from "../pages/signup";
import Feed from "../components/Feed/Feed";


describe("Landing Page", () => {
  it("Correctly renders the landing page", () => {
    const Component = render(<Landing />);

    expect(Component.getByText("Explore the world of cryptocurrencies")).toBeInTheDocument();
    expect(Component.getByRole('link', { name: "Log in" })).toBeInTheDocument();
    expect(Component.getByRole('link', { name: "Create account" })).toBeInTheDocument();
  });
});

describe("Login", () => {
  it("Correctly renders the login page", () => {
    const Component = render(<Login />);

    expect(Component.getByRole("heading", { name: "Log in to your account" })).toBeInTheDocument();
    expect(Component.getByRole("textbox", { name: "Email address" })).toBeInTheDocument();
    // expect(Component.getByRole("textbox", { name: "Password" })).toBeInTheDocument();
    expect(Component.getByRole("link", { name: "Forgot your password?" })).toBeInTheDocument();
    expect(Component.getByRole("button", { name: "Log in" })).toBeInTheDocument();
  });
});

describe("Signup", () => {
  it("Correctly renders the signup page", () => {
    const Component = render(<Signup />);

    const button = screen.getByRole('button', { name: "Create account" });

    expect(button).toBeInTheDocument();
  });
});

describe("Posts Feed", () => {
  it("Correctly renders the posts feed", () => {
    const Component = render(<Feed />);
    //expect(Component.getByRole("button", { name: "comments" })).toBeInTheDocument();
  });
});