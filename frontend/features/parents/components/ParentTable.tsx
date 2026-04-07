"use client";

import { useState } from "react";
import { useParents } from "../api/get-parents";
import { useCreateParent, useDeleteParent, CreateParentDto } from "../api/mutations";
import { UsersRound, Trash2, Plus } from "lucide-react";
import { DataTable, ColumnDef } from "@/components/ui/DataTable";
import { Button } from "@/components/ui/Button";
import { Modal } from "@/components/ui/Modal";
import { ConfirmModal } from "@/components/ui/ConfirmModal";
import { ParentForm } from "./ParentForm";
import { Parent } from "../types";
import toast from "react-hot-toast";

export default function ParentTable() {
  const { data: parents = [], isLoading, isError } = useParents();
  const { mutate: createParent, isPending: isCreating } = useCreateParent();
  const { mutate: deleteParent, isPending: isDeleting } = useDeleteParent();

  const [isFormOpen, setIsFormOpen] = useState(false);
  const [deleteId, setDeleteId] = useState<number | null>(null);

  const handleCreate = (data: CreateParentDto) => {
    createParent(data, {
      onSuccess: () => {
        toast.success("تم إضافة ولي الأمر بنجاح");
        setIsFormOpen(false);
      },
      onError: (err: any) => {
        toast.error(err.response?.data?.title || "فشلت عملية الإضافة");
      }
    });
  };

  const handleDelete = () => {
    if (deleteId === null) return;
    deleteParent(deleteId, {
      onSuccess: () => {
        toast.success("تم حذف ولي الأمر بنجاح");
        setDeleteId(null);
      },
      onError: () => {
        toast.error("فشل في عملية الحذف");
        setDeleteId(null);
      }
    });
  };



  const columns: ColumnDef<Parent>[] = [
    { 
      header: "الرقم", 
      cell: (row) => <span className="font-medium text-gray-900">#{row.id}</span> 
    },
    { 
      header: "الاسم", 
      cell: (row) => <span className="font-semibold text-gray-800">{row.fullName}</span> 
    },
    { 
      header: "الجوال", 
      cell: (row) => <span dir="ltr">{row.phoneNumber}</span> 
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
          <UsersRound className="h-6 w-6 text-primary" />
          إدارة أولياء الأمور
        </h2>
        <Button onClick={() => setIsFormOpen(true)}>
          <Plus className="h-4 w-4 mr-2" />
          إضافة ولي أمر
        </Button>
      </div>

      <DataTable
        data={parents}
        columns={columns}
        isLoading={isLoading}
        searchPlaceholder="ابحث بالاسم أو الجوال..."
        searchKey="fullName"
      />

      <Modal 
        isOpen={isFormOpen} 
        onClose={() => !isCreating && setIsFormOpen(false)} 
        title="إضافة ولي أمر جديد"
      >
        <ParentForm onSubmit={handleCreate} isLoading={isCreating} />
      </Modal>

      <ConfirmModal
        isOpen={deleteId !== null}
        onClose={() => !isDeleting && setDeleteId(null)}
        onConfirm={handleDelete}
        title="حذف ولي أمر"
        message="هل أنت متأكد من رغبتك في حذف هذا السجل؟ سيتأثر الطلاب المرتبطين به."
        isLoading={isDeleting}
      />
    </div>
  );
}
