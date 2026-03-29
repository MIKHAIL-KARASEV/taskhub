import { request } from "@/shared/api/base";
import { UserTask } from "../model/userTask.types";

export const getMyTasks = () =>
    request<UserTask[]>("/userTasks");

export const createTask = (data: {
    title: string;
    description?: string;
}) =>
    request<UserTask>("/userTasks", {
        method: "POST",
        body: JSON.stringify(data),
    });

export const updateTask = (
    id: string,
    data: { title: string; description?: string }
) =>
    request<UserTask>(`/userTasks/${id}`, {
        method: "PUT",
        body: JSON.stringify(data),
    });

export const deleteTask = (id: string) =>
    request(`/userTasks/${id}`, {
        method: "DELETE",
    });

export const completeTask = (id: string) =>
    request(`/userTasks/${id}/complete`, {
        method: "POST",
    });

export const uncompleteTask = (id: string) =>
    request(`/userTasks/${id}/uncomplete`, {
        method: "POST",
    });