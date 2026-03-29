"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { useRegister } from "../model/useRegister";

export function RegisterForm() {
    const router = useRouter();
    const { mutate, loading, error } = useRegister();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const onSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            await mutate({ email, password });
            router.push("/userTasks"); // редирект после регистрации
        } catch {}
    };

    return (
        <div className="min-h-[80vh] flex items-center justify-center px-4">
            <form className="w-full max-w-md bg-white p-8 rounded-2xl shadow-sm border space-y-5"
                  onSubmit={onSubmit}
            >
                <div className="text-center">
                    <h1 className="text-2xl font-semibold">Register</h1>
                    <p className="text-sm text-gray-500 mt-1">
                        Create your account
                    </p>
                </div>

                {error && (
                    <div className="text-sm text-red-500 bg-red-50 border border-red-200 p-2 rounded-md">
                        {error}
                    </div>
                )}

                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    className="w-full px-3 py-2 border rounded-lg text-sm focus:ring-2 focus:ring-blue-500"
                    required
                />

                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className="w-full px-3 py-2 border rounded-lg text-sm focus:ring-2 focus:ring-blue-500"
                    required
                />

                <button
                    disabled={loading}
                    className="w-full py-2 rounded-lg bg-blue-600 text-white hover:bg-blue-700 transition disabled:opacity-50"
                >
                    {loading ? "Creating..." : "Register"}
                </button>

                <p className="text-sm text-center text-gray-500">
                    Already have an account?{" "}
                    <span
                        onClick={() => router.push("/login")}
                        className="text-blue-600 cursor-pointer hover:underline"
                    >
                        Login
                    </span>
                </p>
            </form>
        </div>
    );
}