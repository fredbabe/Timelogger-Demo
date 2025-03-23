import { describe, it, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import { NumberInput } from "./NumberInput";
import { FieldError } from "react-hook-form";

describe("Number input component", () => {
  it("renders correctly", () => {
    render(<NumberInput formLabel="projectId" label="Label" />);
    expect(screen.getByText("Label")).toBeInTheDocument();
    expect(screen.getByTestId("numberInputTestId")).toBeInTheDocument();
    expect(screen.queryByTestId("errorNumberInputId")).not.toBeInTheDocument();
  });

  it("renders error message", () => {
    render(
      <NumberInput
        formLabel="projectId"
        label="Label"
        errors={{ message: "error message" } as FieldError}
      />
    );
    expect(screen.getByText("error message")).toBeInTheDocument();
    expect(screen.getByTestId("errorNumberInputId")).toBeInTheDocument();
  });
});
