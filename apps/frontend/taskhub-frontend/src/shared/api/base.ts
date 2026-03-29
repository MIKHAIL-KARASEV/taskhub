import { useAuthStore } from "../lib/auth.store";

export const API_URL = process.env.NEXT_PUBLIC_API_URL!;

export async function request<T>(
    url: string,
    options?: RequestInit
): Promise<T> {
    const token = useAuthStore.getState().token;

    const res = await fetch(`${API_URL}${url}`, {
        ...options,
        headers: {
            "Content-Type": "application/json",
            ...(token ? { Authorization: `Bearer ${token}` } : {}),
            ...(options?.headers || {}),
        },
    });

    if (!res.ok) {
        if (res.status === 401) {
            useAuthStore.getState().logout();
        }

        throw new Error("API error");
    }

    const text = await res.text();

    return text ? JSON.parse(text) : ({} as T);
}