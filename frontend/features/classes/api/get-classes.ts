import { useQuery } from '@tanstack/react-query';
import api from '@/features/auth/api/api';
import { Class } from '../types';

export const getClasses = async (): Promise<Class[]> => {
  const { data } = await api.get('/Class/GetAllClasses');
  return data;
};

export const useClasses = () => {
  return useQuery({
    queryKey: ['classes'],
    queryFn: getClasses,
  });
};
