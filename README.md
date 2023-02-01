# Dotnet Core6 api with Angular.


Core secure API with Identity user management and agular client.

# Docker commands for API

```
docker build -f CoreTemplate.App.Api/Dockerfile -t easytemplate .
```

```
docker run -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 -e ASPNETCORE_ENVIRONMENT=Development --name easytemplate easytemplate
```