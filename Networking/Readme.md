# Readme - Code Samples for Chapter 23, Networking

This chapter contains the following code samples:

* HTTP
    * HttpServer (HTTP server using `WebListener`)
    * HttpClientSample (HTTP client using `HttpClient`)
    * WinAppHttpClient (HTTP client with UWP app where `HttpClient` supports HTTP 2.0) 
* Utilities
    * Utilities (using `Uri` and `IPAddress`)
* DnsLookup
    * DnsLookup (showing IP Addresses using `Dns`)
* TCP
    * HttpClientUsingTcp (using `TcpClient` to access a HTTP Server)
    * TcpServer (TCP server using `TcpListener` and a custom protocol)
    * TcpClientSample (TCP client using `TcpClient`)
    * WinAppTcpClient (UWP client application managing the custom protocol form TcpServer)
    * WPFAppTcpClient (WPF client application managing the custom protocol from TcpServer)
* UDP
    * UdpSender (UDP sender using `UdpClient`)
    * UdpReceiver (UDP receiver using `UdpClient`)
* Sockets
    * SocketServer (using `Socket` to create a server)
    * SocketClient (using `Socket` to create a client)


To build and run the .NET Core samples, please install one of these tools:

* Visual Studio 2017 Update 5 with the .NET Core workload
* Visual Studio for Mac
* Visual Studio Code

The WinAppTcpClient sample needs Windows 10 and Visual Studio 2017 with the Universal Windows Platform development workload. The other samples can be used on other platforms.
The WPFAppTcpClient needs a Windows system.

Please download and install the tools from [.NET Core downloads](https://www.microsoft.com/net/core).
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp7)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!