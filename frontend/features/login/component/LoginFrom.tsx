"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import Cookies from "js-cookie";
import api from "@/features/auth/api/api";
import { Lock, Mail, ArrowLeft } from "lucide-react";
import toast from "react-hot-toast";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";

export default function LoginFrom() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const router = useRouter();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);

    try {
      const response = await api.post("/Auth/login", { email, password });
      if (response.data?.token || response) {
        const token = response.data.token || response.data.accessKey || response.data;
        Cookies.set('token', typeof token === 'string' ? token : JSON.stringify(token), { expires: 7, secure: true, sameSite: 'strict' });
        Cookies.set('user', JSON.stringify({ email, role: "Admin" }), { expires: 7, secure: true, sameSite: 'strict' });
        toast.success("تم تسجيل الدخول بنجاح");
        router.push('/dashboard');
      } else {
        toast.error("فشل تسجيل الدخول: لم يتم العثور على توكن");
      }
    } catch (error: any) {
      toast.error(error.response?.data || "كلمة المرور أو البريد الإلكتروني غير صحيح");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
      <div className="space-y-4">
        <Input
          label="البريد الإلكتروني"
          id="email"
          name="email"
          type="email"
          autoComplete="email"
          required
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          placeholder="admin@school.com"
          dir="ltr"
          icon={<Mail className="h-5 w-5 text-gray-400" />}
        />

        <Input
          label="كلمة المرور"
          id="password"
          name="password"
          type="password"
          autoComplete="current-password"
          required
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="••••••••"
          dir="ltr"
          icon={<Lock className="h-5 w-5 text-gray-400" />}
        />
      </div>

      <div className="flex items-center justify-between">
        <div className="flex items-center">
          <input
            id="remember-me"
            name="remember-me"
            type="checkbox"
            className="h-4 w-4 text-primary focus:ring-primary border-gray-300 rounded cursor-pointer"
          />
          <label htmlFor="remember-me" className="mr-2 block text-sm text-gray-900 cursor-pointer">
            تذكرني
          </label>
        </div>

        <div className="text-sm">
          <a href="#" className="font-medium text-primary hover:text-indigo-500 transition-colors">
            نسيت كلمة المرور؟
          </a>
        </div>
      </div>

      <Button
        type="submit"
        isLoading={isLoading}
        className="w-full group"
        size="lg"
      >
        <span className="flex items-center justify-center gap-2">
          تسجيل الدخول
          <ArrowLeft className="h-5 w-5 group-hover:-translate-x-1 transition-transform" />
        </span>
      </Button>
    </form>
  );
}