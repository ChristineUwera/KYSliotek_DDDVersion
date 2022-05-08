#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["KYSliotek/KYSliotek.csproj", "KYSliotek/"]
COPY ["KYSliotek.Domain/KYSliotek.Domain.csproj", "KYSliotek.Domain/"]
COPY ["KYSliotek.FrameworkNew/KYSliotek.Framework.csproj", "KYSliotek.FrameworkNew/"]
RUN dotnet restore "KYSliotek/KYSliotek.csproj"
COPY . .
WORKDIR "/src/KYSliotek"
RUN dotnet build "KYSliotek.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KYSliotek.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KYSliotek.dll"]
