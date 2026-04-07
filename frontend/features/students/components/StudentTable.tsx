"use client";

import { useState } from "react";
import { useStudents } from "../api/get-students";
import { useCreateStudent, useDeleteStudent, CreateStudentDto } from "../api/mutations";
import { GraduationCap, Trash2, Plus } from "lucide-react";
import { DataTable, ColumnDef } from "@/components/ui/DataTable";
import { Button } from "@/components/ui/Button";
import { Modal } from "@/components/ui/Modal";
import { ConfirmModal } from "@/components/ui/ConfirmModal";
import { StudentForm } from "./StudentForm";
import { Student } from "../types";
import toast from "react-hot-toast";

export default function StudentTable() {
  const { data: students = [], isLoading, isError } = useStudents();
  const { mutate: createStudent, isPending: isCreating } = useCreateStudent();
  const { mutate: deleteStudent, isPending: isDeleting } = useDeleteStudent();

  const [isFormOpen, setIsFormOpen] = useState(false);
  const [deleteId, setDeleteId] = useState<number | null>(null);

  const handleCreate = (data: CreateStudentDto) => {
    createStudent(data, {
      onSuccess: () => {
        toast.success("تم إضافة الطالب بنجاح");
        setIsFormOpen(false);
      },
      onError: (err: any) => {
        toast.error(err.response?.data?.title || "فشلت عملية الإضافة");
      }
    });
  };

  const handleDelete = () => {
    if (deleteId === null) return;
    deleteStudent(deleteId, {
      onSuccess: () => {
        toast.success("تم حذف الطالب بنجاح");
        setDeleteId(null);
      },
      onError: (err: any) => {
        toast.error("فشل في عملية الحذف");
        setDeleteId(null);
      }
    });
  };


  const columns: ColumnDef<Student>[] = [
    { 
      header: "الرقم", 
      cell: (row) => <span className="font-medium text-gray-900">#{row.id}</span> 
    },
    { 
      header: "اسم الطالب", 
      cell: (row) => (
        <div className="flex items-center gap-3">
          <div className="h-8 w-8 rounded-full bg-primary/10 flex items-center justify-center text-primary font-bold">
            {row.fullName.charAt(0)}
          </div>
          <span className="font-semibold text-gray-800">{row.fullName}</span>
        </div>
      ) 
    },
    { 
      header: "تاريخ الميلاد", 
      cell: (row) => <span dir="ltr">{new Date(row.dateOfBirth).toLocaleDateString()}</span> 
    },
    { 
      header: "تاريخ التسجيل", 
      cell: (row) => <span dir="ltr">{new Date(row.enrollmentDate).toLocaleDateString()}</span> 
    },
    { 
      header: "الجنس", 
      cell: (row) => (
        <span className="px-3 py-1 rounded-full text-xs font-medium bg-blue-100 text-blue-700">
          {row.gender === 0 ? "ذكر" : "أنثى"}
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
        <h2 className="text-2xl font-bold bg-clip-text text-transparent bg-gradient-to-l from-primary to-accent flex items-center gap-2">
          <GraduationCap className="h-6 w-6 text-primary" />
          إدارة الطلاب
        </h2>
        <Button onClick={() => setIsFormOpen(true)}>
          <Plus className="h-4 w-4 mr-2" />
          إضافة طالب
        </Button>
      </div>

      <DataTable
        data={students}
        columns={columns}
        isLoading={isLoading}
        searchPlaceholder="ابحث باسم الطالب..."
        searchKey="fullName"
      />

      <Modal 
        isOpen={isFormOpen} 
        onClose={() => !isCreating && setIsFormOpen(false)} 
        title="تسجيل طالب جديد"
      >
        <StudentForm onSubmit={handleCreate} isLoading={isCreating} />
      </Modal>

      <ConfirmModal
        isOpen={deleteId !== null}
        onClose={() => !isDeleting && setDeleteId(null)}
        onConfirm={handleDelete}
        title="حذف طالب مسجل"
        message="هل أنت متأكد من رغبتك في حذف هذا الطالب؟ لا يمكن التراجع عن هذه العملية."
        isLoading={isDeleting}
      />
    </div>
  );
}
