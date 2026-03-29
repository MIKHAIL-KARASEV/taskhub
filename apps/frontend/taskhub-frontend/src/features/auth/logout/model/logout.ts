import { useAuthStore } from '@/shared/lib/auth.store';

export const logout = async () => {
    // try {
    //     await fetch('/api/auth/logout', {
    //         method: 'POST',
    //         credentials: 'include',
    //     });
    // } catch (e) {
    //     console.error('Logout API failed', e);
    // }

    useAuthStore.getState().logout();
};