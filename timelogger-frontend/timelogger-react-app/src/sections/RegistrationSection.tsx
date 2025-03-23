import { Button } from "../components/button/Button";
import { ReactDayPicker } from "../components/dayPicker/DayPicker";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useProjects } from "../hooks/useProjects";
import { CreateRegistrationDTORequest } from "../client/generated/api-client";
import { SelectInput } from "../components/selectInput/SelectInput";
import { TextInput } from "../components/textInput/TextInput";
import { NumberInput } from "../components/numberInput/NumberInput";
import { useCreateRegistrationMutation } from "../hooks/useRegistrations";
import { Spinner } from "../components/spinner/Spinner";

export type FormDataRegistration = {
  projectId: string;
  registrationDate: Date;
  description: string;
  workingHours: number;
};

export const RegistrationSection = () => {
  const {
    register,
    setValue,
    handleSubmit,
    watch,
    setError,
    clearErrors,
    formState: { errors },
  } = useForm<FormDataRegistration>();

  const [anyProjectOpen, setAnyProjectOpen] = useState<boolean>(true);

  // Watch registrationDate
  const registrationDate = watch("registrationDate");
  const { data: projects, isLoading: isLoadingProjects } = useProjects();
  const {
    mutate: createRegistration,
    isSuccess,
    isError,
    isPending,
  } = useCreateRegistrationMutation();

  useEffect(() => {
    if (registrationDate) {
      clearErrors("registrationDate");
    }
  }, [registrationDate]);

  const onSubmit = async (data: FormDataRegistration) => {
    // Need to handle manually setting registrationDate error
    // due to not being able to register it to react-hook-form
    if (!registrationDate) {
      setError("registrationDate", {
        type: "manual",
        message: "Venligst udfyld dato for registrering",
      });
      return; // Stop submission if error
    }

    var request = {
      description: data.description,
      hoursWorked: data.workingHours,
      projectId: data.projectId,
      registrationDate: data.registrationDate,
    } as CreateRegistrationDTORequest;

    await createRegistration(request);
  };

  // Let the user know if no project is open for registrations
  useEffect(() => {
    const hasOpenProject = projects?.some((x) => !x.isCompleted);
    if (hasOpenProject) {
      setAnyProjectOpen(hasOpenProject);
    } else {
      setAnyProjectOpen(false);
    }
  }, [projects]);

  return (
    <>
      {isLoadingProjects ? (
        <Spinner />
      ) : (
        <>
          {anyProjectOpen ? (
            <form
              className="w-full items-center justify-center mx-auto flex flex-col space-y-10 h-full"
              onSubmit={handleSubmit(onSubmit)}
              noValidate
            >
              <SelectInput
                register={register}
                formLabel="projectId"
                label="Select project"
                options={projects
                  ?.filter((x) => !x.isCompleted)
                  .map((x) => ({ id: x.id, displayName: x.name ?? "" }))}
                required={true}
              />
              <TextInput
                register={register}
                formLabel="description"
                label="Add short description"
                requiredMessage="Please fill out the description"
                errors={errors.description}
              />

              <ReactDayPicker
                setDate={(date) => setValue("registrationDate", date)}
                label="Select registration date"
                value={watch("registrationDate")}
                errors={errors.registrationDate}
                requiredMessage="Please fill set the registration date"
              />
              <NumberInput
                formLabel="workingHours"
                register={register}
                label="Working hours"
                errors={errors.workingHours}
                requiredMessage="Please set the working hours"
              />
              <Button
                disable={isPending}
                isLoading={isPending}
                label="Create Registration"
              />

              {isSuccess && <p>Registration created successfully!</p>}
              {isError && (
                <p className="text-red-500">
                  Failed to create registration. Please try again.
                </p>
              )}
            </form>
          ) : (
            <p className="font-medium text-lg">
              No open projects to register time on. Please create a new one or
              open an existing
            </p>
          )}
        </>
      )}
    </>
  );
};
