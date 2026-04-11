import { TrendingUp, TrendingDown } from "lucide-react";

interface StatCardProps {
    label: string;
    value: string | number;
    icon: React.ReactNode;
    iconColor?: 'indigo' | 'teal' | 'emerald' | 'amber';
    change?: number;
}

export default function StatCard({
    label,
    value,
    icon,
    iconColor = 'indigo',
    change,
}: StatCardProps) {
    return (
        <div className="stat-card">
            <div className={`stat-icon ${iconColor}`}>
                {icon}
            </div>
            <div className="stat-info">
                <div className="stat-label">{label}</div>
                <div className="stat-value">{value}</div>
                {change !== undefined && (
                    <div className={`stat-change ${change >= 0 ? 'positive' : 'negative'}`}>
                        {change >= 0 ? <TrendingUp size={12} /> : <TrendingDown size={12} />}
                        <span>{Math.abs(change)}%</span>
                    </div>
                )}
            </div>
        </div>
    );
}
