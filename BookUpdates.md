
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

## Chapter 31 - ASP.NET Core MVC

Page 981, referencing the source code file (Typo)

The correct file name is: MVCSampleApp/Controllers/ViewsDemoController.cs (instead of ViewDemoController.cs)
