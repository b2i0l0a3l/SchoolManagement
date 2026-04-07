"use client";

import Link from 'next/link';
import { usePathname, useRouter } from 'next/navigation';
import Cookies from 'js-cookie';
import toast from 'react-hot-toast';
import { 
  LayoutDashboard, 
  Users, 
  GraduationCap, 
  BookOpen, 
  ClipboardList, 
  CalendarDays,
  UsersRound,
  LogOut
} from 'lucide-react';

const navItems = [
  { name: 'الرئيسية', href: '/dashboard', icon: LayoutDashboard },
  { name: 'الطلاب', href: '/dashboard/students', icon: GraduationCap },
  { name: 'المعلمون', href: '/dashboard/teachers', icon: Users },
  { name: 'أولياء الأمور', href: '/dashboard/parents', icon: UsersRound },
  { name: 'الصفوف والمواد', href: '/dashboard/classes', icon: BookOpen },
  { name: 'الامتحانات والعلامات', href: '/dashboard/exams', icon: ClipboardList },
  { name: 'الحضور والغياب', href: '/dashboard/attendance', icon: CalendarDays },
];

export default function Sidebar() {
  const pathname = usePathname();
  const router = useRouter();

  const handleLogout = () => {
    Cookies.remove('token');
    Cookies.remove('user');
    toast.success("تم تسجيل الخروج");
    router.push('/login');
  };

  return (
    <aside className="fixed inset-y-0 right-0 z-50 w-64 bg-primary text-white shadow-xl transition-transform duration-300 md:block hidden animate-slide-in-right">
      <div className="flex h-16 items-center justify-center border-b border-white/10 px-4 shadow-sm">
        <h1 className="text-xl font-bold bg-clip-text text-transparent bg-gradient-to-r from-white to-blue-200">
          نظام المدارس
        </h1>
      </div>
      <nav className="flex flex-1 flex-col gap-1 p-4 overflow-y-auto">
        {navItems.map((item) => {
          const isActive = pathname === item.href || pathname?.startsWith(`${item.href}/`);
          return (
            <Link
              key={item.name}
              href={item.href}
              className={`flex items-center gap-3 rounded-xl px-4 py-3 text-sm transition-all duration-200 group relative overflow-hidden ${
                isActive 
                  ? 'bg-white/15 text-white font-medium shadow-md shadow-black/5' 
                  : 'text-white/80 hover:bg-white/10 hover:text-white'
              }`}
            >
              {isActive && (
                <span className="absolute right-0 top-1/2 -translate-y-1/2 w-1.5 h-8 bg-accent rounded-l-full"></span>
              )}
              <item.icon className={`h-5 w-5 ${isActive ? 'text-accent' : 'group-hover:scale-110 transition-transform duration-200'}`} />
              <span className="font-semibold">{item.name}</span>
            </Link>
          );
        })}
      </nav>
      <div className="p-4 border-t border-white/10">
        <button
          onClick={handleLogout}
          className="flex w-full items-center gap-3 rounded-xl px-4 py-3 text-sm text-red-300 transition-colors hover:bg-white/5 hover:text-red-200"
        >
          <LogOut className="h-5 w-5" />
          <span className="font-semibold">تسجيل الخروج</span>
        </button>
      </div>
    </aside>
  );
}
