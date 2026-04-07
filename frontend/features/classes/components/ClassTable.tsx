"use client";

import { useState } from "react";
import { useClasses } from "../api/get-classes";
import { useCreateClass, useDeleteClass, CreateClassDto } from "../api/mutations";
import { School, Trash2, Plus } from "lucide-react";
import { DataTable, ColumnDef } from "@/components/ui/DataTable";
import { Button } from "@/components/ui/Button";
import { Modal } from "@/components/ui/Modal";
import { ConfirmModal } from "@/components/ui/ConfirmModal";
import { ClassForm } from "./ClassForm";
import { Class } from "../types";
import toast from "react-hot-toast";

export default function ClassTable() {
  const { data: classes = [], isLoading, isError } = useClasses();
  const { mutate: createClass, isPending: isCreating } = useCreateClass();
  const { mutate: deleteClass, isPending: isDeleting } = useDeleteClass();

  const [isFormOpen, setIsFormOpen] = useState(false);
  const [deleteId, setDeleteId] = useState<number | null>(null);

  const handleCreate = (data: CreateClassDto) => {
    createClass(data, {
      onSuccess: () => {
        toast.success("تم إضافة الصف بنجاح");
        setIsFormOpen(false);
      },
      onError: (err: any) => {
        toast.error(err.response?.data?.title || "فشلت عملية الإضافة");
      }
    });
  };

  const handleDelete = () => {
    if (deleteId === null) return;
    deleteClass(deleteId, {
      onSuccess: () => {
        toast.success("تم حذف الصف بنجاح");
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
        خطأ في جلب بيانات الصفوف.
      </div>
    );
  }

  const columns: ColumnDef<Class>[] = [
    { 
      header: "الرقم", 
      cell: (row) => <span className="font-medium text-gray-900">#{row.id}</span> 
    },
    { 
      header: "اسم الصف", 
      cell: (row) => (
        <span className="inline-flex items-center rounded-md bg-indigo-50 px-2 py-1 text-xs font-medium text-indigo-700 ring-1 ring-inset ring-indigo-700/10">
          {row.className}
        </span>
      ) 
    },
    { 
      header: "تاريخ الافتتاح", 
      cell: (row) => <span dir="ltr">{new Date(row.year).toLocaleDateString()}</span> 
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
          <School className="h-6 w-6 text-primary" />
          إدارة الصفوف
        </h2>
        <Button onClick={() => setIsFormOpen(true)}>
          <Plus className="h-4 w-4 mr-2" />
          إضافة صف
        </Button>
      </div>

      <DataTable
        data={classes}
        columns={columns}
        isLoading={isLoading}
        searchPlaceholder="ابحث باسم الصف..."
        searchKey="className"
      />

      <Modal 
        isOpen={isFormOpen} 
        onClose={() => !isCreating && setIsFormOpen(false)} 
        title="إضافة صف جديد"
      >
        <ClassForm onSubmit={handleCreate} isLoading={isCreating} />
      </Modal>

      <ConfirmModal
        isOpen={deleteId !== null}
        onClose={() => !isDeleting && setDeleteId(null)}
        onConfirm={handleDelete}
        title="حذف الصف"
        message="هل أنت متأكد من رغبتك في حذف هذا الصف؟"
        isLoading={isDeleting}
      />
    </div>
  );
}
