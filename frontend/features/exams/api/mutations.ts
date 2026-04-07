import { useMutation, useQueryClient } from "@tanstack/react-query";
import api from "@/features/auth/api/api";
import { Exam } from "../types";

export type CreateExamDto = Omit<Exam, "id">;

export const createExam = async (data: CreateExamDto) => {
  const response = await api.post("/Exam/AddExam", data);
  return response.data;
};

export const useCreateExam = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createExam,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["exams"] });
    },
  });
};

export const deleteExam = async (id: number) => {
  const response = await api.delete(`/Exam/DeleteExam`, { params: { id } });
  return response.data;
};

export const useDeleteExam = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteExam,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["exams"] });
    },
  });
};
