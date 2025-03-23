import { useState } from "react";
import { DayPicker } from "react-day-picker";
import "react-day-picker/style.css";
import { Calendar } from "lucide-react";
import { FieldError } from "react-hook-form";
import { LabelForInput } from "../labelForInput/LabelForInput";

export type ReactDayPickerProps = {
  label: string;
  value?: Date;
  requiredMessage?: string;
  errors?: FieldError;
  setDate: (date: Date) => void;
};

export const ReactDayPicker = (props: ReactDayPickerProps) => {
  const { label, value, errors, setDate } = props;
  const [showDatePicker, setShowDatePicker] = useState<boolean>(false);

  return (
    <div className="w-full max-w-sm min-w-[200px]">
      <LabelForInput label={label} />

      <div
        className="w-full bg-transparent placeholder:text-slate-400 text-slate-700 text-sm border border-slate-200 rounded-md px-3 py-2 transition duration-300 ease focus:outline-none focus:border-slate-400 hover:border-slate-300 shadow-sm focus:shadow"
        onClick={() => setShowDatePicker(!showDatePicker)}
      >
        {value ? (
          <p>{value.toLocaleDateString()}</p>
        ) : (
          <div className="flex flex-row space-x-2 items-center">
            <p className="text-slate-400">Pick a date</p>
            <Calendar size={16} strokeWidth={1} />
          </div>
        )}
      </div>
      {showDatePicker && (
        <DayPicker
          animate
          mode="single"
          timeZone="UTC"
          selected={value}
          onSelect={(date) => {
            if (date) {
              setDate(date);
              setShowDatePicker(false);
            }
          }}
          footer={value ? `${value}` : "Pick a day."}
        />
      )}
      {errors && <p className="text-red-500">{errors.message}</p>}
    </div>
  );
};
