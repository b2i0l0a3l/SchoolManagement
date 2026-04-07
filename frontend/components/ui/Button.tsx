import React from "react";

export interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: "primary" | "secondary" | "danger" | "ghost";
  size?: "sm" | "md" | "lg";
  isLoading?: boolean;
}

export const Button = React.forwardRef<HTMLButtonElement, ButtonProps>(
  ({ className = "", variant = "primary", size = "md", isLoading, children, disabled, ...props }, ref) => {
    
    let baseStyles = "inline-flex items-center justify-center font-medium transition-colors focus:outline-none focus:ring-2 focus:ring-offset-2 rounded-xl disabled:opacity-50 disabled:pointer-events-none";
    
    let variantStyles = "";
    switch (variant) {
      case "primary":
        variantStyles = "bg-primary text-white hover:bg-indigo-600 focus:ring-primary shadow-sm";
        break;
      case "secondary":
        variantStyles = "bg-secondary text-secondary-foreground hover:bg-gray-200 focus:ring-gray-400";
        break;
      case "danger":
        variantStyles = "bg-red-500 text-white hover:bg-red-600 focus:ring-red-500 shadow-sm";
        break;
      case "ghost":
        variantStyles = "bg-transparent text-gray-700 hover:bg-gray-100 hover:text-gray-900 focus:ring-gray-400";
        break;
    }

    let sizeStyles = "";
    switch (size) {
      case "sm":
        sizeStyles = "h-8 px-3 text-xs";
        break;
      case "md":
        sizeStyles = "h-10 px-4 py-2 text-sm";
        break;
      case "lg":
        sizeStyles = "h-12 px-6 py-3 text-base";
        break;
    }

    return (
      <button
        ref={ref}
        disabled={disabled || isLoading}
        className={`${baseStyles} ${variantStyles} ${sizeStyles} ${className}`}
        {...props}
      >
        {isLoading && (
          <svg className="mr-2 h-4 w-4 animate-spin text-current" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
            <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
            <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
          </svg>
        )}
        {children}
      </button>
    );
  }
);

Button.displayName = "Button";
