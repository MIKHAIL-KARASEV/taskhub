'use client';

import { useRouter } from 'next/navigation';
import { useAuthStore } from '@/shared/lib/auth.store';
import { logout } from '@/features/auth/logout/model/logout';

export const Header = () => {
    const router = useRouter();

    const token = useAuthStore((s) => s.token);
    const isHydrated = useAuthStore((s) => s.isHydrated);

    const isAuth = !!token;

    const handleLogout = async () => {
        await logout();
        router.push('/login');
    };

    if (!isHydrated) return null;

    return (
        <header className="w-full border-b bg-white/80 backdrop-blur sticky top-0 z-50">
            <div className="max-w-6xl mx-auto flex items-center justify-between px-6 py-4">

                {/* Logo */}
                <div
                    onClick={() => router.push('/')}
                    className="text-xl font-bold cursor-pointer hover:opacity-80 transition"
                >
                    TaskHub
                </div>

                {/* Actions */}
                <div className="flex items-center gap-4">
                    {isAuth ? (
                        <button
                            onClick={handleLogout}
                            className="px-4 py-2 text-sm font-medium rounded-lg border border-red-500 text-red-500 hover:bg-red-500 hover:text-white transition"
                        >
                            Logout
                        </button>
                    ) : (
                        <button
                            onClick={() => router.push('/login')}
                            className="px-4 py-2 text-sm font-medium rounded-lg bg-blue-600 text-white hover:bg-blue-700 transition"
                        >
                            Login
                        </button>
                    )}
                </div>
            </div>
        </header>
    );
};