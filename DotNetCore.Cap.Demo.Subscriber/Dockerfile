#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DotNetCore.Cap.Demo.Subscriber/DotNetCore.Cap.Demo.Subscriber.csproj", "DotNetCore.Cap.Demo.Subscriber/"]
RUN dotnet restore "DotNetCore.Cap.Demo.Subscriber/DotNetCore.Cap.Demo.Subscriber.csproj"
COPY . .
WORKDIR "/src/DotNetCore.Cap.Demo.Subscriber"
RUN dotnet build "DotNetCore.Cap.Demo.Subscriber.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetCore.Cap.Demo.Subscriber.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetCore.Cap.Demo.Subscriber.dll"]