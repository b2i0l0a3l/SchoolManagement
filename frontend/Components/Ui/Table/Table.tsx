"use client";
import {
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";
import { TableBody } from "./TablBody";
import { TableHeader } from "./TableHeader";
import TableFooter from "./TableFooter";
import { TableProps } from "@/Util/Types/TableProps";



export default function Table<T>({
  data,
  columns,
  pageCount,
}: TableProps<T>) {
  

    const table = useReactTable({
    data: data ?? [],
    columns: columns,
    getCoreRowModel: getCoreRowModel(),
    manualPagination: true,
    pageCount: pageCount,
  });
  

  return (
    <div>
      <table border={1} cellPadding={10}>
        <TableHeader table={table} />
        <TableBody table={table} />
      </table>
      <TableFooter pageCount={pageCount} />
    </div>
  );
}

