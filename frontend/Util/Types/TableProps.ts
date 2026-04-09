import { ColumnDef } from "@tanstack/react-table";

export interface TableProps<T> {
  data: T[];
  columns: ColumnDef<T,any>[];
  isLoading?: boolean;
  pageCount: number;
}