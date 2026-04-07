"use client";

import { useState } from "react";
import { useExams } from "../api/get-exams";
import { useCreateExam, useDeleteExam, CreateExamDto } from "../api/mutations";
import { ClipboardList, Trash2, Plus } from "lucide-react";
import { DataTable, ColumnDef } from "@/components/ui/DataTable";
import { Button } from "@/components/ui/Button";
import { Modal } from "@/components/ui/Modal";
import { ConfirmModal } from "@/components/ui/ConfirmModal";
import { ExamForm } from "./ExamForm";
import { Exam } from "../types";
import toast from "react-hot-toast";

export default function ExamTable() {
  const { data: exams = [], isLoading, isError } = useExams();
  const { mutate: createExam, isPending: isCreating } = useCreateExam();
  const { mutate: deleteExam, isPending: isDeleting } = useDeleteExam();

  const [isFormOpen, setIsFormOpen] = useState(false);
  const [deleteId, setDeleteId] = useState<number | null>(null);

  const handleCreate = (data: CreateExamDto) => {
    createExam(data, {
      onSuccess: () => {
        toast.success("تم إضافة الامتحان بنجاح");
        setIsFormOpen(false);
      },
      onError: (err: any) => {
        toast.error(err.response?.data?.title || "فشلت عملية الإضافة");
      }
    });
  };

  const handleDelete = () => {
    if (deleteId === null) return;
    deleteExam(deleteId, {
      onSuccess: () => {
        toast.success("تم حذف الامتحان بنجاح");
        setDeleteId(null);
      },
      onError: () => {
        toast.error("فشل في عملية الحذف");
        setDeleteId(null);
      }
    });
  };

  
  const columns: ColumnDef<Exam>[] = [
    { 
      header: "الرقم", 
      cell: (row) => <span className="font-medium text-gray-900">#{row.id}</span> 
    },
    { 
      header: "عنوان الامتحان", 
      cell: (row) => <span className="font-semibold text-gray-800">{row.title}</span> 
    },
    { 
      header: "التاريخ", 
      cell: (row) => <span dir="ltr">{new Date(row.date).toLocaleDateString()}</span> 
    },
    { 
      header: "الدرجة القصوى", 
      cell: (row) => (
        <span className="inline-flex items-center rounded-md bg-yellow-50 px-2 py-1 text-xs font-medium text-yellow-800 ring-1 ring-inset ring-yellow-600/20">
          {row.maxScore} درجة
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
          <ClipboardList className="h-6 w-6 text-primary" />
          إدارة الامتحانات
        </h2>
        <Button onClick={() => setIsFormOpen(true)}>
          <Plus className="h-4 w-4 mr-2" />
          إضافة امتحان
        </Button>
      </div>

      <DataTable
        data={exams}
        columns={columns}
        isLoading={isLoading}
        searchPlaceholder="ابحث بعنوان الامتحان..."
        searchKey="title"
      />

      <Modal 
        isOpen={isFormOpen} 
        onClose={() => !isCreating && setIsFormOpen(false)} 
        title="إضافة امتحان جديد"
      >
        <ExamForm onSubmit={handleCreate} isLoading={isCreating} />
      </Modal>

      <ConfirmModal
        isOpen={deleteId !== null}
        onClose={() => !isDeleting && setDeleteId(null)}
        onConfirm={handleDelete}
        title="حذف الامتحان"
        message="هل أنت متأكد من رغبتك في حذف هذا الامتحان بالكامل؟"
        isLoading={isDeleting}
      />
    </div>
  );
}
