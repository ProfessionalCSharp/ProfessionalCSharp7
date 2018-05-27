# Readme - Code Samples for Bonus Chapter 3, SignalR and WebHooks

This chapter contains these samples:

* WebSockets (Console app client and ASP.NET Core server)
* SignalR (ASP.NET Core App, HTML client, Windows App client)
* WebHooksReceiver (with Dropbox and GitHub)

The SignalR sample has been updated to the release candidate. Update to the release of .NET Core 2.1 will be done at a later stage.

The .NET Core version of WebHooks will not be part of .NET Core 2.1, but will be released at a later time. The sample code makes use of source code from GitHub. To build and run the sample, create the WebHooks packages from the source code:

1. Clone the WebHoooks packages from [WebHooks](https://github.com/aspnet/WebHooks)

git clone https://github.com/aspnet/WebHooks

2. Look for the projects Microsoft.AspNetCore.WebHooks.Receivers, Microsoft.AspNetCore.WebHooks.Receivers.Dropbox, Microsoft.AspNetCore.WebHooks.Receivers.GitHub. Compile the libraries and create packages:

dotnet build
dotnet pack

3. Put the packages in a local folder and reference these packages from the WebHooksReceiver project

For the WebHooksReceiver project, you also need to configure a WebHook with GitHub and with Dropbox. The sample writes the result to an Azure Storage queue. Create a Azure Storage account in your portal, and configure the project accordingly. More information how to - read the book chapter.
