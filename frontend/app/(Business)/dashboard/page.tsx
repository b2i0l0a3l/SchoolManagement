"use client";

import Header from "@/Components/Ui/Header/Header";
import StatCard from "@/Components/Ui/Card/StatCard";
import Card from "@/Components/Ui/Card/Card";
import {
    Users,
    GraduationCap,
    BookOpen,
    ClipboardCheck,
    Pencil,
    Trash2,
} from "lucide-react";

const recentStudents = [
    { id: 101, name: "محمد خالد", class: "12 أ", status: "نشط" },
    { id: 102, name: "فاطمة أحمد", class: "11 ب", status: "نشط" },
    { id: 103, name: "يوسف علي", class: "10 ج", status: "غائب" },
    { id: 104, name: "نورة سعيد", class: "9 د", status: "نشط" },
    { id: 105, name: "أحمد محمود", class: "12 ب", status: "نشط" },
    { id: 106, name: "سارة حسن", class: "11 أ", status: "غائب" },
];

const recentActivity = [
    { text: "تم تسجيل طالب جديد: عمر أحمد", time: "منذ 5 دقائق" },
    { text: "تم تحديث درجات الفصل 12 أ", time: "منذ 15 دقيقة" },
    { text: "تم إضافة امتحان رياضيات", time: "منذ ساعة" },
    { text: "تم تسجيل حضور الفصل 10 ب", time: "منذ ساعتين" },
    { text: "تم تعيين معلم جديد: سارة محمد", time: "منذ 3 ساعات" },
];

export default function DashboardPage() {
    return (
        <div className="page-container">
            <Header title="لوحة القيادة" />

            <div className="page-content">
                {/* Stats */}
                <div className="stats-grid">
                    <StatCard
                        label="إجمالي الطلاب"
                        value={512}
                        icon={<Users size={22} />}
                        iconColor="indigo"
                        change={12}
                    />
                    <StatCard
                        label="إجمالي المعلمين"
                        value={48}
                        icon={<GraduationCap size={22} />}
                        iconColor="teal"
                        change={5}
                    />
                    <StatCard
                        label="الفصول النشطة"
                        value={24}
                        icon={<BookOpen size={22} />}
                        iconColor="emerald"
                        change={8}
                    />
                    <StatCard
                        label="نسبة الحضور اليوم"
                        value="94.2%"
                        icon={<ClipboardCheck size={22} />}
                        iconColor="amber"
                        change={-2}
                    />
                </div>

                {/* Content Grid */}
                <div style={{ display: 'grid', gridTemplateColumns: '1fr 350px', gap: '24px' }}>
                    {/* Recent Students Table */}
                    <Card
                        header={
                            <>
                                <h3>سجلات الطلاب الحديثة</h3>
                                <button className="btn btn-ghost btn-sm">عرض الكل</button>
                            </>
                        }
                        className=""
                    >
                        <div className="table-wrapper" style={{ border: 'none', background: 'transparent' }}>
                            <table>
                                <thead>
                                    <tr>
                                        <th style={{ padding: '12px 16px', textAlign: 'right', fontSize: '0.8rem', fontWeight: 600, color: 'var(--text-secondary)', borderBottom: '1px solid var(--border-subtle)' }}>المعرف</th>
                                        <th style={{ padding: '12px 16px', textAlign: 'right', fontSize: '0.8rem', fontWeight: 600, color: 'var(--text-secondary)', borderBottom: '1px solid var(--border-subtle)' }}>الاسم</th>
                                        <th style={{ padding: '12px 16px', textAlign: 'right', fontSize: '0.8rem', fontWeight: 600, color: 'var(--text-secondary)', borderBottom: '1px solid var(--border-subtle)' }}>الفصل</th>
                                        <th style={{ padding: '12px 16px', textAlign: 'right', fontSize: '0.8rem', fontWeight: 600, color: 'var(--text-secondary)', borderBottom: '1px solid var(--border-subtle)' }}>الحالة</th>
                                        <th style={{ padding: '12px 16px', textAlign: 'right', fontSize: '0.8rem', fontWeight: 600, color: 'var(--text-secondary)', borderBottom: '1px solid var(--border-subtle)' }}>الإجراءات</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {recentStudents.map((student) => (
                                        <tr key={student.id} style={{ borderBottom: '1px solid var(--border-subtle)', transition: 'background 150ms' }}>
                                            <td style={{ padding: '12px 16px', fontSize: '0.9rem', color: 'var(--primary-light)' }}>#{student.id}</td>
                                            <td style={{ padding: '12px 16px', fontSize: '0.9rem', color: 'var(--text-primary)' }}>{student.name}</td>
                                            <td style={{ padding: '12px 16px', fontSize: '0.9rem', color: 'var(--text-secondary)' }}>{student.class}</td>
                                            <td style={{ padding: '12px 16px' }}>
                                                <span className={`badge ${student.status === 'نشط' ? 'badge-success' : 'badge-warning'}`}>
                                                    {student.status}
                                                </span>
                                            </td>
                                            <td style={{ padding: '12px 16px' }}>
                                                <div style={{ display: 'flex', gap: '6px' }}>
                                                    <button className="btn btn-ghost btn-icon" style={{ width: 30, height: 30, padding: 4 }}>
                                                        <Pencil size={14} />
                                                    </button>
                                                    <button className="btn btn-danger btn-icon" style={{ width: 30, height: 30, padding: 4 }}>
                                                        <Trash2 size={14} />
                                                    </button>
                                                </div>
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    </Card>

                    {/* Recent Activity */}
                    <Card
                        header={
                            <>
                                <h3>النشاط الأخير</h3>
                            </>
                        }
                    >
                        <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
                            {recentActivity.map((activity, index) => (
                                <div key={index} style={{
                                    display: 'flex',
                                    gap: '12px',
                                    alignItems: 'flex-start',
                                    paddingBottom: index < recentActivity.length - 1 ? '16px' : '0',
                                    borderBottom: index < recentActivity.length - 1 ? '1px solid var(--border-subtle)' : 'none',
                                }}>
                                    <div style={{
                                        width: 8,
                                        height: 8,
                                        borderRadius: '50%',
                                        background: 'var(--primary-light)',
                                        marginTop: 6,
                                        flexShrink: 0,
                                    }} />
                                    <div>
                                        <div style={{ fontSize: '0.85rem', color: 'var(--text-primary)', lineHeight: 1.5 }}>
                                            {activity.text}
                                        </div>
                                        <div style={{ fontSize: '0.75rem', color: 'var(--text-muted)', marginTop: 2 }}>
                                            {activity.time}
                                        </div>
                                    </div>
                                </div>
                            ))}
                        </div>
                    </Card>
                </div>
            </div>
        </div>
    );
}
