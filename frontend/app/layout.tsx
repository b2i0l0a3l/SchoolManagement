import type { Metadata } from "next";
import { Geist, Outfit } from "next/font/google";
import "./globals.css";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const outfit = Outfit({
  variable: "--font-outfit",
  subsets: ["latin"],
  weight: ['300', '400', '500', '600', '700'],
});

import { Toaster } from "react-hot-toast";
import QueryProvider from "@/providers/QueryProvider";

export const metadata: Metadata = {
  title: "School Management System",
  description: "Modern web application for managing schools",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html
      lang="ar" dir="rtl"
      className={`${geistSans.variable} ${outfit.variable} antialiased h-full`}
    >
      <body className="min-h-full flex flex-col font-sans bg-gray-50 text-slate-800">
        <QueryProvider>
            <Toaster position="top-center" />
            {children}
        </QueryProvider>
      </body>
    </html>
  );
}
