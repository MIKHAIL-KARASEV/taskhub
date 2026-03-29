"use client";

import { useAuthStore } from "@/shared/lib/auth.store";
import { login } from "@/entities/user/api/auth.api";
import { useRouter } from "next/navigation";

export function useLogin() {
    const setToken = useAuthStore((s) => s.setToken);
    const router = useRouter();

    const mutate = async (email: string, password: string) => {
        const res = await login({ email, password });

        setToken(res.token);

        router.push("/userTasks");

        return res;
    };

    return { mutate };
}