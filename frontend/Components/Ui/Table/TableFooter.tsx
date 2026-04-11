import { usePaginationStore } from "@/Context/paginationStore";
import { ChevronLeft, ChevronRight } from "lucide-react";
import { memo } from "react";

function TableFooter({ pageCount }: { pageCount: number }) {
  const setPage = usePaginationStore((state) => state.setPage);
  const page = usePaginationStore((state) => state.pageIndex);

  const pages = Array.from({ length: pageCount }, (_, i) => i + 1);

  return (
    <div className="pagination">
      <button
        className="pagination-btn"
        onClick={() => setPage(page - 1)}
        disabled={page === 1}
        aria-label="الصفحة السابقة"
      >
        <ChevronRight size={16} />
      </button>

      {pages.map((p) => (
        <button
          key={p}
          className={`pagination-btn ${page === p ? "active" : ""}`}
          onClick={() => setPage(p)}
        >
          {p}
        </button>
      ))}

      <button
        className="pagination-btn"
        onClick={() => setPage(page + 1)}
        disabled={page === pageCount}
        aria-label="الصفحة التالية"
      >
        <ChevronLeft size={16} />
      </button>
    </div>
  );
}

export default memo(TableFooter);