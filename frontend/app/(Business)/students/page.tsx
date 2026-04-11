"use client";

import Header from "@/Components/Ui/Header/Header";
import Table from "@/Components/Ui/Table/Table";
import Button from "@/Components/Ui/Button/Button";
import Modal from "@/Components/Ui/Modal/Modal";
import Input from "@/Components/Ui/Input/Input";
import { usePaginationStore } from "@/Context/paginationStore";
import { createColumnHelper } from "@tanstack/react-table";
import { useMemo, useState } from "react";
import { Plus, Pencil, Trash2 } from "lucide-react";

type Student = {
    id: number;
    name: string;
    class: string;
    email: string;
    status: string;
};

const allStudents: Student[] = [
    { id: 1, name: "محمد خالد", class: "12 أ", email: "mohammed@school.com", status: "نشط" },
    { id: 2, name: "فاطمة أحمد", class: "11 ب", email: "fatma@school.com", status: "نشط" },
    { id: 3, name: "يوسف علي", class: "10 ج", email: "yousef@school.com", status: "غائب" },
    { id: 4, name: "نورة سعيد", class: "9 د", email: "noura@school.com", status: "نشط" },
    { id: 5, name: "أحمد محمود", class: "12 ب", email: "ahmed@school.com", status: "نشط" },
    { id: 6, name: "سارة حسن", class: "11 أ", email: "sara@school.com", status: "غائب" },
    { id: 7, name: "عمر خالد", class: "10 أ", email: "omar@school.com", status: "نشط" },
    { id: 8, name: "ريم محمد", class: "9 ب", email: "reem@school.com", status: "نشط" },
    { id: 9, name: "خالد سعد", class: "12 ج", email: "khaled@school.com", status: "غائب" },
];

export default function StudentsPage() {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [activeFilter, setActiveFilter] = useState("all");

    const columnHelper = createColumnHelper<Student>();

    const columns = useMemo(() => [
        columnHelper.accessor("id", {
            header: "الرقم",
            cell: (info) => (
                <span style={{ color: 'var(--primary-light)', fontWeight: 600 }}>
                    #{info.getValue()}
                </span>
            ),
        }),
        columnHelper.accessor("name", {
            header: "الاسم",
        }),
        columnHelper.accessor("class", {
            header: "الصف",
            cell: (info) => (
                <span style={{ color: 'var(--text-secondary)' }}>{info.getValue()}</span>
            ),
        }),
        columnHelper.accessor("email", {
            header: "البريد الإلكتروني",
            cell: (info) => (
                <span style={{ color: 'var(--text-secondary)', direction: 'ltr', display: 'inline-block' }}>
                    {info.getValue()}
                </span>
            ),
        }),
        columnHelper.accessor("status", {
            header: "الحالة",
            cell: (info) => (
                <span className={`badge ${info.getValue() === 'نشط' ? 'badge-success' : 'badge-warning'}`}>
                    {info.getValue()}
                </span>
            ),
        }),
        columnHelper.display({
            id: "actions",
            header: "الإجراءات",
            cell: () => (
                <div style={{ display: 'flex', gap: '6px' }}>
                    <button className="btn btn-ghost btn-icon" style={{ width: 32, height: 32, padding: 4 }}>
                        <Pencil size={14} />
                    </button>
                    <button className="btn btn-danger btn-icon" style={{ width: 32, height: 32, padding: 4 }}>
                        <Trash2 size={14} />
                    </button>
                </div>
            ),
        }),
    ], []);

    const page = usePaginationStore((state) => state.pageIndex);
    const pageSize = usePaginationStore((state) => state.pageSize);

    const filteredStudents = useMemo(() => {
        if (activeFilter === "active") return allStudents.filter(s => s.status === "نشط");
        if (activeFilter === "absent") return allStudents.filter(s => s.status === "غائب");
        return allStudents;
    }, [activeFilter]);

    const filterData = useMemo(() => {
        return filteredStudents.slice((page - 1) * pageSize, page * pageSize);
    }, [filteredStudents, page, pageSize]);

    const pageCount = Math.ceil(filteredStudents.length / pageSize);

    return (
        <div className="page-container">
            <Header title="إدارة الطلاب" />

            <div className="page-content">
                {/* Title Row */}
                <div className="page-title-row">
                    <div>
                        <h1>إدارة الطلاب</h1>
                        <p style={{ color: 'var(--text-muted)', fontSize: '0.85rem', marginTop: 4 }}>
                            إجمالي {filteredStudents.length} طالب
                        </p>
                    </div>
                    <Button
                        text="إضافة طالب"
                        variant="primary"
                        icon={<Plus size={18} />}
                        onClick={() => setIsModalOpen(true)}
                    />
                </div>

                {/* Filter Chips */}
                <div className="filter-chips">
                    <button
                        className={`filter-chip ${activeFilter === 'all' ? 'active' : ''}`}
                        onClick={() => setActiveFilter('all')}
                    >
                        جميع الطلاب
                    </button>
                    <button
                        className={`filter-chip ${activeFilter === 'active' ? 'active' : ''}`}
                        onClick={() => setActiveFilter('active')}
                    >
                        نشط
                    </button>
                    <button
                        className={`filter-chip ${activeFilter === 'absent' ? 'active' : ''}`}
                        onClick={() => setActiveFilter('absent')}
                    >
                        غائب
                    </button>
                </div>

                {/* Table */}
                <Table data={filterData} pageCount={pageCount} columns={columns} />
            </div>

            {/* Add Student Modal */}
            <Modal
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                title="إضافة طالب جديد"
                footer={
                    <>
                        <Button text="حفظ" variant="accent" onClick={() => setIsModalOpen(false)} />
                        <Button text="إلغاء" variant="outline" onClick={() => setIsModalOpen(false)} />
                    </>
                }
            >
                <Input label="الاسم الكامل" placeholder="أدخل اسم الطالب" />
                <Input label="البريد الإلكتروني" type="email" placeholder="example@school.com" />
                <Input label="رقم الهاتف" type="tel" placeholder="05X XXX XXXX" />
                <div className="input-group">
                    <label className="input-label">الصف الدراسي</label>
                    <select className="select">
                        <option value="">اختر الصف</option>
                        <option value="9">الصف التاسع</option>
                        <option value="10">الصف العاشر</option>
                        <option value="11">الصف الحادي عشر</option>
                        <option value="12">الصف الثاني عشر</option>
                    </select>
                </div>
                <Input label="تاريخ الميلاد" type="date" />
            </Modal>
        </div>
    );
}
