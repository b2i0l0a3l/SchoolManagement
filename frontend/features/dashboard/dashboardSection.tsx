"use client";

import Cookies from "js-cookie";
import { Users, GraduationCap, ClipboardList, TrendingUp } from "lucide-react";
import Notification from "./components/notification";
import ExamsSection from "./components/examsSection";

export default function DashboardSection() {
    const userCookie = Cookies.get("user");
    let email = "مدير النظام";
    try {
        if (userCookie) email = JSON.parse(userCookie).email;
    } catch {}

    const stats = [
        { name: "إجمالي الطلاب", value: "1,240", icon: GraduationCap, change: "+12%", changeType: "positive" },
        { name: "إجمالي المعلمين", value: "84", icon: Users, change: "+2%", changeType: "positive" },
        { name: "الامتحانات القادمة", value: "12", icon: ClipboardList, change: "هذا الأسبوع", changeType: "neutral" },
        { name: "متوسط الحضور", value: "94%", icon: TrendingUp, change: "+1.2%", changeType: "positive" },
    ];

    return (
        <div className="space-y-6">
            <div className="mb-8">
                <h1 className="text-3xl font-bold text-gray-900">مرحباً بعودتك، {email} 👋</h1>
                <p className="mt-2 text-gray-600">إليك نظرة عامة على نشاط المدرسة اليوم.</p>
            </div>

            <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-4">
                {stats.map((stat, index) => (
                    <div
                        key={stat.name}
                        className="glass-panel overflow-hidden rounded-2xl p-6 shadow-sm hover:shadow-md transition-shadow animate-slide-up"
                        style={{ animationDelay: `${index * 100}ms` }}
                    >
                        <div className="flex items-center">
                            <div className="flex h-12 w-12 items-center justify-center rounded-xl bg-primary/10">
                                <stat.icon className="h-6 w-6 text-primary" aria-hidden="true" />
                            </div>
                            <div className="mr-4">
                                <p className="text-sm font-medium text-gray-500">{stat.name}</p>
                                <div className="flex items-baseline gap-2 mt-1">
                                    <p className="text-2xl font-bold text-gray-900">{stat.value}</p>
                                </div>
                            </div>
                        </div>
                        <div className="mt-4">
                            <span className={`text-sm font-medium ${
                                stat.changeType === 'positive' ? 'text-green-600 bg-green-50' :
                                    stat.changeType === 'negative' ? 'text-red-600 bg-red-50' :
                                        'text-gray-600 bg-gray-100'
                                } px-2 py-1 rounded-md`}>
                                {stat.change}
                            </span>
                        </div>
                    </div>
                ))}
            </div>
            <Notification />
            <ExamsSection/>
            </div>
    );
}