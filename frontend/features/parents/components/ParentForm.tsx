"use client";

import { useState } from "react";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import { CreateParentDto } from "../api/mutations";

interface ParentFormProps {
  initialData?: CreateParentDto;
  onSubmit: (data: CreateParentDto) => void;
  isLoading?: boolean;
}

export function ParentForm({ initialData, onSubmit, isLoading }: ParentFormProps) {
  const [formData, setFormData] = useState<CreateParentDto>(initialData || {
    fullName: "",
    phoneNumber: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(formData);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <Input
        label="الاسم الكامل"
        name="fullName"
        required
        value={formData.fullName}
        onChange={handleChange}
        placeholder="أدخل اسم ولي الأمر..."
      />

      <Input
        label="رقم الهاتف"
        type="tel"
        name="phoneNumber"
        required
        value={formData.phoneNumber}
        onChange={handleChange}
        placeholder="05XXXXXXXX"
        dir="ltr"
        className="text-right"
      />

      <div className="pt-4 border-t flex justify-end gap-3 border-gray-100">
        <Button
          type="submit"
          isLoading={isLoading}
          className="w-full sm:w-auto"
        >
          حفظ التغييرات
        </Button>
      </div>
    </form>
  );
}
