using ClassLibraryLabb4;
using System.ComponentModel.DataAnnotations;

namespace Labb4_Projekt.Mappers
{
    public class CustomerMapper
    {
        public static CustomerDTO MapToDTO(Customer customer)
        {
            return new CustomerDTO
            {
                Name = customer.CustomerName,
                Email = customer.CustomerEmail,
                
            };
        }
    }
}
