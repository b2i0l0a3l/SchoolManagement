interface ButtonProps {
    text?: string;
    onClick?: () => void;
    disabled?: boolean;
    className?: string;
    variant?: 'primary' | 'accent' | 'outline' | 'ghost' | 'danger';
    size?: 'sm' | 'md' | 'lg' | 'icon';
    icon?: React.ReactNode;
    loading?: boolean;
}

export default ButtonProps;