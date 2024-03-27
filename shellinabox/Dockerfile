FROM ghcr.io/fpommerening/container-training.tools:linux-${TARGETARCH} as tools

FROM ubuntu:22.04

ENV SIAB_USERCSS="Normal:+/etc/shellinabox/options-enabled/00+Black-on-White.css,Reverse:-/etc/shellinabox/options-enabled/00_White-On-Black.css;Colors:+/etc/shellinabox/options-enabled/01+Color-Terminal.css,Monochrome:-/etc/shellinabox/options-enabled/01_Monochrome.css" \
    SIAB_PORT=4200 \
    SIAB_ADDUSER=true \
    SIAB_USER=coder \
    SIAB_USERID=1000 \
    SIAB_GROUP=coder \
    SIAB_GROUPID=1000 \
    SIAB_PASSWORD=putsafepasswordhere \
    SIAB_SHELL=/bin/bash \
    SIAB_HOME=/home/coder \
    SIAB_SUDO=false \
    SIAB_SSL=true \
    SIAB_SERVICE=/:LOGIN \
    SIAB_PKGS=none \
    SIAB_SCRIPT=none \
    TZ=Europe/Berlin

EXPOSE 4200
VOLUME /etc/shellinabox /var/log/supervisor /home

ENTRYPOINT ["entrypoint.sh"]
CMD ["shellinabox"]

RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
RUN mkdir /usr/local/lib/docker && mkdir /usr/local/lib/docker/cli-plugins

COPY --from=tools /tools/kubectl/kubectl /usr/local/bin/kubectl 
COPY --from=tools /tools/kubelogin/kubectl-oidc_login /usr/local/bin/kubectl-oidc_login 
COPY --from=tools /tools/k9s/k9s /usr/local/bin/k9s 
COPY --from=tools /tools/kubeseal/kubeseal /usr/local/bin/kubeseal 
COPY --from=tools /tools/helm/helm /usr/local/bin/helm 
COPY --from=tools /tools/linkerd/linkerd /usr/local/bin/linkerd 
COPY --from=tools /tools/docker/docker /usr/local/bin/docker 
COPY --from=tools /tools/docker/docker-compose /usr/local/lib/docker/cli-plugins/docker-compose 
COPY --from=tools /tools/docker/docker-buildx /usr/local/lib/docker/cli-plugins/docker-buildx 
ADD entrypoint.sh /usr/local/sbin/

RUN apt-get update && apt-get install -y openssl curl openssh-client shellinabox git tmux && \
    ln -sf '/etc/shellinabox/options-enabled/00+Black on White.css' \
      /etc/shellinabox/options-enabled/00+Black-on-White.css && \
    ln -sf '/etc/shellinabox/options-enabled/00_White On Black.css' \
      /etc/shellinabox/options-enabled/00_White-On-Black.css && \
    ln -sf '/etc/shellinabox/options-enabled/01+Color Terminal.css' \
      /etc/shellinabox/options-enabled/01+Color-Terminal.css && \
    apt-get install -y apt-transport-https gnupg wget mc nano && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

