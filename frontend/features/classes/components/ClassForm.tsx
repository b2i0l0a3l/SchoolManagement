"use client";

import { useState } from "react";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import { CreateClassDto } from "../api/mutations";

interface ClassFormProps {
  initialData?: CreateClassDto;
  onSubmit: (data: CreateClassDto) => void;
  isLoading?: boolean;
}

export function ClassForm({ initialData, onSubmit, isLoading }: ClassFormProps) {
  const [formData, setFormData] = useState<CreateClassDto>(initialData || {
    className: "",
    year: new Date().toISOString().split('T')[0],
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
    const payload = {
        ...formData,
        year: new Date(formData.year).toISOString(),
    }
    onSubmit(payload);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <Input
        label="اسم الصف"
        name="className"
        required
        value={formData.className}
        onChange={handleChange}
        placeholder="أدخل اسم أو رمز الصف..."
      />

      <Input
        label="سنة التأسيس / تاريخ الافتتاح"
        type="date"
        name="year"
        required
        value={formData.year.split('T')[0]}
        onChange={handleChange}
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
