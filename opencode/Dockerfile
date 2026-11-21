
# ------------------------------------------------------------
# OpenCode 2 + .NET SDK + OpenSpec
# ------------------------------------------------------------

ARG OPENCODE_VERSION=2.3.2

FROM ghcr.io/anomalyco/opencode:${OPENCODE_VERSION}

USER root

# ------------------------------------------------------------
# System dependencies
# ------------------------------------------------------------

RUN apk add --no-cache \
    bash \
    curl \
    git \
    icu-libs \
    krb5-libs \
    libgcc \
    libstdc++ \
    libssl3 \
    tzdata \
    ca-certificates

# ------------------------------------------------------------
# Install .NET SDKs
# ------------------------------------------------------------

RUN curl -fsSL https://dot.net/v1/dotnet-install.sh \
    -o /tmp/dotnet-install.sh \
    && bash /tmp/dotnet-install.sh \
        --channel 10.0 --install-dir /usr/share/dotnet --no-path \
    && rm /tmp/dotnet-install.sh

# ------------------------------------------------------------
# Install OpenSpec CLI (standalone)
# ------------------------------------------------------------

RUN curl -fsSL https://openspec.dev/cli/install.sh \
    -o /tmp/openspec-install.sh \
    && OPENSPEC_INSTALL_DIR=/usr/local/bin \
       sh /tmp/openspec-install.sh \
    && rm /tmp/openspec-install.sh \
    && openspec --version

# ------------------------------------------------------------
# Create unprivileged user
# ------------------------------------------------------------

RUN addgroup -S -g 10001 opencode \
    && adduser -S -D -H -u 10001 \
       -G opencode -h /home/opencode opencode \
    && mkdir -p \
       /home/opencode/.config/opencode \
       /home/opencode/.local/share/opencode \
       /home/opencode/.cache \
       /home/opencode/.dotnet \
       /workspace \
    && chown -R 10001:10001 \
       /home/opencode /workspace

# ------------------------------------------------------------
# Environment
# ------------------------------------------------------------

ENV HOME=/home/opencode

ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="/usr/local/bin:/usr/share/dotnet:${PATH}"

ENV DOTNET_CLI_HOME=/home/opencode/.dotnet
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_NOLOGO=1

ENV NUGET_PACKAGES=/home/opencode/.cache/nuget/packages

# ------------------------------------------------------------
# Runtime
# ------------------------------------------------------------

USER opencode

WORKDIR /workspace

ENTRYPOINT ["opencode"]

CMD ["serve", "--hostname", "0.0.0.0", "--port", "4096"]
