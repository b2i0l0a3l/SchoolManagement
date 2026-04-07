import { useMutation, useQueryClient } from "@tanstack/react-query";
import api from "@/features/auth/api/api";
import { Parent } from "../types";

export type CreateParentDto = Omit<Parent, "id">;

export const createParent = async (data: CreateParentDto) => {
  const response = await api.post("/Parent/AddParent", data);
  return response.data;
};

export const useCreateParent = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createParent,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["parents"] });
    },
  });
};

export const deleteParent = async (id: number) => {
  const response = await api.delete(`/Parent/DeleteParent`, { params: { id } });
  return response.data;
};

export const useDeleteParent = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteParent,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["parents"] });
    },
  });
};
