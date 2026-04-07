import React from "react";

export interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label?: string;
  error?: string;
  icon?: React.ReactNode;
}

export const Input = React.forwardRef<HTMLInputElement, InputProps>(
  ({ className = "", label, error, icon, ...props }, ref) => {
    return (
      <div className="w-full">
        {label && (
          <label className="block text-sm font-medium text-gray-700 mb-1">
            {label}
          </label>
        )}
        <div className="relative">
          {icon && (
            <div className="absolute inset-y-0 right-0 pr-3 flex items-center pointer-events-none">
              {icon}
            </div>
          )}
          <input
            ref={ref}
            className={`
              appearance-none block w-full px-3 py-2 border rounded-xl shadow-sm placeholder-gray-400 
              focus:outline-none focus:ring-2 focus:ring-primary focus:border-primary transition-all sm:text-sm bg-white/50 backdrop-blur-sm
              ${icon ? "pr-10" : ""}
              ${error ? "border-red-500 focus:ring-red-500 focus:border-red-500" : "border-gray-300"}
              ${className}
            `}
            {...props}
          />
        </div>
        {error && <p className="mt-1 text-sm text-red-600">{error}</p>}
      </div>
    );
  }
);

Input.displayName = "Input";
