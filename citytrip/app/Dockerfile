FROM mcr.microsoft.com/dotnet/aspnet:7.0.11-bullseye-slim
ARG TARGETARCH
EXPOSE 80/TCP
WORKDIR /app/

COPY $TARGETARCH ./

ENTRYPOINT ["./app"]