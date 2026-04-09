"use client";

import Table from "@/Components/Ui/Table/Table";
import { usePaginationStore } from "@/Context/paginationStore";
import { createColumnHelper } from "@tanstack/react-table";
import { useMemo } from "react";

export default function Home() {
  
  type user = {
    id: number;
    name: string;
    age: number;
    email: string;
  }
  const data : user[] = [
    { id: 1, name: "John Doe", age: 30, email: "[EMAIL_ADDRESS]" },
    { id: 2, name: "ahmed", age: 25, email: "[EMAIL_ADDRESS]" },
    { id: 3, name: "mohamed", age: 28, email: "[EMAIL_ADDRESS]" },
     { id: 5, name: "John Doe", age: 30, email: "[EMAIL_ADDRESS]" },
    { id: 6, name: "ahmed", age: 25, email: "[EMAIL_ADDRESS]" },
    { id: 7, name: "mohamed", age: 28, email: "[EMAIL_ADDRESS]" },
     { id: 8, name: "John Doe", age: 30, email: "[EMAIL_ADDRESS]" },
    { id: 9, name: "ahmed", age: 25, email: "[EMAIL_ADDRESS]" },
    { id: 10, name: "mohamed", age: 28, email: "[EMAIL_ADDRESS]" },

  ]
  const columnHelper = createColumnHelper<user>();
 

  const columns = useMemo( ()=>{
    return [
      columnHelper.accessor("id", {
        header: "ID",
        cell: (info) => <span className="text-primary">{info.getValue()}</span>,
      }),
      columnHelper.accessor("name", {
        header: "Name",
      }),
      columnHelper.accessor("age", {
        header: "Age",
      }),
      columnHelper.accessor("email", {
        header: "Email",
      }),
    ]
  },[]);
  const page = usePaginationStore((state)=> state.pageIndex)
  const pageSize = usePaginationStore((state)=> state.pageSize)

  const filterData = useMemo(()=>{
    return data.slice((page - 1) * pageSize, page * pageSize)
  },[data])

  return (
    <div className="flex h-screen w-screen items-center justify-center bg-gray-50">
      <Table data={filterData}  pageCount={3} columns={columns} />
    </div>
  );
}
