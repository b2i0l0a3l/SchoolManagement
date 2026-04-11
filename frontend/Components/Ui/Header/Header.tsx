"use client";

import { Search, Bell } from "lucide-react";

interface HeaderProps {
    title: string;
    children?: React.ReactNode;
}

export default function Header({ title, children }: HeaderProps) {
    return (
        <header className="main-header">
            <h1 className="header-title">{title}</h1>
            <div className="header-actions">
                {children}
                <div className="search-bar">
                    <input
                        className="input"
                        placeholder="ابحث عن طالب، معلم، فصل..."
                        type="search"
                    />
                    <Search className="search-icon" />
                </div>
                <button className="notification-btn" aria-label="الإشعارات">
                    <Bell size={18} />
                    <span className="notif-dot" />
                </button>
                <div className="avatar">م</div>
            </div>
        </header>
    );
}
