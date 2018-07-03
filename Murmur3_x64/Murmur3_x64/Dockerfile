## Muti-stage dockerfile to optize image size (Image size 219 MB)
FROM microsoft/dotnet:2.0-sdk AS build
COPY . ./Murmur3_x64
WORKDIR /Murmur3_x64/
RUN dotnet build -c Release -o output

FROM microsoft/dotnet:2.0-runtime AS runtime
COPY --from=build /Murmur3_x64/output .
ENTRYPOINT ["dotnet", "Murmur3_x64.dll"]


## Standard docker file (Image size 2GB)
FROM microsoft/dotnet:2.0-sdk
COPY . ./Murmur3_x64
WORKDIR /Murmur3_x64/
RUN dotnet build -c Release
ENTRYPOINT ["dotnet", "run", "-c", "Release", "--no-build"]