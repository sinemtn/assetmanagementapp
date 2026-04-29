# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and all project files to restore dependencies (layer-cached)
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
RUN dotnet restore

# Copy everything else and publish the Server project
COPY . ./
RUN dotnet publish Server/Server.csproj -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Server.dll"]