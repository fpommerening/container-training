FROM ghcr.io/fpommerening/container-training.tools:linux-${TARGETARCH} as tools

FROM codercom/code-server:4.96.4-ubuntu
COPY --from=tools /tools/kubectl/kubectl /usr/local/bin/kubectl 
COPY --from=tools /tools/k9s/k9s /usr/local/bin/k9s 
COPY --from=tools /tools/helm/helm /usr/local/bin/helm
COPY --from=tools /tools/kubelogin/kubectl-oidc_login /usr/local/bin/kubectl-oidc_login
