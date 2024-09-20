using System;
using Azure.Storage.Queues; // Namespace for Queue storage

class FibonacciGenerator
{
    static void Main(string[] args)
    {
        // Azure Queue Connection string
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=cldvice3;AccountKey=3a+ppItcqYmKQWHYx27Pfrx4qwgleg8QggTUY9BaATgyuT+lK98oqQazmlEt1ju0r0FA2pfgFu/E+ASttNHO0g==;EndpointSuffix=core.windows.net";
        string queueName = "fibonacci-queue";

        // Initialize the queue client
        QueueClient queueClient = new QueueClient(connectionString, queueName);
        queueClient.CreateIfNotExists();

        // Generate Fibonacci sequence up to 233
        int a = 0, b = 1, sum = 0;

        // Store Fibonacci numbers in Azure Queue
        while (sum <= 233)
        {
            Console.WriteLine(sum); // Display on console
            queueClient.SendMessage(sum.ToString()); // Send the message to Azure Queue

            // Generate next Fibonacci number
            a = b;
            b = sum;
            sum = a + b;
        }

        Console.WriteLine("Fibonacci sequence sent to queue.");
    }
}
