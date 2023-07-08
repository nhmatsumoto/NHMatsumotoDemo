#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NHMatsumotoDemo.API/NHMatsumotoDemo.API.csproj", "NHMatsumotoDemo.API/"]
COPY ["NHMatsumotoDemo.Infrastructure.CrossCutting.IoC/NHMatsumotoDemo.Infrastructure.CrossCutting.IoC.csproj", "NHMatsumotoDemo.Infrastructure.CrossCutting.IoC/"]
COPY ["NHMatsumotoDemo.Domain/NHMatsumotoDemo.Domain.csproj", "NHMatsumotoDemo.Domain/"]
COPY ["NHMatsumotoDemo.Infrastructure.Database/NHMatsumotoDemo.Infrastructure.Database.csproj", "NHMatsumotoDemo.Infrastructure.Database/"]
COPY ["NHMatsumotoDemo.Services/NHMatsumotoDemo.Services.csproj", "NHMatsumotoDemo.Services/"]
RUN dotnet restore "NHMatsumotoDemo.API/NHMatsumotoDemo.API.csproj"
COPY . .
WORKDIR "/src/NHMatsumotoDemo.API"
RUN dotnet build "NHMatsumotoDemo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NHMatsumotoDemo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NHMatsumotoDemo.API.dll"]

