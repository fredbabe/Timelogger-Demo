import { describe, it, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import { LabelForInput } from "./LabelForInput";

describe("Label for input component", () => {
  it("renders correctly", () => {
    render(<LabelForInput label="Label here" />);
    expect(screen.getByText("Label here"));
  });
});
