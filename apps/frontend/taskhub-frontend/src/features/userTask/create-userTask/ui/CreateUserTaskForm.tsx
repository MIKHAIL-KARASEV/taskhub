"use client";

import { useState } from "react";
import { useCreateUserTask } from "../model/useCreateUserTask";

interface Props {
    onSuccess: () => void;
}

export const CreateUserTaskForm = ({ onSuccess }: Props) => {
    const { mutate, loading } = useCreateUserTask();

    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [error, setError] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            setError(null);

            await mutate({
                title,
                description,
            });

            setTitle("");
            setDescription("");

            onSuccess(); // refetch
        } catch {
            setError("Failed to create task");
        }
    };

    return (
        <form
            onSubmit={handleSubmit}
            className="bg-white border rounded-2xl p-5 shadow-sm space-y-4"
        >
            <h2 className="text-lg font-semibold">New Task</h2>

            {error && (
                <div className="text-sm text-red-500 bg-red-50 border border-red-200 p-2 rounded-md">
                    {error}
                </div>
            )}

            <input
                placeholder="Task title"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                className="w-full px-3 py-2 border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                required
            />

            <textarea
                placeholder="Description (optional)"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                className="w-full px-3 py-2 border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                rows={3}
            />

            <button
                type="submit"
                disabled={loading || !title}
                className="w-full py-2 rounded-lg text-sm font-medium bg-blue-600 text-white hover:bg-blue-700 transition disabled:opacity-50"
            >
                {loading ? "Creating..." : "Create Task"}
            </button>
        </form>
    );
};