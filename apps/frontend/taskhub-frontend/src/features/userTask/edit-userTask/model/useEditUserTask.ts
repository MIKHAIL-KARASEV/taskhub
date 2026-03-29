import { useState } from "react";
import { updateTask } from "@/entities/userTask/api/userTask.api";

export const useEditUserTask = () => {
    const [loading, setLoading] = useState(false);

    const mutate = async (params: {
        id: string;
        title: string;
        description?: string;
    }) => {
        try {
            setLoading(true);

            await updateTask(params.id, {
                title: params.title,
                description: params.description,
            });
        } catch (e) {
            console.error("Update task failed", e);
            throw e;
        } finally {
            setLoading(false);
        }
    };

    return { mutate, loading };
};