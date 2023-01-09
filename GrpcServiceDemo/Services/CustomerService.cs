using Grpc.Core;
using GrpcServiceDemo.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcServiceDemo.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            this._logger = logger;
        }
        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel ouput = new CustomerModel();
            if (request.UserId == 1 )
            {
                ouput.FistName = "tuan";
                ouput.LastName = "tam ";
            }else if (request.UserId == 2 )
            {
                ouput.FistName = "tuan 2 ";
                ouput.LastName = "tam 2 ";
            }
            else
            {
                ouput.FistName = "fane";
                ouput.LastName = "thomas";
            }
            return Task.FromResult(ouput);
        }
        public override async Task GetNewCustomers(NewCustomerRetquest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FistName = "tim",
                    LastName = "cat",
                    EmailAddrees = " cattim@gmail.com",
                    Age = 12,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FistName = "chim",
                    LastName = "co1",
                    EmailAddrees = " cattim@gmail.com",
                    Age = 12,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FistName = "tim3",
                    LastName = "cat5",
                    EmailAddrees = " cattim@gmail.com",
                    Age = 12,
                    IsAlive = true
                },
            };
            foreach (var cust in customers)
            {
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
