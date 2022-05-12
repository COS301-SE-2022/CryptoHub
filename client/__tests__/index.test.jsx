// __tests__/index.test.jsx

import { fireEvent, render, screen } from "@testing-library/react";
import { Component } from "react";
import Home from "../pages/index";
import "@testing-library/jest-dom";
import { userContext } from "../auth/auth";
import Landing from "../components/LandingPage/LandingPage";
import Login from "../pages/login";
import Signup from "../pages/signup";
import Feed from "../components/Feed/Feed";
import NavBar from "../components/NavigationBar/NavigationBar";
import PostButton from "../components/NavigationBar/CreatePostButton";
import Suggestions from "../components/InfoSection/Suggestions";
import News from "../components/InfoSection/News";


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
    expect(Component.getByRole("link", { name: "Forgot your password?" })).toBeInTheDocument();
    expect(Component.getByRole("button", { name: "Log in" })).toBeInTheDocument();
  });
});

describe("Signup", () => {
  it("Correctly renders the signup page", () => {
    const Component = render(<Signup />);

    expect(Component.getByRole("heading", { name: "Create your account" })).toBeInTheDocument();
    expect(Component.getByRole("textbox", { name: "Firstname" })).toBeInTheDocument();
    expect(Component.getByRole("textbox", { name: "Lastname" })).toBeInTheDocument();
    expect(Component.getByRole("textbox", { name: "Email address" })).toBeInTheDocument();
    expect(Component.getByRole('button', { name: "Create account" })).toBeInTheDocument();
  });
});

describe("Posts Feed", () => {
  it("Correctly renders the posts feed", () => {
    const Component = render(<Feed />);
  });
});

describe("Post button", () => {
  it("Correctly renders the post button", () => {
    const Component = render(<NavBar />);

    const button = screen.getByRole('button', { name: "Post" });
    expect(button).toBeInTheDocument();
  });

  it("Opens post menu when post is clicked", () => {
    const Component = render(<NavBar />);

    const button = screen.getByRole('button', { name: "Post" });

    fireEvent(button, new MouseEvent('click', { bubbles: true }));
    expect(Component.getByRole("button", { name: "Share" })).toBeInTheDocument();
  });
  it("Closes post menu when \"X\" is clicked", () => {
    const Component = render(<NavBar />);
    const button = screen.getByRole('button', { name: "Post" });

    fireEvent(button, new MouseEvent('click', { bubbles: true }));
    const button2 = screen.getByRole('button', { name: "", exact: true });
    fireEvent(button2, new MouseEvent('click', { bubbles: true }));
    expect(Component.queryByRole("button", { name: "Share" })).not.toBeInTheDocument();

  });

});

describe("Suggestions", () => {
  it("Correctly renders the suggestions", () => {
    const Component = render(<Suggestions />);
    expect(Component.getByText("Suggestions")).toBeInTheDocument();
  });
});

describe("News", () => {
  it("Correctly renders the news", () => {
    const Component = render(<News />);
    expect(Component.getByText("News")).toBeInTheDocument();
  });
});

describe("Profile", () => {
  it("Profile icon renders and works correctly", () => {
    const Component = render(<NavBar />);
    const button = screen.getByText('Open user menu');
    expect(button).toBeInTheDocument();

    fireEvent(button, new MouseEvent('click', { bubbles: true }));
    expect(Component.getByRole("menuitem", { name: "Sign out" })).toBeInTheDocument();
  });
});