"use client";

import { useState } from "react";
import { useGetUserTasks } from "@/features/userTask/get-userTasks/model/useGetUserTasks";
import { useDeleteUserTask } from "@/features/userTask/delete-userTask/model/useDeleteUserTask";
import { useToggleUserTask } from "@/features/userTask/toggle-userTask/model/useToggleUserTask";

import { CreateUserTaskForm } from "@/features/userTask/create-userTask/ui/CreateUserTaskForm";
import { EditUserTaskForm } from "@/features/userTask/edit-userTask/ui/EditUserTaskForm";

import { UserTask } from "@/entities/userTask/model/userTask.types";

export function UserTaskList() {
    const { tasks, loading, refetch } = useGetUserTasks();
    const { mutate: remove } = useDeleteUserTask();
    const { mutate: toggle } = useToggleUserTask();

    const [editingTask, setEditingTask] = useState<UserTask | null>(null);
    const [isCreateOpen, setIsCreateOpen] = useState(false);

    if (loading) {
        return (
            <div className="flex justify-center py-10 text-gray-500">
                Loading...
            </div>
        );
    }

    return (
        <div className="max-w-3xl mx-auto p-6 space-y-4">

            {/* 🔥 Header блока */}
            <div className="flex items-center justify-between">
                <h1 className="text-xl font-semibold">My Tasks</h1>

                <button
                    onClick={() => setIsCreateOpen(true)}
                    className="px-4 py-2 text-sm font-medium rounded-lg bg-green-600 text-white hover:bg-green-700 transition"
                >
                    New Task
                </button>
            </div>

            {/* Empty state */}
            {!tasks.length && (
                <div className="text-center text-gray-500 py-10">
                    No tasks yet
                </div>
            )}

            {/* Список */}
            {tasks.map((task) => (
                <div
                    key={task.id}
                    className={`border rounded-xl p-5 shadow-sm transition hover:shadow-md ${
                        task.isCompleted
                            ? "bg-gray-50 opacity-70"
                            : "bg-white"
                    }`}
                >
                    <div className="flex items-center justify-between">
                        <h3
                            className={`text-lg font-semibold ${
                                task.isCompleted
                                    ? "line-through text-gray-400"
                                    : "text-gray-900"
                            }`}
                        >
                            {task.title}
                        </h3>

                        <span
                            className={`text-xs px-2 py-1 rounded-full ${
                                task.isCompleted
                                    ? "bg-green-100 text-green-600"
                                    : "bg-blue-100 text-blue-600"
                            }`}
                        >
                            {task.isCompleted ? "Completed" : "Active"}
                        </span>
                    </div>

                    {task.description && (
                        <p className="text-sm text-gray-500 mt-2">
                            {task.description}
                        </p>
                    )}

                    <div className="flex items-center justify-between mt-4">
                        <span className="text-xs text-gray-400">
                            Created: {new Date(task.createdAt).toLocaleDateString()}
                        </span>

                        <div className="flex gap-2">
                            <button
                                onClick={async () => {
                                    await toggle({
                                        id: task.id,
                                        isCompleted: task.isCompleted,
                                    });
                                    refetch();
                                }}
                                className={`px-3 py-1 text-sm rounded-md transition ${
                                    task.isCompleted
                                        ? "bg-gray-200 text-gray-700 hover:bg-gray-300"
                                        : "bg-green-500 text-white hover:bg-green-600"
                                }`}
                            >
                                {task.isCompleted ? "Undo" : "Complete"}
                            </button>

                            <button
                                onClick={() => setEditingTask(task)}
                                className="px-3 py-1 text-sm rounded-md border border-gray-400 hover:bg-gray-100"
                            >
                                Edit
                            </button>

                            <button
                                onClick={async () => {
                                    await remove(task.id);
                                    refetch();
                                }}
                                className="px-3 py-1 text-sm rounded-md border border-red-500 text-red-500 hover:bg-red-500 hover:text-white transition"
                            >
                                Delete
                            </button>
                        </div>
                    </div>
                </div>
            ))}

            {/* Create modal */}
            {isCreateOpen && (
                <div className="fixed inset-0 z-50 flex items-center justify-center">
                    <div
                        className="absolute inset-0 bg-black/40"
                        onClick={() => setIsCreateOpen(false)}
                    />

                    <div className="relative w-full max-w-md mx-4">
                        <CreateUserTaskForm
                            onSuccess={() => {
                                setIsCreateOpen(false);
                                refetch();
                            }}
                        />
                    </div>
                </div>
            )}

            {/* Edit modal */}
            {editingTask && (
                <EditUserTaskForm
                    task={editingTask}
                    onClose={() => setEditingTask(null)}
                    onSuccess={refetch}
                />
            )}
        </div>
    );
}