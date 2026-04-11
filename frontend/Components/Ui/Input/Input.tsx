"use client";

interface InputProps {
    label?: string;
    placeholder?: string;
    value?: string;
    onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
    type?: string;
    icon?: React.ReactNode;
    className?: string;
    name?: string;
    required?: boolean;
    disabled?: boolean;
}

export default function Input({
    label,
    placeholder,
    value,
    onChange,
    type = 'text',
    icon,
    className = '',
    name,
    required,
    disabled,
}: InputProps) {
    return (
        <div className={`input-group ${className}`}>
            {label && <label className="input-label">{label}</label>}
            {icon ? (
                <div className="input-with-icon">
                    <input
                        type={type}
                        placeholder={placeholder}
                        value={value}
                        onChange={onChange}
                        name={name}
                        required={required}
                        disabled={disabled}
                        className="input"
                    />
                    <span className="input-icon">{icon}</span>
                </div>
            ) : (
                <input
                    type={type}
                    placeholder={placeholder}
                    value={value}
                    onChange={onChange}
                    name={name}
                    required={required}
                    disabled={disabled}
                    className="input"
                />
            )}
        </div>
    );
}
