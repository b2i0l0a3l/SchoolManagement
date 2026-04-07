import { useQuery } from '@tanstack/react-query';
import api from '@/features/auth/api/api';
import { Attendance } from '../types';

export const getAttendance = async (): Promise<Attendance[]> => {
  const { data } = await api.get('/Attendance/GetAll');
  return data;
};

export const useAttendance = () => {
  return useQuery({
    queryKey: ['attendance'],
    queryFn: getAttendance,
  });
};
