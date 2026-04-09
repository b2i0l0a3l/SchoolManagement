import { TableRow } from "./TableRow";
import { Table as ReactTable } from "@tanstack/react-table";

interface BodyProps<T> {
  table: ReactTable<T>;
}
export function TableBody<T>({table}: BodyProps<T>) {
  return (
    <tbody>
      {table.getRowModel().rows.map((row) => (
        <TableRow key={row.id} row={row} />
      ))}
    </tbody>
  );
}
