FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SagaGuide/SagaGuide.Api/SagaGuide.Api.csproj", "SagaGuide/SagaGuide.Api/"]
RUN dotnet restore "SagaGuide/SagaGuide.Api/SagaGuide.Api.csproj"
COPY . .
WORKDIR "/src/SagaGuide/SagaGuide.Api"
RUN dotnet build "SagaGuide.Api.csproj" -c Release -o /app/build

FROM build AS test
LABEL intermediate=true
ENV DOTNET_TEST_RESULTS=/test_results
RUN mkdir $DOTNET_TEST_RESULTS
LABEL dotnet_test_results=$DOTNET_TEST_RESULTS
RUN dotnet test "../SagaGuide.UnitTests/SagaGuide.UnitTests.csproj" --logger trx --results-directory $DOTNET_TEST_RESULTS
RUN dotnet test "../SagaGuide.IntegrationTests/SagaGuide.IntegrationTests.csproj" --logger trx --results-directory $DOTNET_TEST_RESULTS

FROM build AS publish
LABEL intermediate=true
RUN dotnet publish "SagaGuide.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#COPY  --from=gcs_library . /gcs_master_library/library
ENTRYPOINT ["dotnet", "SagaGuide.Api.dll"]