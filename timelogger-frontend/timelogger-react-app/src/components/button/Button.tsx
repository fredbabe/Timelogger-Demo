import clsx from "clsx";
import { Spinner } from "../spinner/Spinner";

export type ButtonProps = {
  label: string;
  isLoading: boolean;
  onClick?: () => void;
  disable: boolean;
  className?: string;
};

export const Button = (props: ButtonProps) => {
  const { label, isLoading, onClick, disable, className } = props;
  return (
    <button
      onClick={onClick}
      type="submit"
      disabled={disable}
      className={clsx(
        className,
        disable && "bg-gray-400",
        !disable && "hover:bg-gray-700 cursor-pointer ",
        "px-[5px] py-[16px] w-[200px] h-[40px] bg-black text-white rounded-md text-center justify-center font-medium flex items-center"
      )}
    >
      <p className="mr-2">{label}</p>
      {isLoading && <Spinner />}
    </button>
  );
};
