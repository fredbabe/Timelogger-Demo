import { useMutation, useQuery } from "@tanstack/react-query";
import { apiClient } from "../client/apiClient";
import { CreateRegistrationDTORequest } from "../client/generated/api-client";

const useRegistrations = (projectId?: string) => {
  return useQuery({
    refetchOnMount: true,
    refetchOnWindowFocus: false,
    retry: false,
    queryKey: ["registrations", projectId],
    queryFn: async () => {
      const response = await apiClient.getRegistrationsOfProject(
        projectId ?? ""
      );
      return response;
    },
  });
};

const useCreateRegistrationMutation = () => {
  return useMutation({
    mutationFn: async (formData: CreateRegistrationDTORequest) => {
      await apiClient.createRegistration(formData);
    },
  });
};

const useDeleteRegistrationMutation = () => {
  return useMutation({
    mutationFn: async (registrationId: string) => {
      apiClient.deleteRegistration(registrationId);
    },
  });
};

export {
  useRegistrations,
  useCreateRegistrationMutation,
  useDeleteRegistrationMutation,
};
