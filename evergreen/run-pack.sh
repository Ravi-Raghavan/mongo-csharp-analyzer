#!/usr/bin/env bash
set -o errexit # Exit the script with error if any of the commands fail
set +o xtrace  # Disable tracing.

# Environment variables used as input:
# ARTIFACTORY_PASSWORD
# ARTIFACTORY_USERNAME
# AZURE_NUGET_SIGN_TENANT_ID
# AZURE_NUGET_SIGN_CLIENT_ID
# AZURE_NUGET_SIGN_CLIENT_SECRET
# PACKAGE_VERSION

if [ -z "$PACKAGE_VERSION" ]; then
  echo "PACKAGE_VERSION variable should be set"
  exit 1
fi

echo Creating nuget package...

dotnet clean "./MongoDB.Analyzer.sln"
dotnet build "./MongoDB.Analyzer.sln" -c Release -p:Version="$PACKAGE_VERSION"
dotnet pack ./src/MongoDB.Analyzer.Package/MongoDB.Analyzer.Package.csproj -o ./artifacts/nuget -c Release -p:Version="$PACKAGE_VERSION" -p:ContinuousIntegrationBuild=true

echo "${ARTIFACTORY_PASSWORD}" | docker login --password-stdin --username "${ARTIFACTORY_USERNAME}" artifactory.corp.mongodb.com

docker run --platform="linux/amd64" --rm -v $(pwd):/workdir -w /workdir \
  artifactory.corp.mongodb.com/release-tools-container-registry-local/azure-keyvault-nuget \
  NuGetKeyVaultSignTool sign "artifacts/nuget/*"."$PACKAGE_VERSION".nupkg \
    --force \
    --file-digest=sha256 \
    --timestamp-rfc3161=http://timestamp.digicert.com \
    --timestamp-digest=sha256 \
    --azure-key-vault-url=https://mdb-authenticode.vault.azure.net \
    --azure-key-vault-tenant-id="$AZURE_NUGET_SIGN_TENANT_ID" \
    --azure-key-vault-client-secret="$AZURE_NUGET_SIGN_CLIENT_SECRET" \
    --azure-key-vault-client-id="$AZURE_NUGET_SIGN_CLIENT_ID" \
    --azure-key-vault-certificate=authenticode-2021