import { useMutation, useQueryClient } from "@tanstack/react-query";
import api from "@/features/auth/api/api";
import { Attendance } from "../types";

export type CreateAttendanceDto = Omit<Attendance, "id">;

export const createAttendance = async (data: CreateAttendanceDto) => {
  const response = await api.post("/Attendance/AddAttendance", data);
  return response.data;
};

export const useCreateAttendance = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createAttendance,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["attendance"] });
    },
  });
};

export const deleteAttendance = async (id: number) => {
  const response = await api.delete(`/Attendance/DeleteAttendance`, { params: { id } });
  return response.data;
};

export const useDeleteAttendance = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteAttendance,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["attendance"] });
    },
  });
};
