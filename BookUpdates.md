
# Book Updates - Professional C# 7 and .NET Core 2.0

This document contains update information what has been changed after the book Professional C# 7 and .NET Core 2.0 was published, as well as typos.

## Chapter 1 - .NET Applications and Tools

Page 5 - Update for LTS and Current versions of .NET Core to include 2.1, see this blog article [.NET Core Current and Long Time Support Versions](https://csharp.christiannagel.com/2018/06/26/ltsandcurrent/).

## Chapter 8 - Delegates, Lambdas, and Events

Page 233, last paragraph, text should be:

**Inside the method `NewCar`, the event `NewCarInfo` is fired.** The implementation of this method verifies whether the delegate is not null and raises the event.

## Chapter 23 - Networking

### Http Client Factory - Update with .NET Core 2.1

.NET Core 2.1 adds a Http Client Factory which allows easy management and caching of HttpClient instances. See the article [HttpClient Factory with .NET Core 2.1](https://csharp.christiannagel.com/2018/06/05/httpclient/) for more information and a code sample.

## Chapter 27 - Localization

### Sample App UWPCultureDemo

When the book was released, Windows 10 Fall Creators Update (build 16299) was used to build the sample. The sample app uses a TreeView control available from a Microsoft sample. Windows 10 April 2018 Update (version 1803, build 17134) includes a TreeView control. The sample app was updated to use this new TreeView control.

See the article [TreeView Control with Windows Apps](https://csharp.christiannagel.com/2018/05/05/treeview/) for more information.

## Chapter 30 - ASP.NET Core

> Updates for .NET Core 2.1

Page 928, the package for ASP.NET Core changed from *Microsoft.AspNetCore.All* to *Microsoft.AspNetCore.App*. This new package does not include all the packages from *All*, e.g. application insights is missing. You can add additional packages as needed. *Microsoft.AspNetCore.All* will be obsolete with .NET Core 3.0.

New *WebSampleApp.csproj*:

```JSON
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>

  <ItemGroup>
    <Folder Include="wwwroot\" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.Web.LibraryManager.Build" Version="1.0.113" />
  </ItemGroup>

</Project>
```

> The package *Microsoft.Web.LibraryManager.Build* is added to restore client-side packages using **Library Manager** (see below).

Page 930, with ASP.NET Core 2.1, the startup code changed, but this is just a different code style.

New *Program.cs*:

```csharp
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(configure =>
                {
                    configure.AddXmlFile("appsettings.xml", optional: true);
                });
    }
```

### Using Client Side Libraries with Library Manager

Page 936 describes using client side libraries with *Bower*. Bower packages have been included by default with ASP.NET Core project templates. With new versions, this is no longer the case. The book also introduces *NPM* and *WebPack*. Visual Studio 2017 Update 8 also includes a new feature - the **Library Manager**. The *Library Manager* is a simple alternative to get files from CDN servers when JavaScript build environments are not needed.

See this blog article [Bower or Library Manager?](https://csharp.christiannagel.com/2018/06/13/librarymanager/) for more information.

The new configuration to include Bootstrap and jQuery in the WebSampleApp:

```json
{
  "version": "1.0",
  "defaultProvider": "cdnjs",
  "libraries": [
    {
      "library": "twitter-bootstrap@4.1.3",
      "destination": "wwwroot/lib/bootstrap/"
    },
    {
      "library": "jquery@3.3.1",
      "destination": "wwwroot/lib/jquery/",
      "files": ["jquery.js", "jquery.min.js"]
    }
  ]
}
```

### Packing with webpack

The ASP.NET Core project template for Angular no longer includes the integration with Webpack. Instead, the template integrates the Angular CLI. With the startup code, instead of the namespace *Microsoft.AspNetCore.SpaServices.Webpack*, now the namespace *Microsoft.AspNetCore.SpaServices.AngularCli* is used.

## Chapter 31 - ASP.NET Core MVC

Page 973, in the first paragraph - the base class is `RazorPage`, as also explained in page 974.

Page 981, referencing the source code file (Typo)

The correct file name is: MVCSampleApp/Controllers/ViewsDemoController.cs (instead of ViewDemoController.cs)

Page 1007 and later, using ASP.NET Core 2.1 the AccountController and views are no longer created with the project. Instead, a UI library is used. Read [ASP.NET Core Identity Pages with ASP.NET Core 2.1](https://csharp.christiannagel.com/2018/07/18/identitypages/) for more information.
