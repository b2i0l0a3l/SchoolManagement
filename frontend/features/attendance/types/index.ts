// 0 = Present, 1 = Absent, 2 = Late
export type AttendanceStatus = 0 | 1 | 2;

export interface Attendance {
  id: number;
  studentId: number;
  date: string;
  status: AttendanceStatus;
  remarks: string;
}

export const statusLabels: Record<AttendanceStatus, string> = {
  0: "حاضر",
  1: "غائب",
  2: "تأخير",
};

export const statusColors: Record<AttendanceStatus, string> = {
  0: "bg-green-100 text-green-700",
  1: "bg-red-100 text-red-700",
  2: "bg-orange-100 text-orange-700",
};
