import { flexRender, Table as ReactTable } from "@tanstack/react-table";

interface HeaderProps<T> {
  table: ReactTable<T>;
} 
export function TableHeader<T>({table}: HeaderProps<T>) {
  return (
    <thead>
      {table.getHeaderGroups().map((headerGroup) => (
        <tr key={headerGroup.id}>
          {headerGroup.headers.map((header) => (
            <th key={header.id}>
              {flexRender(header.column.columnDef.header, header.getContext())}
            </th>
          ))}
        </tr>
      ))}
    </thead>
  );
}
