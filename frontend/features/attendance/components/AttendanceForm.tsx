"use client";

import { useState } from "react";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import { CreateAttendanceDto } from "../api/mutations";
import { AttendanceStatus } from "../types";

interface AttendanceFormProps {
  initialData?: CreateAttendanceDto;
  onSubmit: (data: CreateAttendanceDto) => void;
  isLoading?: boolean;
}

export function AttendanceForm({ initialData, onSubmit, isLoading }: AttendanceFormProps) {
  const [formData, setFormData] = useState<CreateAttendanceDto>(initialData || {
    studentId: 1,
    date: new Date().toISOString().split('T')[0],
    status: 0 as AttendanceStatus,
    remarks: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "studentId" || name === "status" ? Number(value) : value,
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
      <div className="grid grid-cols-2 gap-4">
        <Input
          label="رقم الطالب (Student ID)"
          type="number"
          name="studentId"
          min="1"
          required
          value={formData.studentId}
          onChange={handleChange}
        />

        <Input
          label="التاريخ"
          type="date"
          name="date"
          required
          value={formData.date.split('T')[0]}
          onChange={handleChange}
        />
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">الحالة</label>
        <select
          name="status"
          value={formData.status}
          onChange={handleChange}
          className="block w-full px-3 py-2 border rounded-xl shadow-sm border-gray-300 focus:outline-none focus:ring-2 focus:ring-primary focus:border-primary sm:text-sm"
        >
          <option value={0}>حاضر</option>
          <option value={1}>غائب</option>
          <option value={2}>تأخير</option>
        </select>
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">ملاحظات</label>
        <textarea
          name="remarks"
          value={formData.remarks}
          onChange={handleChange}
          rows={3}
          className="block w-full px-3 py-2 border rounded-xl shadow-sm border-gray-300 focus:outline-none focus:ring-2 focus:ring-primary focus:border-primary sm:text-sm resize-none"
          placeholder="أضف ملاحظات إن وجدت..."
        />
      </div>

      <div className="pt-4 border-t flex justify-end gap-3 border-gray-100">
        <Button
          type="submit"
          isLoading={isLoading}
          className="w-full sm:w-auto"
        >
          تسجيل الحضور
        </Button>
      </div>
    </form>
  );
}
