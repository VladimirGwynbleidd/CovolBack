FROM nexus:8084/dotnet-21:2.1-29
ENV PATH=/opt/rh/rh-nodejs10/root/usr/bin:/opt/rh/rh-dotnet21/root/usr/bin:/opt/rh/rh-dotnet21/root/usr/sbin:/opt/app-root/.dotnet/tools:${PATH}
ENV ASPNETCORE_URLS="http://+:9080"

COPY --chown=1001:0 ./app /opt/app-root/app/
WORKDIR /opt/app-root/app
RUN ls -l
EXPOSE 9080

ENTRYPOINT ["dotnet", "WebApiCore.dll"]