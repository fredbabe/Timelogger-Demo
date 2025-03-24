import { useForm } from "react-hook-form";
import { useCustomers } from "../hooks/useCustomers";
import { useCreateProjectMutation } from "../hooks/useProjects";
import { CreateProjectDTORequest } from "../client/generated/api-client";
import { useEffect } from "react";
import { SelectInput } from "../components/selectInput/SelectInput";
import { ReactDayPicker } from "../components/dayPicker/DayPicker";
import { TextInput } from "../components/textInput/TextInput";
import { Button } from "../components/button/Button";
import { Spinner } from "../components/spinner/Spinner";

export type FormDataCreateProject = {
  projectName: string;
  customerId: string;
  description: string;
  deadline: Date;
};

export const CreateProjectSection = () => {
  const {
    register,
    handleSubmit,
    setValue,
    watch,
    setError,
    clearErrors,
    formState: { errors },
  } = useForm<FormDataCreateProject>();

  const {
    data: customers,
    isLoading: isLoadingCustomers,
    isError: isLoadingCustomerError,
  } = useCustomers();

  const {
    mutateAsync,
    isError: isCreatingProjectError,
    isSuccess: isSuccessCreatingProject,
    isPending: isPendingCreatingProject,
  } = useCreateProjectMutation();

  // Watch deadline date
  const deadlineDate = watch("deadline");

  const onSubmit = async (data: FormDataCreateProject) => {
    // Need to handle manually setting registrationDate error
    // due to not being able to register it to react-hook-form
    if (!deadlineDate) {
      setError("deadline", {
        type: "manual",
        message: "Please fill out date for deadline",
      });
      return; // Stop submission if error
    }

    const request = {
      name: data.projectName,
      customerId: data.customerId,
      deadline: data.deadline,
    } as CreateProjectDTORequest;

    await mutateAsync(request);
  };

  // Reset date error
  useEffect(() => {
    if (deadlineDate) {
      clearErrors("deadline");
    }
  }, [deadlineDate]);
  return (
    <>
      {isLoadingCustomers ? (
        <Spinner />
      ) : (
        <>
          {isLoadingCustomerError ? (
            <p className="text-red-500">Something went wrong loading form ..</p>
          ) : (
            <form
              className="w-full items-center justify-center mx-auto flex flex-col space-y-10"
              onSubmit={handleSubmit(onSubmit)}
              noValidate
            >
              <SelectInput
                register={register}
                formLabel="customerId"
                label="Select customer"
                options={customers?.map((x) => ({
                  id: x.id,
                  displayName: x.name ?? "",
                }))}
                required={true}
              />

              <TextInput
                register={register}
                formLabel="projectName"
                label="Add name for project"
                requiredMessage="Please fill out the name for the project"
                errors={errors.projectName}
              />

              <TextInput
                register={register}
                formLabel="description"
                label="Add a description for the project"
                requiredMessage="Please fill out the description for the project"
                errors={errors.description}
              />

              <ReactDayPicker
                label="Select date for deadline"
                setDate={(date) => setValue("deadline", date)}
                errors={errors.deadline}
                requiredMessage="Please fill out date for deadline"
                value={watch("deadline")}
              />

              <Button
                disable={isPendingCreatingProject}
                isLoading={isPendingCreatingProject}
                label="Create Project"
              />

              {isSuccessCreatingProject && <p>Project created successfully!</p>}
              {isCreatingProjectError && (
                <p className="text-red-500">
                  Failed to create customer. Please try again.
                </p>
              )}
            </form>
          )}
        </>
      )}
    </>
  );
};
