// __tests__/index.test.jsx

import { render, screen } from "@testing-library/react";
import Home from "../pages/index";
import "@testing-library/jest-dom";
import Feed from "../components/Feed/Feed";
import { userContext } from "../auth/auth";
import Landing from "../components/LandingPage/LandingPage";
import { Component } from "react";


describe("Landing Page", () => {
  it("renders the landing page", () => {
    const Component = render(<Landing />);

    const heading = screen.getByRole('link', { name: "Log in" });

    expect(heading).toBeInTheDocument();
  });
});
