import clsx from "clsx";

export type TabsProps = {
  setSelected: (selected: TabsTypes) => void;
  selected: string;
};

export type TabsTypes = "Registration" | "Projects" | "CreateProject";

export const Tabs = (props: TabsProps) => {
  const { setSelected, selected } = props;
  return (
    <div
      data-testid="TabsTestId"
      className={
        "h-[40px] flex flex-row max-w-[400px] bg-zinc-100 w-full items-center p-1 justify-center rounded-md"
      }
    >
      <button
        onClick={() => setSelected("Registration")}
        className={clsx(
          "flex items-center h-full justify-center text-center text-[14px] font-medium w-full  cursor-pointer",
          selected == "Registration"
            ? "text-black bg-white rounded-sm "
            : "text-stone-500"
        )}
      >
        Registration
      </button>
      <button
        onClick={() => setSelected("Projects")}
        className={clsx(
          "flex items-center justify-center h-full text-center w-full text-[14px] font-medium  cursor-pointer",
          selected == "Projects"
            ? "text-black bg-white rounded-sm "
            : "text-stone-500"
        )}
      >
        Projects
      </button>
      <button
        onClick={() => setSelected("CreateProject")}
        className={clsx(
          "flex items-center justify-center h-full text-center w-full text-[14px] font-medium  cursor-pointer",
          selected == "CreateProject"
            ? "text-black bg-white rounded-sm "
            : "text-stone-500"
        )}
      >
        Create Project
      </button>
    </div>
  );
};
