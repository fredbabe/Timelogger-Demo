import {
  UseFormRegister,
  Path,
  FieldError,
  FieldValues,
} from "react-hook-form";
import { LabelForInput } from "../labelForInput/LabelForInput";

export type NumberInputProps<T extends FieldValues> = {
  label: string;
  register?: UseFormRegister<T>;
  formLabel: Path<T>;
  requiredMessage?: string;
  errors?: FieldError;
};

export const NumberInput = <T extends FieldValues>(
  props: NumberInputProps<T>
) => {
  const { label, register, formLabel, requiredMessage, errors } = props;

  return (
    <div className="w-full max-w-sm min-w-[200px]">
      <LabelForInput label={label} />
      <input
        data-testid="numberInputTestId"
        {...(register
          ? register(formLabel, {
              required: requiredMessage,
              min: { value: 0.5, message: "Hours must be at least 0.5 hour" },
              minLength: {
                value: 0.5,
                message: "Hours must be at least 0.5 hour",
              },
            })
          : {})}
        className="w-full bg-transparent placeholder:text-slate-400 text-slate-700 text-sm border border-slate-200 rounded-md px-3 py-2 transition duration-300 ease focus:outline-none focus:border-slate-400 hover:border-slate-300 shadow-sm focus:shadow"
        placeholder="Enter amount of hours, minimum 0.5"
        type="number"
        onWheel={(e) => e.currentTarget.blur()}
        onKeyDown={(e) => {
          if (e.key === "-" || e.key === "e") {
            e.preventDefault();
          }
        }}
      />
      {errors && (
        <p data-testid="errorNumberInputId" className="text-red-500">
          {errors.message}
        </p>
      )}
    </div>
  );
};
