"use client";

import { useState } from "react";
import { useTeachers } from "../api/get-teachers";
import { useCreateTeacher, useDeleteTeacher, CreateTeacherDto } from "../api/mutations";
import { Users, Trash2, Plus } from "lucide-react";
import { DataTable, ColumnDef } from "@/components/ui/DataTable";
import { Button } from "@/components/ui/Button";
import { Modal } from "@/components/ui/Modal";
import { ConfirmModal } from "@/components/ui/ConfirmModal";
import { TeacherForm } from "./TeacherForm";
import { Teacher } from "../types";
import toast from "react-hot-toast";

export default function TeacherTable() {
  const { data: teachers = [], isLoading, isError } = useTeachers();
  const { mutate: createTeacher, isPending: isCreating } = useCreateTeacher();
  const { mutate: deleteTeacher, isPending: isDeleting } = useDeleteTeacher();

  const [isFormOpen, setIsFormOpen] = useState(false);
  const [deleteId, setDeleteId] = useState<number | null>(null);

  const handleCreate = (data: CreateTeacherDto) => {
    createTeacher(data, {
      onSuccess: () => {
        toast.success("تم إضافة المعلم بنجاح");
        setIsFormOpen(false);
      },
      onError: (err: any) => {
        toast.error(err.response?.data?.title || "فشلت عملية الإضافة");
      }
    });
  };

  const handleDelete = () => {
    if (deleteId === null) return;
    deleteTeacher(deleteId, {
      onSuccess: () => {
        toast.success("تم حذف المعلم بنجاح");
        setDeleteId(null);
      },
      onError: () => {
        toast.error("فشل في عملية الحذف");
        setDeleteId(null);
      }
    });
  };

  if (isError) {
    return (
      <div className="flex h-64 items-center justify-center text-red-500">
        خطأ في جلب بيانات المعلمين.
      </div>
    );
  }

  const columns: ColumnDef<Teacher>[] = [
    { 
      header: "الرقم", 
      cell: (row) => <span className="font-medium text-gray-900">#{row.id}</span> 
    },
    { 
      header: "الاسم", 
      cell: (row) => <span className="font-semibold text-gray-800">{row.fullName}</span> 
    },
    { 
      header: "تاريخ التعيين", 
      cell: (row) => <span dir="ltr">{new Date(row.hireDate).toLocaleDateString()}</span> 
    },
    { 
      header: "رقم القسم", 
      cell: (row) => (
        <span className="inline-flex items-center rounded-md bg-blue-50 px-2 py-1 text-xs font-medium text-blue-700 ring-1 ring-inset ring-blue-700/10">
          Dept #{row.departmentId}
        </span>
      ) 
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
          <Users className="h-6 w-6 text-primary" />
          إدارة المعلمين
        </h2>
        <Button onClick={() => setIsFormOpen(true)}>
          <Plus className="h-4 w-4 mr-2" />
          إضافة معلم
        </Button>
      </div>

      <DataTable
        data={teachers}
        columns={columns}
        isLoading={isLoading}
        searchPlaceholder="ابحث باسم المعلم..."
        searchKey="fullName"
      />

      <Modal 
        isOpen={isFormOpen} 
        onClose={() => !isCreating && setIsFormOpen(false)} 
        title="إضافة معلم جديد"
      >
        <TeacherForm onSubmit={handleCreate} isLoading={isCreating} />
      </Modal>

      <ConfirmModal
        isOpen={deleteId !== null}
        onClose={() => !isDeleting && setDeleteId(null)}
        onConfirm={handleDelete}
        title="حذف المعلم"
        message="هل أنت متأكد من رغبتك في حذف هذا المعلم؟"
        isLoading={isDeleting}
      />
    </div>
  );
}
