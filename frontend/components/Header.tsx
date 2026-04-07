"use client";

import Cookies from 'js-cookie';
import { Bell, Menu, Search, UserCircle } from 'lucide-react';

export default function Header() {
  const userCookie = Cookies.get("user");
  let email = "مدير النظام";
  try {
    if (userCookie) email = JSON.parse(userCookie).email;
  } catch {}

  return (
    <header className="sticky top-0 z-40 flex h-16 shrink-0 items-center gap-x-4 border-b border-gray-100/50 bg-white/70 backdrop-blur-xl px-4 shadow-sm sm:gap-x-6 sm:px-6 lg:px-8 transition-colors">
      <button type="button" className="-m-2.5 p-2.5 text-gray-700 lg:hidden">
        <span className="sr-only">Open sidebar</span>
        <Menu className="h-6 w-6" aria-hidden="true" />
      </button>

      {/* Separator */}
      <div className="h-6 w-px bg-gray-200 lg:hidden" aria-hidden="true" />

      <div className="flex flex-1 gap-x-4 self-stretch lg:gap-x-6 items-center justify-between">
        <div className="relative flex flex-1 w-full max-w-md">
          <label htmlFor="search-field" className="sr-only">بحث</label>
          <Search
            className="pointer-events-none absolute inset-y-0 right-0 h-full w-5 text-gray-400 mr-2"
            aria-hidden="true"
          />
          <input
            id="search-field"
            className="block h-full w-full border-0 py-0 pr-10 pl-0 text-gray-900 placeholder:text-gray-400 focus:ring-0 sm:text-sm bg-transparent"
            placeholder="بحث في النظام..."
            type="search"
            name="search"
          />
        </div>
        <div className="flex items-center gap-x-4 lg:gap-x-6">
          <button type="button" className="-m-2.5 p-2.5 text-gray-400 hover:text-gray-500 relative transition-colors">
            <span className="sr-only">View notifications</span>
            <Bell className="h-6 w-6" aria-hidden="true" />
            <span className="absolute top-2 right-2 w-2 h-2 bg-red-500 rounded-full"></span>
          </button>

          {/* Separator */}
          <div className="hidden lg:block lg:h-6 lg:w-px lg:bg-gray-200" aria-hidden="true" />

          {/* Profile dropdown */}
          <div className="flex items-center gap-x-3 cursor-pointer p-1 rounded-full hover:bg-gray-100 transition-colors">
            <UserCircle className="h-9 w-9 text-gray-400" aria-hidden="true" />
            <span className="hidden lg:flex lg:items-center">
              <span className="text-sm font-semibold leading-6 text-gray-900" aria-hidden="true">
                {email}
              </span>
            </span>
          </div>
        </div>
      </div>
    </header>
  );
}
