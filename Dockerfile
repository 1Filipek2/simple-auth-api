FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY SimpleAuthApi.slnx ./

COPY src/SimpleAuthApi.Domain/*.csproj src/SimpleAuthApi.Domain/
COPY src/SimpleAuthApi.Application/*.csproj src/SimpleAuthApi.Application/
COPY src/SimpleAuthApi.Infrastructure/*.csproj src/SimpleAuthApi.Infrastructure/
COPY src/SimpleAuthApi.WebApi/*.csproj src/SimpleAuthApi.WebApi/

RUN dotnet restore SimpleAuthApi.slnx

COPY . .
RUN dotnet publish src/SimpleAuthApi.WebApi/*.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "SimpleAuthApi.WebApi.dll"]