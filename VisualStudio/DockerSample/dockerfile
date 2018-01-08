FROM microsoft/aspnetcore
WORKDIR /app
COPY ./Publish .
RUN dir .
ENTRYPOINT [ "dotnet", "/app/DockerSample.dll" ]
RUN echo 'completed building image'
