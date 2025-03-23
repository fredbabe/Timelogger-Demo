import { describe, it, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import { TextInput } from "./TextInput";
import { FieldError } from "react-hook-form";

describe("Text Input Component", () => {
  it("renders correctly", () => {
    render(<TextInput formLabel="description" label="Label" />);
    expect(screen.getByText("Label")).toBeInTheDocument();
    expect(screen.getByTestId("TextInputTestId")).toBeInTheDocument();
  });

  it("renders error message", () => {
    render(
      <TextInput
        formLabel="description"
        label="Label"
        errors={{ message: "error message" } as FieldError}
      />
    );

    expect(screen.getByText("error message")).toBeInTheDocument();
    expect(screen.getByTestId("errorTextInputId")).toBeInTheDocument();
  });
});
