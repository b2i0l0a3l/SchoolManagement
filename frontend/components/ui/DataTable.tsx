"use client";

import React, { useMemo, useState } from "react";
import { Search } from "lucide-react";
import { Input } from "./Input";

export interface ColumnDef<T> {
  header: string;
  accessorKey?: keyof T;
  cell?: (item: T) => React.ReactNode;
}

interface DataTableProps<T> {
  data: T[];
  columns: ColumnDef<T>[];
  isLoading?: boolean;
  searchPlaceholder?: string;
  onSearch?: (value: string) => void;
  searchKey?: keyof T;
  emptyMessage?: string;
}

export function DataTable<T>({
  data,
  columns,
  isLoading,
  searchPlaceholder = "ابحث...",
  onSearch,
  searchKey,
  emptyMessage = "لا يوجد بيانات",
}: DataTableProps<T>) {
  
  const [localSearch, setLocalSearch] = useState("");

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const val = e.target.value;
    setLocalSearch(val);
    if (onSearch) {
      onSearch(val);
    }
  };

  const filteredData = useMemo(() => {
    if (onSearch || !searchKey || !localSearch) return data;
    return data.filter((item) => {
      const val = item[searchKey];
      return String(val).toLowerCase().includes(localSearch.toLowerCase());
    });
  }, [data, localSearch, searchKey, onSearch]);

  return (
    <div className="glass-panel premium-card rounded-2xl overflow-hidden animate-fade-in">
      <div className="p-4 border-b border-gray-100 flex gap-4">
        <div className="w-full max-w-md">
          <Input
            icon={<Search className="h-4 w-4 text-gray-400" />}
            placeholder={searchPlaceholder}
            value={localSearch}
            onChange={handleSearchChange}
          />
        </div>
      </div>

      <div className="overflow-x-auto relative min-h-[200px]">
        {isLoading && (
          <div className="absolute inset-0 bg-white/50 backdrop-blur-sm z-10 flex items-center justify-center">
             <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary border-t-transparent"></div>
          </div>
        )}
        
        <table className="w-full text-right text-sm">
          <thead className="bg-gray-50/50 text-gray-500 font-medium border-b border-gray-100">
            <tr>
              {columns.map((col, idx) => (
                <th key={idx} className="px-6 py-4">
                  {col.header}
                </th>
              ))}
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-100">
            {!isLoading && filteredData.length === 0 && (
              <tr>
                <td colSpan={columns.length} className="px-6 py-8 text-center text-gray-500">
                  {emptyMessage}
                </td>
              </tr>
            )}
            
            {filteredData.map((row, rowIdx) => (
              <tr key={rowIdx} className="hover:bg-gray-50/50 transition-colors">
                {columns.map((col, colIdx) => (
                  <td key={colIdx} className="px-6 py-4 text-gray-600">
                    {col.cell ? col.cell(row) : col.accessorKey ? String(row[col.accessorKey]) : null}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
