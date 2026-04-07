"use client";

import { useState } from "react";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import { CreateStudentDto } from "../api/mutations";

interface StudentFormProps {
  initialData?: CreateStudentDto;
  onSubmit: (data: CreateStudentDto) => void;
  isLoading?: boolean;
}

export function StudentForm({ initialData, onSubmit, isLoading }: StudentFormProps) {
  const [formData, setFormData] = useState<CreateStudentDto>(initialData || {
    fullName: "",
    dateOfBirth: new Date().toISOString().split('T')[0],
    enrollmentDate: new Date().toISOString().split('T')[0],
    gender: 0, 
    classId: 1, 
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "gender" || name === "classId" ? Number(value) : value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const payload = {
        ...formData,
        dateOfBirth: new Date(formData.dateOfBirth).toISOString(),
        enrollmentDate: new Date(formData.enrollmentDate).toISOString()
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
        placeholder="أدخل اسم الطالب..."
      />

      <div className="grid grid-cols-2 gap-4">
        <Input
          label="تاريخ الميلاد"
          type="date"
          name="dateOfBirth"
          required
          value={formData.dateOfBirth.split('T')[0]}
          onChange={handleChange}
        />
        
        <Input
          label="تاريخ التسجيل"
          type="date"
          name="enrollmentDate"
          required
          value={formData.enrollmentDate.split('T')[0]}
          onChange={handleChange}
        />
      </div>

      <div className="grid grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">الجنس</label>
          <select
            name="gender"
            value={formData.gender}
            onChange={handleChange}
            className="block w-full px-3 py-2 border rounded-xl shadow-sm border-gray-300 focus:outline-none focus:ring-2 focus:ring-primary focus:border-primary sm:text-sm"
          >
            <option value={0}>ذكر</option>
            <option value={1}>أنثى</option>
          </select>
        </div>

        <Input
          label="رقم الصف"
          type="number"
          name="classId"
          min="1"
          required
          value={formData.classId}
          onChange={handleChange}
        />
      </div>

      <div className="pt-4 border-t flex justify-end gap-3 border-gray-100">
        <Button
          type="submit"
          isLoading={isLoading}
          className="w-full sm:w-auto"
        >
          {initialData ? "تحديث التغييرات" : "حفظ الطالب"}
        </Button>
      </div>
    </form>
  );
}
