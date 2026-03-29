"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { useAuthStore } from "@/shared/lib/auth.store";
import { login } from "@/entities/user/api/auth.api";

export function useLogin() {
    const setToken = useAuthStore((s) => s.setToken);
    const router = useRouter();
    const [loading, setLoading] = useState(false);

    const mutate = async (email: string, password: string) => {
        setLoading(true);

        try {
            const res = await login({ email, password });

            setToken(res.token);
            router.push("/userTasks");

            return res;
        } finally {
            setLoading(false);
        }
    };

    return { mutate, loading };
}
