FROM mcr.microsoft.com/dotnet/aspnet:10.0
ARG TARGETARCH
EXPOSE 8080/TCP
WORKDIR /app/

COPY $TARGETARCH ./

ENTRYPOINT ["./Healthcheck"]