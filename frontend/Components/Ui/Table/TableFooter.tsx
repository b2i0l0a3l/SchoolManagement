import { usePaginationStore } from "@/Context/paginationStore";
import { memo } from "react";

function TableFooter({pageCount}: {pageCount: number}) {
  const setPage = usePaginationStore((state)=> state.setPage)
  const page = usePaginationStore((state)=> state.pageIndex)
  return (
    <div className="flex items-center justify-between" style={{ marginTop: 10 }}>
        <button onClick={() => setPage(page - 1)} disabled={page === 1}>Prev</button>
        <span style={{ margin: "0 10px" }}>Page {page}</span>
        <button onClick={() => setPage(page + 1)} disabled={page === 3}>Next</button>
    </div>
  );
}
export default memo(TableFooter);