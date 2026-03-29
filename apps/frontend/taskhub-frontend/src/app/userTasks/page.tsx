import { UserTaskList } from "@/widgets/userTask-list/ui/UserTaskList";
import { AuthGuard } from "@/shared/lib/AuthGuard";

export default function Page() {
    return (
        <AuthGuard>
            <UserTaskList />
        </AuthGuard>
    );
}