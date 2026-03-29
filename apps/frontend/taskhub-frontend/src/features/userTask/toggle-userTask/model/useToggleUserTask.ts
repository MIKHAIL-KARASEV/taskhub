import { useState } from "react";
import {
    completeTask,
    uncompleteTask,
} from "@/entities/userTask/api/userTask.api";

export const useToggleUserTask = () => {
    const [loading, setLoading] = useState(false);

    const mutate = async (params: {
        id: string;
        isCompleted: boolean;
    }) => {
        try {
            setLoading(true);

            if (params.isCompleted) {
                await uncompleteTask(params.id);
            } else {
                await completeTask(params.id);
            }
        } catch (e) {
            console.error("Toggle task failed", e);
            throw e;
        } finally {
            setLoading(false);
        }
    };

    return { mutate, loading };
};