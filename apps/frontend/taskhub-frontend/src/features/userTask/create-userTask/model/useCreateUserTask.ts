import { createTask } from "@/entities/userTask/api/userTask.api";

export function useCreateUserTask() {
    const mutate = async (title: string, description?: string) => {
        return createTask({ title, description });
    };

    return { mutate };
}