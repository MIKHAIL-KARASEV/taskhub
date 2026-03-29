FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "TaskHub.Api/TaskHub.Api.csproj"
RUN dotnet publish "TaskHub.Api/TaskHub.Api.csproj" -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /out .

ENTRYPOINT ["dotnet", "TaskHub.Api.dll"]