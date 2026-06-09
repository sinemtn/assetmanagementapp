# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and all project files first so restore stays layer-cached.
COPY AssetManagement.sln ./
COPY Server/Server.csproj ./Server/
COPY Auth/Auth.csproj ./Auth/
COPY Printer/Printer.csproj ./Printer/
COPY Sparepart/Sparepart.csproj ./Sparepart/
COPY StockPrinter/StockPrinter.csproj ./StockPrinter/
COPY Toner/Toner.csproj ./Toner/
COPY User/User.csproj ./User/
COPY Customer/Customer.csproj ./Customer/
COPY StockToner/StockToner.csproj ./StockToner/
COPY Role/Role.csproj ./Role/
COPY Supplier/Supplier.csproj ./Supplier/
COPY Assignment/Assignment.csproj ./Assignment/
COPY Complaint/Complaint.csproj ./Complaint/
COPY StockSparepart/StockSparepart.csproj ./StockSparepart/
RUN dotnet restore ./AssetManagement.sln

# Copy the remaining source and publish only the web host.
COPY . ./
RUN dotnet publish ./Server/Server.csproj -c Release -o /app/publish /p:UseAppHost=false

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production
ENV PORT=5000

EXPOSE 5000

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Server.dll"]