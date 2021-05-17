FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt install -y nodejs
WORKDIR /app
COPY . .
RUN dotnet restore
#Publish to out directory
RUN dotnet publish -c Release -o out

FROM nginx:alpine
EXPOSE 80
COPY --from=build-env /app/out/wwwroot /usr/share/nginx/html