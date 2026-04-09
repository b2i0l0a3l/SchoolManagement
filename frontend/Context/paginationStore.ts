import { create } from "zustand";

type PaginationState = {
  pageIndex: number;
  pageSize: number;
  setPage: (page: number) => void;
};

export const usePaginationStore = create<PaginationState>((set) => ({
  pageIndex: 1,
  pageSize: 3,
  setPage: (page) => set({ pageIndex: page >= 1 ? page : 1}),
}));