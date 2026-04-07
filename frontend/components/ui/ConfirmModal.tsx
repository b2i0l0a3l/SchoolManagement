"use client";

import { Modal } from "./Modal";
import { Button } from "./Button";
import { AlertTriangle } from "lucide-react";

interface ConfirmModalProps {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: () => void;
  title: string;
  message: string;
  isLoading?: boolean;
}

export function ConfirmModal({ isOpen, onClose, onConfirm, title, message, isLoading }: ConfirmModalProps) {
  return (
    <Modal isOpen={isOpen} onClose={onClose} title={title} width="sm">
      <div className="flex flex-col items-center text-center">
        <div className="h-12 w-12 rounded-full bg-red-100 flex items-center justify-center mb-4">
          <AlertTriangle className="h-6 w-6 text-red-600" />
        </div>
        <p className="text-gray-600 mb-6">{message}</p>
        
        <div className="flex gap-3 w-full">
          <Button 
            variant="ghost" 
            className="flex-1 bg-gray-100" 
            onClick={onClose}
            disabled={isLoading}
          >
            إلغاء
          </Button>
          <Button 
            variant="danger" 
            className="flex-1" 
            onClick={onConfirm}
            isLoading={isLoading}
          >
            تأكيد الحذف
          </Button>
        </div>
      </div>
    </Modal>
  );
}
