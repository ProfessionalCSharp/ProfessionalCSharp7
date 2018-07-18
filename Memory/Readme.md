# Readme - Code Samples for Chapter 17, Managed and Unmanaged Memory

This chapter contains the following code samples:

* PointerPlayground (using pointers with C#, *unsafe* keyword)
* PointerPlayground2 (adding a struct)
* QuickArray (quick sorting using pointers, *stackalloc* keyword)
* ReferenceSemantics (ref return, ref local, ref readonly)
* SpanSample (the Span type)
* PlatformInvokeSample (invoking native methods from C#)

> The *PlatformInvokeSample* requires Windows because of the native method invoked, all other samples run on Windows and Linux.

> The library used with the Span type is currently in preview (System.Memory). In the sample code I'm using a newer version than available on the default GitHub feed. The nuget.config file defines the GitHub feed for the newer preview of System.Memory. You might need to update the reference.

To build and run the .NET Core samples, please install one of these tools:

* Visual Studio 2017 Update 7 with the .NET Core workload
* Visual Studio for Mac
* Visual Studio Code

Please download and install the tools from [.NET Core downloads](https://www.microsoft.com/net/core).
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp7)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!
