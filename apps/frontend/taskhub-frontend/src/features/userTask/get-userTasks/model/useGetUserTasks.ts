import { useEffect, useState } from "react";
import { getMyTasks } from "@/entities/userTask/api/userTask.api";
import { UserTask } from "@/entities/userTask/model/userTask.types";

export function useGetUserTasks() {
    const [tasks, setTasks] = useState<UserTask[]>([]);
    const [loading, setLoading] = useState(true);

    const fetchTasks = async () => {
        setLoading(true);
        const data = await getMyTasks();
        setTasks(data);
        setLoading(false);
    };

    useEffect(() => {
        fetchTasks();
    }, []);

    return { tasks, loading, refetch: fetchTasks };
}