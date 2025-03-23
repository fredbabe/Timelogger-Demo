import { describe, it, expect } from "vitest";
import { render, screen } from "@testing-library/react";

import { Button } from "./Button";

describe("Button Component", () => {
  it("renders correctly", () => {
    render(<Button disable={false} isLoading={false} label="Click me" />);
    expect(screen.getByText("Click me")).toBeInTheDocument();
    expect(screen.queryByTestId("spinner")).not.toBeInTheDocument();
  });

  it("render spinner when loading is true", () => {
    render(<Button disable={false} isLoading={true} label="Click me" />);
    expect(screen.getByTestId("spinner")).toBeInTheDocument();
  });
});
