#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Simple-Organizer-Backend/Simple-Organizer-Backend.csproj", "Simple-Organizer-Backend/"]
RUN dotnet restore "Simple-Organizer-Backend/Simple-Organizer-Backend.csproj"
COPY . .
WORKDIR "/src/Simple-Organizer-Backend"
RUN dotnet build "Simple-Organizer-Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simple-Organizer-Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simple-Organizer-Backend.dll"]