import { GraduationCap } from "lucide-react";
import LoginFrom from "@/features/login/component/LoginFrom";

export default function LoginPage() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-linear-to-br from-slate-50 to-blue-50 py-12 px-4 sm:px-6 lg:px-8 relative overflow-hidden">
      <div className="absolute top-0 right-0 -mr-20 -mt-20 w-64 h-64 rounded-full bg-blue-400 opacity-20 blur-[80px]"></div>
      <div className="absolute bottom-0 left-0 -ml-20 -mb-20 w-80 h-80 rounded-full bg-indigo-500 opacity-20 blur-[100px]"></div>
      <div className="max-w-md w-full space-y-8 glass-panel p-8 sm:p-10 rounded-3xl shadow-2xl z-10 animate-fade-in border border-white/60">
        <div className="text-center">
          <div className="mx-auto h-16 w-16 bg-primary text-white rounded-2xl flex items-center justify-center shadow-lg shadow-primary/30 mb-6">
            <GraduationCap className="h-8 w-8" />
          </div>
          <h2 className="mt-2 text-3xl font-extrabold text-gray-900 tracking-tight">
            مرحباً بك مجدداً
          </h2>
          <p className="mt-3 text-sm text-gray-500">
            سجل دخولك للوصول إلى لوحة تحكم نظام المدارس
          </p>
        </div>
        <LoginFrom />
      </div>
    </div>
  );
}
