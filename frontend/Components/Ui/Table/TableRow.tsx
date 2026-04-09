import { Row, flexRender } from "@tanstack/react-table";
import { memo } from "react";

interface RowProps<T> {
  row: Row<T>;
}

function TableRowInner<T>({ row }: RowProps<T>) {
  return (
    <tr>
      {row.getVisibleCells().map((cell) => (
        <td key={cell.id}>
          {flexRender(
            cell.column.columnDef.cell,
            cell.getContext()
          )}
        </td>
      ))}
    </tr>
  );
}

export const TableRow = memo(TableRowInner) as typeof TableRowInner;