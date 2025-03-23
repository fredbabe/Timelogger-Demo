import { useMutation, useQuery } from "@tanstack/react-query";
import { apiClient } from "../client/apiClient";
import { CreateProjectDTORequest } from "../client/generated/api-client";

const useProjects = () => {
  return useQuery({
    refetchOnMount: true,
    refetchOnWindowFocus: false,
    retry: false,
    queryKey: ["projects"],
    queryFn: async () => {
      const response = await apiClient.getAllProjects();
      return response;
    },
  });
};

const useCompleteProjectMutation = () => {
  return useMutation({
    mutationFn: async (projectId: string) => {
      await apiClient.completeProject(projectId);
    },
  });
};

const useOpenProjectMutation = () => {
  return useMutation({
    mutationFn: async (projectId: string) => {
      await apiClient.openProject(projectId);
    },
  });
};

const useCreateProjectMutation = () => {
  return useMutation({
    mutationFn: async (request: CreateProjectDTORequest) => {
      await apiClient.createProject(request);
    },
  });
};

const useDeleteProjectMutation = () => {
  return useMutation({
    mutationFn: async (projectId: string) => {
      await apiClient.deleteProject(projectId);
    },
  });
};

export {
  useProjects,
  useCompleteProjectMutation,
  useOpenProjectMutation,
  useCreateProjectMutation,
  useDeleteProjectMutation,
};
