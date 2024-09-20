using System;
using System.IO;
using Azure.Storage.Queues;
using Azure.Storage.Files.Shares; // Namespace for File storage

class FibonacciMessageProcessor
{
    static void Main(string[] args)
    {
        // Azure Queue and File storage connection strings
        string queueConnectionString = "DefaultEndpointsProtocol=https;AccountName=cldvice3;AccountKey=3a+ppItcqYmKQWHYx27Pfrx4qwgleg8QggTUY9BaATgyuT+lK98oqQazmlEt1ju0r0FA2pfgFu/E+ASttNHO0g==;EndpointSuffix=core.windows.net";
        string queueName = "fibonacci-queue";
        string fileConnectionString = "DefaultEndpointsProtocol=https;AccountName=cldvice3;AccountKey=3a+ppItcqYmKQWHYx27Pfrx4qwgleg8QggTUY9BaATgyuT+lK98oqQazmlEt1ju0r0FA2pfgFu/E+ASttNHO0g==;EndpointSuffix=core.windows.net";
        string shareName = "fibonacci-share";
        string fileName = "Shivon-Prawlall.txt";

        // Initialize the queue client
        QueueClient queueClient = new QueueClient(queueConnectionString, queueName);
        queueClient.CreateIfNotExists();

        // Create a file to store the Fibonacci sequence
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var message in queueClient.ReceiveMessages(maxMessages: 32).Value)
            {
                // Write each message (Fibonacci number) to the file
                writer.WriteLine(message.MessageText);
                
                // Delete message from queue
                queueClient.DeleteMessage(message.MessageId, message.PopReceipt);
            }
        }

        // Upload the file to Azure File Storage
        ShareClient shareClient = new ShareClient(fileConnectionString, shareName);
        shareClient.CreateIfNotExists();
        ShareDirectoryClient directoryClient = shareClient.GetRootDirectoryClient();
        ShareFileClient fileClient = directoryClient.GetFileClient(fileName);

        using (FileStream fs = File.OpenRead(fileName))
        {
            fileClient.Create(fs.Length);
            fileClient.Upload(fs);
        }

        Console.WriteLine($"{fileName} has been saved to Azure File Storage.");
    }
}
