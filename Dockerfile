FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy csproj and restore dependencies for both projects
COPY *.sln .
COPY IncomeFollowUp.Api/*.csproj ./IncomeFollowUp.Api/
COPY IncomeFollowUp.Application/*.csproj ./IncomeFollowUp.Application/
COPY IncomeFollowUp.Contract/*.csproj ./IncomeFollowUp.Contract/
COPY IncomeFollowUp.Domain/*.csproj ./IncomeFollowUp.Domain/
COPY IncomeFollowUp.Infrastructure/*.csproj ./IncomeFollowUp.Infrastructure/
COPY IncomeFollowUp.Ui/*.csproj ./IncomeFollowUp.Ui/
RUN dotnet restore

# Copy the remaining files and build the application
COPY . .
RUN dotnet build -c Release 

# Publish the WebAPI project
WORKDIR /App/IncomeFollowUp.Api
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/IncomeFollowUp.Api/out .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "IncomeFollowUp.Api.dll"]