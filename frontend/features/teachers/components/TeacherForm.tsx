"use client";

import { useState } from "react";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import { CreateTeacherDto } from "../api/mutations";

interface TeacherFormProps {
  initialData?: CreateTeacherDto;
  onSubmit: (data: CreateTeacherDto) => void;
  isLoading?: boolean;
}

export function TeacherForm({ initialData, onSubmit, isLoading }: TeacherFormProps) {
  const [formData, setFormData] = useState<CreateTeacherDto>(initialData || {
    fullName: "",
    hireDate: new Date().toISOString().split('T')[0],
    departmentId: 1, // Default fallback
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "departmentId" ? Number(value) : value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const payload = {
        ...formData,
        hireDate: new Date(formData.hireDate).toISOString(),
    }
    onSubmit(payload);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <Input
        label="الاسم الكامل"
        name="fullName"
        required
        value={formData.fullName}
        onChange={handleChange}
        placeholder="أدخل اسم المعلم..."
      />

      <div className="grid grid-cols-2 gap-4">
        <Input
          label="تاريخ التعيين"
          type="date"
          name="hireDate"
          required
          value={formData.hireDate.split('T')[0]}
          onChange={handleChange}
        />
        
        <Input
          label="رقم القسم (Department ID)"
          type="number"
          name="departmentId"
          min="1"
          required
          value={formData.departmentId}
          onChange={handleChange}
        />
      </div>

      <div className="pt-4 border-t flex justify-end gap-3 border-gray-100">
        <Button
          type="submit"
          isLoading={isLoading}
          className="w-full sm:w-auto"
        >
          حفظ المعلم
        </Button>
      </div>
    </form>
  );
}
