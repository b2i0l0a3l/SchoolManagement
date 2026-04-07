import { useQuery } from '@tanstack/react-query';
import api from '@/features/auth/api/api';
import { Exam } from '../types';

export const getExams = async (): Promise<Exam[]> => {
  const { data } = await api.get('/Exam/GetAll');
  return data;
};

export const useExams = () => {
  return useQuery({
    queryKey: ['exams'],
    queryFn: getExams,
  });
};
