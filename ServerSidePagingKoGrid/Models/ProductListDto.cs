using System.Collections.Generic;

namespace ServerSidePagingKoGrid.Models
{
    public class ProductListDto
    {
        public int ProductCount { get; set; }
        public List<ProductDto> PageOfProducts { get; set; }
    }
}