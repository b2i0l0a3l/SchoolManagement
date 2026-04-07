import { useQuery } from "@tanstack/react-query";
import api from "@/features/auth/api/api";
import { Student } from "../types";

export const getStudents = async (): Promise<Student[]> => {
  const { data } = await api.get("/Student/GetAllStudents");
  return data;
};
export const useStudents = () => {
  return useQuery({
    queryKey: ["students"],
    queryFn: getStudents,
  });
};
