import { useQuery } from "@tanstack/react-query";
import { apiClient } from "../client/apiClient";

const useCustomers = () => {
  return useQuery({
    refetchOnMount: false,
    refetchOnWindowFocus: false,
    retry: false,
    queryKey: ["customers"],
    queryFn: async () => {
      const response = await apiClient.getAllCustomers();
      return response;
    },
  });
};

export { useCustomers };
