import { useEffect, useState } from "react";
import {
  GetRegistrationsForProjectResponse,
  Project,
} from "../../client/generated/api-client";
import {
  useDeleteRegistrationMutation,
  useRegistrations,
} from "../../hooks/useRegistrations";
import { Button } from "../button/Button";
import {
  useCompleteProjectMutation,
  useDeleteProjectMutation,
  useOpenProjectMutation,
} from "../../hooks/useProjects";
import clsx from "clsx";

export type ProjectCardProps = {
  project: Project;
  refetchProjcts: () => void;
};

export const ProjectCard = (props: ProjectCardProps) => {
  const { project, refetchProjcts } = props;

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [registrations, setRegistrations] = useState<
    GetRegistrationsForProjectResponse[]
  >([]);
  const [totalWorkingTime, setTotalWorkingTime] = useState<number | undefined>(
    undefined
  );
  const [collapseRegistrations, setCollapseRegistration] =
    useState<boolean>(false);

  const {
    data: registrationData,
    isLoading: isLoadingRegistrations,
    isError,
    isSuccess: isSuccessLoadingRegistrations,
  } = useRegistrations(project.id);

  const {
    mutateAsync: completeProject,
    isPending: isPendingCompletProject,
    isSuccess: isSuccessCompletingProject,
  } = useCompleteProjectMutation();

  const {
    mutateAsync: openProject,
    isPending: isPendingOpenProject,
    isSuccess: isSuccesOpenProject,
  } = useOpenProjectMutation();

  const {
    mutateAsync: deleteProject,
    isPending: isPendingDeleteProject,
    isSuccess: isSuccessDeleteProject,
  } = useDeleteProjectMutation();

  const {
    mutateAsync: deleteRegistration,
    isPending: isPendingDeleteRegistration,
    isSuccess: isSuccesDeleteRegistration,
  } = useDeleteRegistrationMutation();

  // Set loading state for component
  useEffect(() => {
    if (
      isLoadingRegistrations ||
      isPendingCompletProject ||
      isPendingOpenProject ||
      isPendingDeleteProject ||
      isPendingDeleteRegistration
    ) {
      setIsLoading(true);
    } else {
      setIsLoading(false);
    }
  }, [
    isLoadingRegistrations,
    isPendingCompletProject,
    isPendingDeleteProject,
    isPendingDeleteRegistration,
    isPendingOpenProject,
  ]);

  // Refetch projects if complete project was success
  useEffect(() => {
    if (
      isSuccessCompletingProject ||
      isSuccesOpenProject ||
      isSuccessDeleteProject ||
      isSuccesDeleteRegistration
    ) {
      refetchProjcts();
    }
  }, [
    isSuccessCompletingProject,
    isSuccesOpenProject,
    isSuccessDeleteProject,
    refetchProjcts,
    isSuccesDeleteRegistration,
  ]);

  useEffect(() => {
    setRegistrations(registrationData ?? []);
  }, [project, registrationData]);

  useEffect(() => {
    if (registrations.length > 0) {
      let totalWorkingHours = 0;

      registrations.forEach((x) => {
        totalWorkingHours += x.hoursWorked;
      });

      setTotalWorkingTime(totalWorkingHours);
    } else {
      setTotalWorkingTime(undefined);
    }
  }, [registrations]);

  return (
    <div className="bg-gray-200 p-2 rounded-lg shadow-md w-[400px] flex flex-col space-y-4 mb-10">
      <div className="flex flex-row w-full justify-between">
        <p className="text-gray-500">Project:</p>

        <Button
          className={clsx(
            "max-w-[100px] bg-red-500",
            !isPendingCompletProject && "hover:bg-red-400"
          )}
          label={"Delete"}
          onClick={async () => await deleteProject(project.id)}
          isLoading={isPendingDeleteProject}
          disable={isLoading}
        ></Button>
      </div>
      <p className="text-3xl font-semibold w-full">{project.name}</p>
      <div className="mt-4 mb-10 space-y-2 font-medium text-lg">
        <p>Status: {project.isCompleted ? "Completed" : "In progress"}</p>
        <p>Created: {project.createdOn.toDateString()}</p>
        <p>Customer: {project.customer.name}</p>
        <p>Deadline: {project.deadline.toDateString()}</p>
        {totalWorkingTime && <p>Total working hours: {totalWorkingTime}</p>}
      </div>

      <div>
        {registrations.length > 0 && (
          <button
            className="p-2 cursor-pointer underline mb-2"
            onClick={() => setCollapseRegistration(!collapseRegistrations)}
          >
            {!collapseRegistrations
              ? "Hide registrations"
              : "Show registrations"}
          </button>
        )}
        {!collapseRegistrations &&
          registrations?.map((x, index) => {
            return (
              <div
                key={index}
                className="flex flex-col font-medium text-lg border p-2 mb-4 rounded-md border-gray-600"
              >
                <Button
                  className="max-w-[70px] max-h-[20px] bg-red-500 text-sm p-1"
                  onClick={async () => {
                    await deleteRegistration(x.id);
                  }}
                  isLoading={isPendingDeleteRegistration}
                  disable={isLoading}
                  label="Delete"
                />
                <p>Description: {x.description}</p>
                <p>Registration Date: {x.registrationDate.toDateString()}</p>
                <p>Hours Worked: {x.hoursWorked}</p>
              </div>
            );
          })}
        {!collapseRegistrations &&
          isSuccessLoadingRegistrations &&
          registrations?.length === 0 && (
            <p className="pl-2 font-medium">No registration made</p>
          )}
      </div>
      <div className="flex flex-col items-center space-y-4">
        <Button
          className="w-full"
          label={project.isCompleted ? "Open project" : "Complete project"}
          onClick={async () =>
            project.isCompleted
              ? await openProject(project.id)
              : await completeProject(project.id)
          }
          isLoading={isPendingCompletProject || isPendingOpenProject}
          disable={isLoading}
        ></Button>
      </div>

      {isError && (
        <p data-testid="errorNumberInputId" className="text-red-500">
          Der skete en fejl ved indhentning af projeker..
        </p>
      )}
    </div>
  );
};
