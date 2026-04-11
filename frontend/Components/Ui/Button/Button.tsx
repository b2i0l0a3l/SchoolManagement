import ButtonProps from "@/Util/Types/ButtonProps";

type ButtonVariant = 'primary' | 'accent' | 'outline' | 'ghost' | 'danger';
type ButtonSize = 'sm' | 'md' | 'lg' | 'icon';

interface ExtendedButtonProps extends ButtonProps {
    variant?: ButtonVariant;
    size?: ButtonSize;
    icon?: React.ReactNode;
    loading?: boolean;
}

const variantClasses: Record<ButtonVariant, string> = {
    primary: 'btn-primary',
    accent: 'btn-accent',
    outline: 'btn-outline',
    ghost: 'btn-ghost',
    danger: 'btn-danger',
};

const sizeClasses: Record<ButtonSize, string> = {
    sm: 'btn-sm',
    md: '',
    lg: 'btn-lg',
    icon: 'btn-icon',
};

export default function Button({
    text,
    onClick,
    disabled,
    className = '',
    variant = 'primary',
    size = 'md',
    icon,
    loading = false,
}: ExtendedButtonProps) {
    return (
        <button
            onClick={onClick}
            disabled={disabled || loading}
            className={`btn ${variantClasses[variant]} ${sizeClasses[size]} ${className}`}
        >
            {loading ? (
                <span className="spinner" style={{ width: 16, height: 16, borderWidth: 2 }} />
            ) : (
                <>
                    {icon && <span className="btn-icon-inner">{icon}</span>}
                    {text && <span>{text}</span>}
                </>
            )}
        </button>
    );
}