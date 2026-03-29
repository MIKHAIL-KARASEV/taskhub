"use client";


import { useCompleteUserTask } from "@/features/userTask/complete-userTask/model/useCompleteUserTask";
import { useDeleteUserTask } from "@/features/userTask/delete-userTask/model/useDeleteUserTask";
import {useGetUserTasks} from "@/features/userTask/get-userTasks/model/useGetUserTasks";

export function UserTaskList() {
    const { tasks, loading, refetch } = useGetUserTasks();
    const { mutate: complete } = useCompleteUserTask();
    const { mutate: remove } = useDeleteUserTask();

    if (loading) return <div>Loading...</div>;

    return (
        <div>
            {tasks.map((task) => (
                <div key={task.id}>
                    <h3>{task.title}</h3>

                    <button
                        onClick={async () => {
                            await complete(task.id);
                            refetch();
                        }}
                    >
                        Complete
                    </button>

                    <button
                        onClick={async () => {
                            await remove(task.id);
                            refetch();
                        }}
                    >
                        Delete
                    </button>
                </div>
            ))}
        </div>
    );
}