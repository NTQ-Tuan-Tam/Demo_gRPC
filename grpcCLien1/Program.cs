using Grpc.Core;
using Grpc.Net.Client;

using GrpcServiceDemo;
using GrpcServiceDemo.Protos;
using System;
using System.Threading.Tasks;

namespace grpcCLien1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "tuan"};
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var customerClient = new Customer.CustomerClient(channel);


            var clientRequest = new CustomerLookupModel { UserId = 2 };
            var customer = await customerClient.GetCustomerInfoAsync(clientRequest);
            Console.WriteLine($"{customer.FistName}{customer.LastName}");
            Console.WriteLine("New Customer List");
            Console.WriteLine();

            using (var call = customerClient.GetNewCustomers(new NewCustomerRetquest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{ currentCustomer.FistName}{currentCustomer.LastName} : {currentCustomer.EmailAddrees}");
                }
            }
                Console.ReadLine();
        }
    }
}
