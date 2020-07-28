FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app/src

# copy just csproj/sln files
COPY KitchenService.Api/*.csproj KitchenService.Api/
COPY KitchenService.Core/*.csproj KitchenService.Core/
COPY KitchenService.DataAccess/*.csproj KitchenService.DataAccess/
COPY KitchenService.UnitTests/*.csproj KitchenService.UnitTests/
COPY *.sln ./
RUN dotnet restore

# copy rest of build context into /app/src
COPY . ./

# publish to /app/publish
RUN dotnet publish KitchenService.Api -o ../publish --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

WORKDIR /app

# copy assemblies from build stage into this stage
COPY --from=build /app/publish ./

CMD dotnet KitchenService.Api.dll
