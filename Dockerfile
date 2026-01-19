# Giai đoạn 1: Build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy file dự án và tải thư viện
COPY ["AiBoardGame.csproj", "./"]
RUN dotnet restore "AiBoardGame.csproj"

# Copy toàn bộ code và build
COPY . .
RUN dotnet build "AiBoardGame.csproj" -c Release -o /app/build

# Publish ra thư mục /app/publish
FROM build AS publish
RUN dotnet publish "AiBoardGame.csproj" -c Release -o /app/publish

# Giai đoạn 2: Chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Mở cổng 8080 (Mặc định của .NET 9 Container)
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "AiBoardGame.dll"]