"use client";

import { useState } from "react";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import { CreateExamDto } from "../api/mutations";

interface ExamFormProps {
  initialData?: CreateExamDto;
  onSubmit: (data: CreateExamDto) => void;
  isLoading?: boolean;
}

export function ExamForm({ initialData, onSubmit, isLoading }: ExamFormProps) {
  const [formData, setFormData] = useState<CreateExamDto>(initialData || {
    title: "",
    date: new Date().toISOString().split('T')[0],
    subjectId: 1,
    maxScore: 100,
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "subjectId" || name === "maxScore" ? Number(value) : value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const payload = {
        ...formData,
        date: new Date(formData.date).toISOString(),
    }
    onSubmit(payload);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <Input
        label="عنوان الامتحان"
        name="title"
        required
        value={formData.title}
        onChange={handleChange}
        placeholder="مثال: الاختبار النصفي للأحياء..."
      />

      <div className="grid grid-cols-2 gap-4">
        <Input
          label="تاريخ الامتحان"
          type="date"
          name="date"
          required
          value={formData.date.split('T')[0]}
          onChange={handleChange}
        />
        
        <Input
          label="الدرجة القصوى"
          type="number"
          name="maxScore"
          min="1"
          required
          value={formData.maxScore}
          onChange={handleChange}
        />
      </div>

      <Input
        label="رقم المادة (Subject ID)"
        type="number"
        name="subjectId"
        min="1"
        required
        value={formData.subjectId}
        onChange={handleChange}
      />

      <div className="pt-4 border-t flex justify-end gap-3 border-gray-100">
        <Button
          type="submit"
          isLoading={isLoading}
          className="w-full sm:w-auto"
        >
          حفظ الامتحان
        </Button>
      </div>
    </form>
  );
}
