"use client";

import { useState } from "react";
import { useAttendance } from "../api/get-attendance";
import { useCreateAttendance, useDeleteAttendance, CreateAttendanceDto } from "../api/mutations";
import { CalendarDays, Trash2, Plus } from "lucide-react";
import { DataTable, ColumnDef } from "@/components/ui/DataTable";
import { Button } from "@/components/ui/Button";
import { Modal } from "@/components/ui/Modal";
import { ConfirmModal } from "@/components/ui/ConfirmModal";
import { AttendanceForm } from "./AttendanceForm";
import { Attendance, statusLabels, statusColors, AttendanceStatus } from "../types";
import toast from "react-hot-toast";

export default function AttendanceTable() {
  const { data: attendance = [], isLoading, isError } = useAttendance();
  const { mutate: createAttendance, isPending: isCreating } = useCreateAttendance();
  const { mutate: deleteAttendance, isPending: isDeleting } = useDeleteAttendance();

  const [isFormOpen, setIsFormOpen] = useState(false);
  const [deleteId, setDeleteId] = useState<number | null>(null);

  const handleCreate = (data: CreateAttendanceDto) => {
    createAttendance(data, {
      onSuccess: () => {
        toast.success("تم تسجيل الحضور بنجاح");
        setIsFormOpen(false);
      },
      onError: (err: any) => {
        toast.error(err.response?.data?.title || "فشلت عملية التسجيل");
      }
    });
  };

  const handleDelete = () => {
    if (deleteId === null) return;
    deleteAttendance(deleteId, {
      onSuccess: () => {
        toast.success("تم حذف السجل بنجاح");
        setDeleteId(null);
      },
      onError: () => {
        toast.error("فشل في عملية الحذف");
        setDeleteId(null);
      }
    });
  };


  const columns: ColumnDef<Attendance>[] = [
    { 
      header: "الرقم", 
      cell: (row) => <span className="font-medium text-gray-900">#{row.id}</span> 
    },
    { 
      header: "رقم الطالب", 
      cell: (row) => <span className="font-semibold text-gray-800">#{row.studentId}</span> 
    },
    { 
      header: "التاريخ", 
      cell: (row) => <span dir="ltr">{new Date(row.date).toLocaleDateString()}</span> 
    },
    { 
      header: "الحالة", 
      cell: (row) => (
        <span className={`px-3 py-1 rounded-full text-xs font-medium ${statusColors[row.status as AttendanceStatus] ?? "bg-gray-100 text-gray-700"}`}>
          {statusLabels[row.status as AttendanceStatus] ?? "غير معروف"}
        </span>
      ) 
    },
    { 
      header: "ملاحظات", 
      cell: (row) => <span className="text-gray-500">{row.remarks || "-"}</span> 
    },
    { 
      header: "إجراءات", 
      cell: (row) => (
        <div className="flex items-center gap-2">
          <button 
            onClick={() => setDeleteId(row.id)}
            className="text-red-400 hover:text-red-600 transition-colors p-1 rounded-md hover:bg-red-50 border border-transparent"
            title="حذف"
          >
            <Trash2 className="h-4 w-4" />
          </button>
        </div>
      ) 
    },
  ];

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h2 className="text-2xl font-bold text-gray-900 flex items-center gap-2">
          <CalendarDays className="h-6 w-6 text-primary" />
          الحضور والغياب
        </h2>
        <Button onClick={() => setIsFormOpen(true)}>
          <Plus className="h-4 w-4 mr-2" />
          تسجيل حضور
        </Button>
      </div>

      <DataTable
        data={attendance}
        columns={columns}
        isLoading={isLoading}
        searchPlaceholder="ابحث برقم الطالب..."
        searchKey="studentId"
      />

      <Modal 
        isOpen={isFormOpen} 
        onClose={() => !isCreating && setIsFormOpen(false)} 
        title="تسجيل حضور جديد"
      >
        <AttendanceForm onSubmit={handleCreate} isLoading={isCreating} />
      </Modal>

      <ConfirmModal
        isOpen={deleteId !== null}
        onClose={() => !isDeleting && setDeleteId(null)}
        onConfirm={handleDelete}
        title="حذف سجل الحضور"
        message="هل أنت متأكد من رغبتك في حذف هذا السجل؟"
        isLoading={isDeleting}
      />
    </div>
  );
}
