# Readme - Code Samples for Chapter 20, Dependency Injection

This chapter contains the following code samples:

* NoDI (without dependency injection)
* WithDI (with dependency injection)
* WithDIContainer (with the DI container Microsoft.Extensions.DependencyInjection)
* ServicesLifetime (creating scoped services, disposing of services)
* DIWithOptions (using the IOptions interface for instantiating services)
* DIWithConfiguration (using configuration files to configure the services)
* DIWithAutofac (using the Autofac container adapter for Microsoft.Extensions.DependencyInjection)

* PlatformIndependenceSample 
    * DISampleLib (.NET STandard Library, shared between WPF, UWP, Xamarin)
    * UWPClient (UWP Client using the dependency injection container)
    * WPFClient (WPF Client using the dependency injection container)
    * XamarinClient (shared project for Android/iPhone/UWP)
    * XamarinClient.Android (Android client)
    * XamarinClient.IoS (IoS Client)
    * XamarinClient.UWP (UWP Client)

With the PlatformIndependenceSample, for the IoS project a Mac is needed for compilation. The WPF and UWP clients need a Windows system. You need to install the *Mobile Development with .NET* workload with the Visual Studio Installer for this project.

To build and run the .NET Core samples, please install one of these tools:

* Visual Studio 2017 Update 5 with the .NET Core workload
* Visual Studio for Mac
* Visual Studio Code

Please download and install the tools from [.NET Core downloads](https://www.microsoft.com/net/core).
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp7)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!