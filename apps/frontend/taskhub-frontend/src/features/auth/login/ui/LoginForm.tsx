"use client";

import { useState } from "react";
import { useLogin } from "../model/useLogin";

export function LoginForm() {
    const { mutate, loading } = useLogin();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const onSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await mutate(email, password);
    };

    return (
        <form onSubmit={onSubmit}>
            <input
                placeholder="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />

            <input
                type="password"
                placeholder="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />

            <button disabled={loading}>
                {loading ? "Loading..." : "Login"}
            </button>
        </form>
    );
}