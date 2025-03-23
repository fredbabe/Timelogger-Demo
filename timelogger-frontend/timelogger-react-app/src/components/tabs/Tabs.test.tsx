import { describe, it, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import { Tabs } from "./Tabs";

describe("Tabs component", () => {
  it("renders correctly", () => {
    render(<Tabs setSelected={() => {}} selected={""} />);
    expect(screen.getByText("Registration")).toBeInTheDocument();
    expect(screen.getByText("Projects")).toBeInTheDocument();
    expect(screen.getByTestId("TabsTestId"));
  });
});
