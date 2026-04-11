"use client";

import Link from "next/link";
import {
    LayoutDashboard,
    Users,
    GraduationCap,
    BookOpen,
    ClipboardCheck,
    CalendarCheck,
    Settings,
} from "lucide-react";
import { usePathname } from "next/navigation";

const links = [
    {
        title: "لوحة القيادة",
        href: "/dashboard",
        icon: <LayoutDashboard size={20} />,
    },
    {
        title: "إدارة الطلاب",
        href: "/students",
        icon: <Users size={20} />,
    },
    {
        title: "إدارة المعلمين",
        href: "/teachers",
        icon: <GraduationCap size={20} />,
    },
    {
        title: "إدارة المناهج",
        href: "/courses",
        icon: <BookOpen size={20} />,
    },
    {
        title: "إدارة الحضور",
        href: "/attendance",
        icon: <ClipboardCheck size={20} />,
    },
    {
        title: "إدارة الامتحانات",
        href: "/exams",
        icon: <CalendarCheck size={20} />,
    },
    {
        title: "الإعدادات",
        href: "/settings",
        icon: <Settings size={20} />,
    },
];

export default function SideBar() {
    const pathname = usePathname();

    return (
        <div className="sidebar">
            {/* Logo */}
            <div className="sidebar-logo">
                <div className="logo-icon">
                    <GraduationCap size={22} />
                </div>
                <span className="logo-text">بوابة المدرسة</span>
            </div>

            {/* Navigation */}
            <nav className="sidebar-nav">
                {links.map((link) => (
                    <Link
                        key={link.href}
                        href={link.href}
                        className={`sidebar-link ${pathname === link.href ? "active" : ""}`}
                    >
                        <span className="link-icon">{link.icon}</span>
                        <span>{link.title}</span>
                    </Link>
                ))}
            </nav>

            {/* Footer */}
            <div style={{ padding: '16px 12px', borderTop: '1px solid var(--border-subtle)' }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: '12px', padding: '8px' }}>
                    <div className="avatar" style={{ width: 36, height: 36, fontSize: '0.75rem' }}>
                        م
                    </div>
                    <div>
                        <div style={{ fontSize: '0.85rem', fontWeight: 600, color: 'var(--text-primary)' }}>
                            المدير
                        </div>
                        <div style={{ fontSize: '0.7rem', color: 'var(--text-muted)' }}>
                            admin@school.com
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}