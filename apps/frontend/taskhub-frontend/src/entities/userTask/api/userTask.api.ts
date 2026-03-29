import { request } from "@/shared/api/base";
import { UserTask } from "../model/userTask.types";

export const getMyTasks = () =>
    request<UserTask[]>("/api/userTasks");

export const createTask = (data: {
    title: string;
    description?: string;
}) =>
    request<UserTask>("/api/userTasks", {
        method: "POST",
        body: JSON.stringify(data),
    });

export const updateTask = (
    id: string,
    data: { title: string; description?: string }
) =>
    request<UserTask>(`/api/userTasks/${id}`, {
        method: "PUT",
        body: JSON.stringify(data),
    });

export const deleteTask = (id: string) =>
    request(`/api/userTasks/${id}`, {
        method: "DELETE",
    });

export const completeTask = (id: string) =>
    request(`/api/userTasks/${id}/complete`, {
        method: "POST",
    });

export const uncompleteTask = (id: string) =>
    request(`/api/userTasks/${id}/uncomplete`, {
        method: "POST",
    });