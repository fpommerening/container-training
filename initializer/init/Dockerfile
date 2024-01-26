FROM mcr.microsoft.com/dotnet/runtime:8.0-bookworm-slim
ARG TARGETARCH
WORKDIR /app/

COPY $TARGETARCH ./

ENTRYPOINT ["./init"]