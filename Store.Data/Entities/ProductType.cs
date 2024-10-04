using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities
{
    
    public class ProductType :BaseEntity<int>
    {
        public string Name { get; set; }

        //   public int ProductTypeId { get; set; }


    }
}