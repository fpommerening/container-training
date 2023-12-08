FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim
ARG TARGETARCH
EXPOSE 80/TCP
WORKDIR /app/

COPY $TARGETARCH ./

ENTRYPOINT ["./led-matrix"]