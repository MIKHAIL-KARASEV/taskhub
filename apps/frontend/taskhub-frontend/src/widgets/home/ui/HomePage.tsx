import { HOME_TITLE, HOME_DESCRIPTION } from '../model';

export function HomePage() {
    return (
        <main className="flex min-h-screen flex-col items-center justify-center bg-gray-100">
            <h1 className="mb-4 text-3xl font-bold text-blue-600">
                {HOME_TITLE}
            </h1>

            <p className="text-gray-600">{HOME_DESCRIPTION}</p>
        </main>
    );
}