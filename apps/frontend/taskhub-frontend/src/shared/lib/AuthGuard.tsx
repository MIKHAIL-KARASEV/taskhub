"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuthStore } from "./auth.store";

export function AuthGuard({ children }: { children: React.ReactNode }) {
    const token = useAuthStore((s) => s.token);
    const isHydrated = useAuthStore((s) => s.isHydrated);
    const router = useRouter();

    useEffect(() => {
        if (isHydrated && !token) {
            router.push("/login");
        }
    }, [isHydrated, token]);

    if (!isHydrated) return null;

    if (!token) return null;

    return <>{children}</>;
}