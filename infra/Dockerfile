FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./src/**/*.csproj ./
RUN for file in ./*.csproj; do filename=$(basename -s .csproj $file); filepath="./$filename"; mkdir $filepath; mv $file $filepath; done
RUN dotnet restore "Systore.Api/Systore.Api.csproj"
COPY ./src .
WORKDIR "/src/Systore.Api"
RUN dotnet build "Systore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Systore.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Systore.Api.dll"]
