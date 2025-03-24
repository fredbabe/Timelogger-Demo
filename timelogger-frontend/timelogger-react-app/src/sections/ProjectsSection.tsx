import { useEffect, useState } from "react";
import { ProjectCard } from "../components/projectCard/ProjectCard";
import { Spinner } from "../components/spinner/Spinner";
import { useProjects } from "../hooks/useProjects";
import { Project } from "../client/generated/api-client";

export const ProjectsSection = () => {
  const { data, isFetching, isError, refetch, isSuccess } = useProjects();
  const [sortType, setSortType] = useState<"latestDeadline" | "newestDeadline">(
    "latestDeadline"
  );
  const [sortedProjects, setSortedProjects] = useState<Project[]>([]);

  // Sort project funcionality
  useEffect(() => {
    if (data) {
      const sortedProjects = [...data];

      if (sortType === "latestDeadline") {
        // Sort projects with the nearest upcoming deadline
        sortedProjects.sort(
          (a, b) =>
            new Date(a.deadline).getTime() - new Date(b.deadline).getTime()
        );
      } else if (sortType === "newestDeadline") {
        // Sort by most recently created projects first
        sortedProjects.sort(
          (a, b) =>
            new Date(b.deadline).getTime() - new Date(a.deadline).getTime()
        );
      }

      setSortedProjects(sortedProjects);
    }
  }, [data, sortType]);

  return (
    <div>
      {isFetching ? (
        <Spinner />
      ) : (
        <>
          {data && data.length > 0 && (
            <div className="flex gap-4 flex-col text-center mb-10">
              <p className="text-lg font-medium">Sort projects</p>
              <div className="flex flex-row space-x-2 justify-center">
                <button
                  onClick={() => setSortType("latestDeadline")}
                  className={`px-4 py-2 rounded cursor-pointer ${
                    sortType === "latestDeadline"
                      ? "bg-blue-500 text-white"
                      : "bg-gray-200"
                  }`}
                >
                  Closest deadline
                </button>
                <button
                  onClick={() => setSortType("newestDeadline")}
                  className={`px-4 py-2 rounded cursor-pointer ${
                    sortType === "newestDeadline"
                      ? "bg-blue-500 text-white"
                      : "bg-gray-200"
                  }`}
                >
                  Farthest Deadline
                </button>
              </div>
            </div>
          )}

          {/* Render the sorted projects */}
          {data &&
            sortedProjects.map((x, index) => (
              <ProjectCard key={index} refetchProjcts={refetch} project={x} />
            ))}

          {/* Error message */}
          {isError && (
            <p data-testid="errorNumberInputId" className="text-red-500">
              Failed to fetch projects ..
            </p>
          )}

          {isSuccess && data && data.length === 0 && (
            <p className="font-medium text-lg">
              No project exists, please create one
            </p>
          )}
        </>
      )}
    </div>
  );
};
