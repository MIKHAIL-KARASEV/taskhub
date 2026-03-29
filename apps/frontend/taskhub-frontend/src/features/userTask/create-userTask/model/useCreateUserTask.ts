import { useState } from "react";
import { createTask } from "@/entities/userTask/api/userTask.api";

export const useCreateUserTask = () => {
    const [loading, setLoading] = useState(false);

    const mutate = async (params: {
        title: string;
        description?: string;
    }) => {
        try {
            setLoading(true);

            return await createTask({
                title: params.title,
                description: params.description,
            });
        } catch (e) {
            console.error("Create task failed", e);
            throw e;
        } finally {
            setLoading(false);
        }
    };

    return { mutate, loading };
};