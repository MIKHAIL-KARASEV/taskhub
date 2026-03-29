import { useState } from "react";
import { registerUser } from "@/entities/user/api/auth.api";
import { useAuthStore } from "@/shared/lib/auth.store";

export const useRegister = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const setToken = useAuthStore((s) => s.setToken);

    const mutate = async (params: {
        email: string;
        password: string;
    }) => {
        try {
            setLoading(true);
            setError(null);

            const res = await registerUser(params);

            setToken(res.token); // сразу логиним

        } catch (e: any) {
            setError(e.message || "Registration failed");
            throw e;
        } finally {
            setLoading(false);
        }
    };

    return { mutate, loading, error };
};