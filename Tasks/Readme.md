# Readme - Code Samples for Chapter 21, Tasks and Parallel Programming

This chapter contains the following code samples:

* Parallel Samples
    * ParallelSamples (using the `Parallel` class)
    * TaskSamples (using the `Task` class, task hierarchies)
    * ValueTaskSample (`ValueTask`)
    * CancellationSamples (`CancellationToken`)
    * SimpleDataFlowSample (DataFlow)
    * DataFlowSample (a pipeline using multiple blocks)
    * TimersSample (`Timer`)s
    * WinAppTimer (Windows App - `DispatcherTimer`)
* Synchronization Samples
    * ThradingIssues (race condition and deadlocks)
    * SynchronizationSamples (`lock`, `Interlocked`, `Monitor`)
    * SingletonUsingMutex (`Mutex`)
    * SemaphoreSample (`SemaphoreSlim`)
    * EventSample (`ManualResetEventSlim`)
    * EventSampleWithCountdownEvent (`CountdownEvent`)
    * BarrierSample (`Barrier`)
    * ReaderWriterLockSample (`ReaderWriterLockSlim`)
    * LockAcrossAwait (using locks with multiple threads - with `SemaphoreSlim`)

To build and run the .NET Core samples, please install one of these tools:

* Visual Studio 2017 Update 5 with the .NET Core workload
* Visual Studio for Mac
* Visual Studio Code

The WindowsAppTimer sample needs Windows 10 and Visual Studio 2017 with the Universal Windows Platform development workload. The other samples can be used on other platforms.

Please download and install the tools from [.NET Core downloads](https://www.microsoft.com/net/core).
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp7)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!