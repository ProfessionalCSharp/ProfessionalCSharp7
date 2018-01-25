# Readme - Code Samples for Chapter 22, Files and Streams

This chapter contains the following code samples:

* DriveInformation (using `DriveInfo`)
* Working with Files and Folders (accessing files and directories using `File`, `Directory`, and `Path`)
* Stream Samples (`FileStream`, `StreamReader`, `StreamWriter`, `Encoding`)
* Reader Writer Samples (reading and writing binary and text files)
* Compress File Sample (compressing and uncompressing using `DeflateStream`, `ZipArchive`, and `BrotliStream`)
* File Monitor (monitoring file changes with `FileSystemWatcher`)
* Memory Mapped Files (using shared memory)
* Named Pipes (PipesReader, PipesWriter - sharing data between processes using named pipes)
* Anonymous Pipes (sharing data with anonymous pipes)
* WindowsAppEditor (an editor using UWP)

To build and run the .NET Core samples, please install one of these tools:

* Visual Studio 2017 Update 5 with the .NET Core workload
* Visual Studio for Mac
* Visual Studio Code

The WindowsAppEditor sample needs Windows 10 and Visual Studio 2017 with the Universal Windows Platform development workload. The other samples can be used on other platforms.

The Brotli algorithm (Compress File Sample) currently needs these NuGet sources:

[.NET Core NuGet](https://dotnet.myget.org/F/dotnet-core/api/v3/index.json) - for System.Memory
[CoreFx Lab](https://dotnet.myget.org/F/dotnet-corefxlab/) - for System.IO.Compression.Brotli

Please download and install the tools from [.NET Core downloads](https://www.microsoft.com/net/core).
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp7)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!