import { describe, it, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import { Spinner } from "./Spinner";

describe("Spinner Component", () => {
  it("renders correctly", () => {
    render(<Spinner />);
    expect(screen.getByTestId("spinner"));
  });
});
