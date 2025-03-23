import { ComponentProps } from "react";
import { FieldValues, Path, UseFormRegister } from "react-hook-form";
import { LabelForInput } from "../labelForInput/LabelForInput";

type optionValue = {
  id: string;
  displayName: string;
};

export type SelectInputProps<T extends FieldValues> = {
  options?: optionValue[];
  label: string;
  register?: UseFormRegister<T>;
  required?: boolean;
  formLabel: Path<T>;
} & ComponentProps<"select">;

export const SelectInput = <T extends FieldValues>(
  props: SelectInputProps<T>
) => {
  const { options, label, register, required, formLabel } = props;
  return (
    <div className="w-full max-w-sm min-w-[200px]">
      <LabelForInput label={label} />
      <div className="relative">
        <select
          data-testid="selectInputTestId"
          {...(register ? register(formLabel, { required }) : {})}
          className="w-full bg-transparent placeholder:text-slate-400 text-slate-700 text-sm border border-slate-200 rounded pl-3 pr-8 py-2 transition duration-300 ease focus:outline-none focus:border-slate-400 hover:border-slate-400 shadow-sm focus:shadow-md appearance-none cursor-pointer"
        >
          {options?.map((x, index) => (
            <option key={index} value={x.id}>
              {x.displayName}
            </option>
          ))}
        </select>
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          strokeWidth="1.2"
          stroke="currentColor"
          className="h-5 w-5 ml-1 absolute top-2.5 right-2.5 text-slate-700"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            d="M8.25 15 12 18.75 15.75 15m-7.5-6L12 5.25 15.75 9"
          />
        </svg>
      </div>
    </div>
  );
};
