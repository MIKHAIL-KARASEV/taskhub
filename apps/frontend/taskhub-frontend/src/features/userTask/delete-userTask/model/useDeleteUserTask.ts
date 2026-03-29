import { deleteTask } from "@/entities/userTask/api/userTask.api";

export function useDeleteUserTask() {
    const mutate = async (id: string) => {
        return deleteTask(id);
    };

    return { mutate };
}