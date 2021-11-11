# OS Architeture amd64|arm64v8
ARG ARC=amd64

FROM $ARC/alpine:3.13

RUN apk add --no-cache \
        ca-certificates \
        \
        # .NET Core dependencies
        krb5-libs \
        libgcc \
        libintl \
        libssl1.1 \
        libstdc++ \
        zlib \
        \
        # NET Sdk Dependencies
        icu-libs \
        git 

ENV \
    # Configure web servers to bind to port 80 when present
    ASPNETCORE_URLS=http://+:80 \
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true \   
    # Do not generate certificate
    DOTNET_GENERATE_ASPNET_CERTIFICATE=false \
    # .NET Version
    DOTNET_VERSION=5.0.11 \
    # SDK version 
    DOTNET_SDK_VERSION=5.0.402 \
    # SDK OS architecture x64|arm64
    DOTNET_SDK_ARC=x64 \
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip \
    # Enable globalization APIs (icu-libs libs included) see https://github.com/dotnet/announcements/issues/20 and https://aka.ms/dotnet-globalization-alpine-containers for more information.
    # Set the invariant mode since icu-libs isn't included (see https://github.com/dotnet/announcements/issues/20)
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    LC_ALL=en_US.UTF-8 \
    LANG=en_US.UTF-8 \
    # Set to opt out of the telemetry feature
    DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    # Disable .NET welcome message
    DOTNET_NOLOGO=true

# Install .NET SDK
RUN wget -O dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Sdk/$DOTNET_SDK_VERSION/dotnet-sdk-$DOTNET_SDK_VERSION-linux-musl-$DOTNET_SDK_ARC.tar.gz \
    && dotnet_sha512='6144ef0922c0bb67a2a1692d296df328ee8eeebfc631cc3774c4e87b352128a95ebf36419ce874151fdcc5cfd7d083ce26de998aa7a02be1eda58880baf2b845' \
    && echo "$dotnet_sha512  dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -oxzf dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    && rm dotnet.tar.gz

# Trigger first run experience by running arbitrary cmd
RUN dotnet help

# Create folder for .NET Core SDK tools
RUN mkdir /opt/dotnet-tools

# Install SonnarScanner for .NET
RUN dotnet tool install dotnet-sonarscanner --tool-path /opt/dotnet-tools --version 5.3.2

# Add .NET Core SDK tools
ENV PATH="/opt/dotnet-tools:${PATH}"

# Create cicd folder
RUN mkdir /home/cicd

RUN apk add --no-cache openjdk11

ENV \
    JAVA_HOME=/usr/lib/jvm/java-11-openjdk \
    PATH=$PATH:/usr/lib/jvm/java-11-openjdk/bin

WORKDIR /home/cicd