import { completeTask } from "@/entities/userTask/api/userTask.api";

export function useCompleteUserTask() {
    const mutate = async (id: string) => {
        return completeTask(id);
    };

    return { mutate };
}