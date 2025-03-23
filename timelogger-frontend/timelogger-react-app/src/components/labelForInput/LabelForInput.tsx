export type LabelInputProps = {
  label: string;
};

export const LabelForInput = (props: LabelInputProps) => {
  const { label } = props;
  return (
    <label className="block mb-1 text-sm font-bold text-slate-600">
      {label}
    </label>
  );
};
