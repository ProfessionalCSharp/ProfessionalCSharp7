# Readme - Code Samples for Bonus Chapter 3, SignalR and WebHooks

This chapter contains these samples:

* WebSockets (Console app client and ASP.NET Core server)
* SignalR (ASP.NET Core App, HTML client, Windows App client)
* WebHooksReceiver (with Dropbox and GitHub)

The WebHooks packages for .NET Core are early versions available on NuGet, this is not part of .NET Core 2.1.

For WebHooks you need to configure the myget server to get the early packages:

* [.NET Core MyGet](https://dotnet.myget.org/F/dotnet-core/api/v3/index.json)
* [ASP.NET Core MyGet](https://dotnet.myget.org/F/aspnetcore-dev/api/v3/index.json)

For the WebHooksReceiver project, you also need to configure a WebHook with GitHub and with Dropbox. The sample writes the result to an Azure Storage queue. Create a Azure Storage account in your portal, and configure the project accordingly. More information how to - read the book chapter.
