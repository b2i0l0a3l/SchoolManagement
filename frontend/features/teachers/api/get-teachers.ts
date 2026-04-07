import { useQuery } from '@tanstack/react-query';
import api from '@/features/auth/api/api';
import { Teacher } from '../types';

export const getTeachers = async (): Promise<Teacher[]> => {
  const { data } = await api.get('/Teacher/GetAllTeachers');
  return data;
};

export const useTeachers = () => {
  return useQuery({
    queryKey: ['teachers'],
    queryFn: getTeachers,
  });
};
