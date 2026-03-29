import { request } from "@/shared/api/base";

export interface LoginDto {
    email: string;
    password: string;
}

export interface AuthResponse {
    token: string;
}

export const login = (dto: LoginDto) =>
    request<AuthResponse>("/auth/login", {
        method: "POST",
        body: JSON.stringify(dto),
    });

export const registerUser = (dto: LoginDto) =>
    request<{ token: string }>("/auth/register", {
        method: "POST",
        body: JSON.stringify(dto),
    });

// export const register = (dto: LoginDto) =>
//     request<AuthResponse>("/api/auth/register", {
//         method: "POST",
//         body: JSON.stringify(dto),
//     });