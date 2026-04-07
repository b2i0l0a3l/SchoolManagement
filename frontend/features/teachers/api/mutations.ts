import { useMutation, useQueryClient } from "@tanstack/react-query";
import api from "@/features/auth/api/api";
import { Teacher } from "../types";

export type CreateTeacherDto = Omit<Teacher, "id">;

export const createTeacher = async (data: CreateTeacherDto) => {
  const response = await api.post("/Teacher/AddNewTeacher", data);
  return response.data;
};

export const useCreateTeacher = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createTeacher,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["teachers"] });
    },
  });
};

export const deleteTeacher = async (id: number) => {
  const response = await api.delete(`/Teacher/DeleteTeacher`, { params: { id } });
  return response.data;
};

export const useDeleteTeacher = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteTeacher,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["teachers"] });
    },
  });
};
