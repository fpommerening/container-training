FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim
ARG TARGETARCH
WORKDIR /app/

COPY $TARGETARCH ./

ENTRYPOINT ["./limiter"]