"use client";

import { useState } from "react";
import { useEditUserTask } from "../model/useEditUserTask";
import { UserTask } from "@/entities/userTask/model/userTask.types";

interface Props {
    task: UserTask;
    onClose: () => void;
    onSuccess: () => void;
}

export const EditUserTaskForm = ({ task, onClose, onSuccess }: Props) => {
    const { mutate, loading } = useEditUserTask();

    const [title, setTitle] = useState(task.title);
    const [description, setDescription] = useState(task.description || "");
    const [error, setError] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            setError(null);

            await mutate({
                id: task.id,
                title,
                description,
            });

            onSuccess(); // refetch
            onClose();   // закрыть модалку
        } catch (e: any) {
            setError("Failed to update task");
        }
    };

    return (
        <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-50">
            <form
                onSubmit={handleSubmit}
                className="w-full max-w-md bg-white rounded-2xl shadow-lg p-6 space-y-4"
            >
                {/* Title */}
                <h2 className="text-lg font-semibold text-gray-900">
                    Edit Task
                </h2>

                {/* Error */}
                {error && (
                    <div className="text-sm text-red-500 bg-red-50 border border-red-200 p-2 rounded-md">
                        {error}
                    </div>
                )}

                {/* Title input */}
                <div className="flex flex-col gap-1">
                    <label className="text-sm text-gray-600">Title</label>
                    <input
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        className="px-3 py-2 border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                        required
                    />
                </div>

                {/* Description */}
                <div className="flex flex-col gap-1">
                    <label className="text-sm text-gray-600">
                        Description
                    </label>
                    <textarea
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        className="px-3 py-2 border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                        rows={3}
                    />
                </div>

                {/* Actions */}
                <div className="flex justify-end gap-2 pt-2">
                    <button
                        type="button"
                        onClick={onClose}
                        className="px-4 py-2 text-sm border rounded-lg hover:bg-gray-100 transition"
                    >
                        Cancel
                    </button>

                    <button
                        type="submit"
                        disabled={loading || !title}
                        className="px-4 py-2 text-sm font-medium bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition disabled:opacity-50"
                    >
                        {loading ? "Saving..." : "Save"}
                    </button>
                </div>
            </form>
        </div>
    );
};