# Fibonacci-Sequence-ST10262464

This repository contains two console applications:
1. **Fibonacci Sequence Generator**: Generates the Fibonacci sequence up to 233 and stores each Fibonacci number as a message in Azure Queue Storage.
2. **Message Processor**: Retrieves the Fibonacci numbers from Azure Queue Storage, writes them into a text file named `Shivon-Prawlall.txt`, and uploads the file to Azure File Storage.


## Prerequisites
Before running the applications, ensure that you have the following:
- .NET 6.0 SDK or later installed on your machine.
- An active Azure account with:
  - **Azure Queue Storage** installed.
  - **Azure File Storage** installed.
- Azure Storage connection string for both the Queue and File Storage.

## Running the applications
- dotnet build
- dotnet run




