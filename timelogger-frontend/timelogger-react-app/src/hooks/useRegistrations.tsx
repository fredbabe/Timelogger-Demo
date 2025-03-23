import { useMutation, useQuery } from "@tanstack/react-query";
import { apiClient } from "../client/apiClient";
import { CreateRegistrationDTORequest } from "../client/generated/api-client";

const useRegistrations = (projectId?: string) => {
  return useQuery({
    refetchOnMount: false,
    refetchOnWindowFocus: false,
    retry: false,
    enabled: false,
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

export { useRegistrations, useCreateRegistrationMutation };
