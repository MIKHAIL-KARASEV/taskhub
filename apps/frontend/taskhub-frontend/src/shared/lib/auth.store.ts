// src/shared/lib/auth.store.ts
import { create } from "zustand";
import { persist } from "zustand/middleware";

interface AuthState {
    token: string | null;
    isHydrated: boolean;
    setToken: (token: string) => void;
    logout: () => void;
    setHydrated: () => void;
}

export const useAuthStore = create<AuthState>()(
    persist(
        (set) => ({
            token: null,
            isHydrated: false,

            setToken: (token) => set({ token }),

            logout: () => set({ token: null }),

            setHydrated: () => set({ isHydrated: true }),
        }),
        {
            name: "auth-storage",
            onRehydrateStorage: () => (state) => {
                state?.setHydrated();
            },
        }
    )
);