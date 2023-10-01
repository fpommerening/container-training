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

RUN apt-get update && apt-get install -y openssl curl openssh-client sudo shellinabox git && \
    ln -sf '/etc/shellinabox/options-enabled/00+Black on White.css' \
      /etc/shellinabox/options-enabled/00+Black-on-White.css && \
    ln -sf '/etc/shellinabox/options-enabled/00_White On Black.css' \
      /etc/shellinabox/options-enabled/00_White-On-Black.css && \
    ln -sf '/etc/shellinabox/options-enabled/01+Color Terminal.css' \
      /etc/shellinabox/options-enabled/01+Color-Terminal.css && \
    apt-get install -y apt-transport-https gnupg wget mc nano && \
    sudo curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl" && \
    sudo install -o root -g root -m 0755 kubectl /usr/local/bin/kubectl && \
    wget 'https://github.com/derailed/k9s/releases/download/v0.26.7/k9s_Linux_x86_64.tar.gz' && \
    mkdir /tmp/k9s/ && tar -xvf k9s_Linux_x86_64.tar.gz -C /tmp/k9s/ && \
    mv /tmp/k9s/k9s /usr/local/bin/k9s && \
    rm k9s_Linux_x86_64.tar.gz && rm -R /tmp/k9s && \
    wget https://get.helm.sh/helm-v3.10.1-linux-amd64.tar.gz && \
    mkdir /tmp/helm && tar -xvf helm-v3.10.1-linux-amd64.tar.gz -C /tmp/helm && \  
    mv /tmp/helm/linux-amd64/helm /usr/local/bin/helm && \
    rm helm-v3.10.1-linux-amd64.tar.gz && rm -R /tmp/helm  && \
    wget 'https://github.com/linkerd/linkerd2/releases/download/stable-2.12.2/linkerd2-cli-stable-2.12.2-linux-amd64' && \
    chmod +x linkerd2-cli-stable-2.12.2-linux-amd64 &&  mv linkerd2-cli-stable-2.12.2-linux-amd64 /usr/local/bin/linkerd && \
    wget https://download.docker.com/linux/static/stable/x86_64/docker-24.0.6.tgz && \
    mkdir /tmp/docker && tar -xvf docker-24.0.6.tgz -C /tmp/docker && \  
    mv /tmp/docker/docker/docker /usr/local/bin/docker && \
    rm docker-24.0.6.tgz && rm -R /tmp/docker  && \
	  mkdir /usr/local/lib/docker/ && mkdir /usr/local/lib/docker/cli-plugins && \
	  curl -SL https://github.com/docker/compose/releases/download/v2.12.2/docker-compose-linux-x86_64 -o /usr/local/lib/docker/cli-plugins/docker-compose && \
	  chmod +x /usr/local/lib/docker/cli-plugins/docker-compose && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

ADD entrypoint.sh /usr/local/sbin/
