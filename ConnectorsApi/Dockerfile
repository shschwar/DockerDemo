FROM mcr.microsoft.com/dotnet/sdk:7.0

COPY ./ConnectorsApi /app/ConnectorsApi
COPY ./CommonCode /app/CommonCode
WORKDIR /app/ConnectorsApi

RUN dotnet restore
RUN dotnet build

ENV CRUSHCOURSE_CONNECTORSAPI_PORT 6001

ENV WORKSPACE_METADATA_SERVICE_DOMAIN DESKTOP-CLUTQOA
ENV WORKSPACE_METADATA_SERVICE_PORT 5000

EXPOSE 6001/tcp

ENTRYPOINT ["dotnet", "run"]