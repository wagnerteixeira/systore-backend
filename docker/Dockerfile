FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app

COPY *.sln ./
COPY ./src/**/*.csproj ./src/
#COPY ./tests/**/*.csproj ./tests/
# for debian RUN for file in src/*.csproj; do filename=$(basename -s .csproj $file); filepath="src/$filename"; mkdir $filepath; mv $file $filepath; done
# for debian RUN for file in tests/*.csproj; do filename=$(basename -s .csproj $file); filepath="tests/$filename"; mkdir $filepath; mv $file $filepath; done
RUN for file in src/*.csproj; do filename=$(basename $file .csproj); filepath="src/$filename"; mkdir $filepath; mv $file $filepath; done
#RUN for file in tests/*.csproj; do filename=$(basename $file .csproj); filepath="tests/$filename"; mkdir $filepath; mv $file $filepath; done
RUN dotnet restore

COPY ./src ./src
#COPY ./tests ./tests
RUN dotnet publish ./src/*.Api/ -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT [ "dotnet", "Systore.Api.dll" ]
