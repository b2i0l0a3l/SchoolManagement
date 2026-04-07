import { useMutation, useQueryClient } from "@tanstack/react-query";
import api from "@/features/auth/api/api";
import { Class } from "../types";

export type CreateClassDto = Omit<Class, "id">;

export const createClass = async (data: CreateClassDto) => {
  const response = await api.post("/Class/AddNewClass", data);
  return response.data;
};

export const useCreateClass = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createClass,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["classes"] });
    },
  });
};

export const deleteClass = async (id: number) => {
  const response = await api.delete(`/Class/DeleteClass`, { params: { id } });
  return response.data;
};

export const useDeleteClass = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteClass,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["classes"] });
    },
  });
};
