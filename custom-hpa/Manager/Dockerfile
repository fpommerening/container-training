FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim
ARG TARGETARCH
EXPOSE 5001/TCP
EXPOSE 5002/TCP
WORKDIR /app/

COPY $TARGETARCH ./

ENTRYPOINT ["./Manager"]