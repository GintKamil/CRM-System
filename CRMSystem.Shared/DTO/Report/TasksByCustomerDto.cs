using CRMSystem.Shared.DTO.Api;

namespace CRMSystem.Shared.DTO.Report
{
    public class TasksByCustomerDto
    {
        public CustomerApiDto Customer { get; set; }
        public int Count { get; set; }
        public TasksByCustomerDto(CustomerApiDto customer, int count) 
        {
            Customer = customer;
            Count = count;
        }
    }
}
