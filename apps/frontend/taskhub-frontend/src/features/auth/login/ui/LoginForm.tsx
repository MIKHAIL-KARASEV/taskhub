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
        <div className="min-h-[80vh] flex items-center justify-center px-4">
            <form
                onSubmit={onSubmit}
                className="w-full max-w-md bg-white p-8 rounded-2xl shadow-sm border space-y-5"
            >
                {/* Title */}
                <div className="text-center">
                    <h1 className="text-2xl font-semibold text-gray-900">
                        Login
                    </h1>
                    <p className="text-sm text-gray-500 mt-1">
                        Welcome back
                    </p>
                </div>

                {/*/!* Error *!/*/}
                {/*{error && (*/}
                {/*    <div className="text-sm text-red-500 bg-red-50 border border-red-200 p-2 rounded-md">*/}
                {/*        {error}*/}
                {/*    </div>*/}
                {/*)}*/}

                {/* Email */}
                <div className="flex flex-col gap-1">
                    <label className="text-sm text-gray-600">Email</label>
                    <input
                        type="email"
                        placeholder="you@example.com"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        className="px-3 py-2 border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>

                {/* Password */}
                <div className="flex flex-col gap-1">
                    <label className="text-sm text-gray-600">Password</label>
                    <input
                        type="password"
                        placeholder="••••••••"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        className="px-3 py-2 border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>

                {/* Button */}
                <button
                    type="submit"
                    disabled={loading}
                    className="w-full py-2 rounded-lg text-sm font-medium bg-blue-600 text-white hover:bg-blue-700 transition disabled:opacity-50"
                >
                    {loading ? "Signing in..." : "Login"}
                </button>
            </form>
        </div>
    );
}