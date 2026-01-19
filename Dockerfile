# Giai đoạn 1: Build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# --- SỬA LẠI ĐOẠN NÀY ĐỂ TRỎ VÀO THƯ MỤC CON ---
COPY ["AiBoardGame/AiBoardGame.csproj", "AiBoardGame/"]
RUN dotnet restore "AiBoardGame/AiBoardGame.csproj"

# Copy toàn bộ code còn lại
COPY . .
WORKDIR "/src/AiBoardGame"
RUN dotnet build "AiBoardGame.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "AiBoardGame.csproj" -c Release -o /app/publish

# Giai đoạn 2: Chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "AiBoardGame.dll"]