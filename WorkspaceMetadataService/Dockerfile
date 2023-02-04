FROM mcr.microsoft.com/dotnet/sdk:7.0

COPY ./WorkspaceMetadataService /app/WorkspaceMetadataService
COPY ./CommonCode /app/CommonCode

WORKDIR /app/WorkspaceMetadataService
RUN dotnet restore
RUN dotnet build

ENV CRUSHCOURSE_WORKSPACEMETADATA_PORT 6000
EXPOSE 6000/tcp

ENTRYPOINT ["dotnet", "run"]