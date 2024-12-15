FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MediaReview.Identity.Api/MediaReview.Identity.Api.csproj", "src/MediaReview.Identity.Api/"]
COPY ["src/MediaReview.Identity.Infrastructure/MediaReview.Identity.Infrastructure.csproj", "src/MediaReview.Identity.Infrastructure/"]
COPY ["src/MediaReview.Identity.Domain/MediaReview.Identity.Domain.csproj", "src/MediaReview.Identity.Domain/"]
COPY ["src/MediaReview.Identity.Application/MediaReview.Identity.Application.csproj", "src/MediaReview.Identity.Application/"]
RUN dotnet restore "src/MediaReview.Identity.Api/MediaReview.Identity.Api.csproj"
COPY . .
WORKDIR "/src/src/MediaReview.Identity.Api"
RUN dotnet build "MediaReview.Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MediaReview.Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaReview.Identity.Api.dll"]
