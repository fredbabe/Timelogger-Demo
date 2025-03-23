import { FieldError, FieldValues, Path, UseFormRegister } from "react-hook-form";
import { LabelForInput } from "../labelForInput/LabelForInput";

export type TextInputProps<T extends FieldValues> = {
  label: string;
  register?: UseFormRegister<T>;
  formLabel: Path<T>;
  requiredMessage?: string;
  errors?: FieldError;
};

export const TextInput = <T extends FieldValues,>(props: TextInputProps<T>) => {
  const { label, register, formLabel, requiredMessage, errors } = props;

  return (
    <div className="w-full max-w-sm min-w-[200px]">
      <LabelForInput label={label} />
      <input
        data-testid="TextInputTestId"
        autoComplete="off"
        {...(register ? register(formLabel, { required: requiredMessage }) : {})}
        className="w-full bg-transparent placeholder:text-slate-400 text-slate-700 text-sm border border-slate-200 rounded-md px-3 py-2 transition duration-300 ease focus:outline-none focus:border-slate-400 hover:border-slate-300 shadow-sm focus:shadow"
        placeholder="Type here..."
      />
      {errors && (
        <p data-testid="errorTextInputId" className="text-red-500">
          {errors.message}
        </p>
      )}
    </div>
  );
};