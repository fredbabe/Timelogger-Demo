import { useEffect, useState } from "react";
import { Project } from "../../client/generated/api-client";
import { useRegistrations } from "../../hooks/useRegistrations";
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
  const [collapseRegistrations, setCollapseRegistration] =
    useState<boolean>(false);

  const {
    data,
    refetch,
    isLoading: isLoadingRegistrations,
    isError,
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

  // Set loading state for component
  useEffect(() => {
    if (
      isLoadingRegistrations ||
      isPendingCompletProject ||
      isPendingOpenProject
    ) {
      setIsLoading(true);
    } else {
      setIsLoading(false);
    }
  }, [isLoadingRegistrations, isPendingCompletProject, isPendingOpenProject]);

  // Refetch projects if complete project was success
  useEffect(() => {
    if (
      isSuccessCompletingProject ||
      isSuccesOpenProject ||
      isSuccessDeleteProject
    ) {
      refetchProjcts();
    }
  }, [isSuccessCompletingProject, isSuccesOpenProject, isSuccessDeleteProject]);

  return (
    <div className="bg-gray-200 p-2 rounded-lg shadow-md w-[400px] flex flex-col space-y-4 mb-10">
      <div className="flex flex-row w-full justify-between">
        <p className="text-gray-500">Project:</p>

        <Button
          className={clsx(
            "max-w-[100px] bg-red-500 ",
            !isPendingCompletProject && "hover:bg-red-400"
          )}
          label={"Delete"}
          onClick={async () => await deleteProject(project.id)}
          isLoading={isPendingDeleteProject}
          disable={isPendingDeleteProject}
        ></Button>
      </div>
      <p className="text-3xl font-semibold w-full">{project.name}</p>
      <div className="mt-4 mb-10 space-y-2 font-medium text-lg">
        <p>Status: {project.isCompleted ? "Completed" : "In progress"}</p>
        <p>Created: {project.createdOn.toDateString()}</p>
        <p>Customer: {project.customer.name}</p>
        <p>Deadline: {project.deadline.toDateString()}</p>
      </div>

      <div>
        {data && (
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
          data?.map((x, index) => {
            return (
              <div
                key={index}
                className="flex flex-col font-medium text-lg border p-2 mb-4 rounded-md border-gray-600"
              >
                <p>Description: {x.description}</p>
                <p>Registration Date: {x.registrationDate.toDateString()}</p>
                <p>Hours Worked: {x.hoursWorked}</p>
              </div>
            );
          })}
        {!collapseRegistrations && data?.length === 0 && (
          <p className="pl-2 font-medium">No registration made</p>
        )}
      </div>
      <div className="flex flex-col items-center space-y-4">
        <Button
          className="w-full"
          label={"View Registrations"}
          onClick={async () => await refetch()}
          isLoading={isLoadingRegistrations}
          disable={isLoading}
        ></Button>
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
