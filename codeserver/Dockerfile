FROM ghcr.io/fpommerening/container-training.tools:linux-${TARGETARCH} as tools

FROM codercom/code-server:4.17.1-bullseye
COPY --from=tools /tools/kubectl/kubectl /usr/local/bin/kubectl 
COPY --from=tools /tools/k9s/k9s /usr/local/bin/k9s 
COPY --from=tools /tools/helm/helm /usr/local/bin/helm