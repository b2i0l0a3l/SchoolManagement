import { useQuery } from '@tanstack/react-query';
import api from '@/features/auth/api/api';
import { Parent } from '../types';

export const getParents = async (): Promise<Parent[]> => {
  const { data } = await api.get('/Parent/GetAll');
  return data;
};

export const useParents = () => {
  return useQuery({
    queryKey: ['parents'],
    queryFn: getParents,
  });
};
