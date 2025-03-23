import { describe, it, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import { SelectInput } from "./SelectInput";

describe("Select Input Component", () => {
  it("renders correctly", () => {
    render(<SelectInput formLabel="projectId" label="Label" />);
    expect(screen.getByText("Label")).toBeInTheDocument();
    expect(screen.getByTestId("selectInputTestId")).toBeInTheDocument();
  });
});
