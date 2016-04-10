using ServerSidePagingKoGrid.Models;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Http;

namespace ServerSidePagingKoGrid.Controllers
{
    public class ProductController : ApiController
    {
        private ServerSidePagingKoGridDbContext db = new ServerSidePagingKoGridDbContext();

        // GET: api/Product
        public IHttpActionResult GetProducts([FromUri]int page, [FromUri]int pageSize, [FromUri]string filter, [FromUri]string sort)
        {
            ProductListDto dto = new ProductListDto();

            IQueryable<Product> query = db.Products;

            if (filter != null)
            {
                if (filter.Contains(':'))
                {
                    string[] filterArray = filter.Split(':');
                    switch (filterArray[0].ToLower())
                    {
                        case "name":
                            query = query.Where(p => p.Name.Contains(filterArray[1].Trim()));
                            break;
                        case "description":
                            query = query.Where(p => p.Description.Contains(filterArray[1].Trim()));
                            break;
                    }
                }
                else
                {
                    query = query.Where(p => p.Name.Contains(filter.Trim()) || p.Description.Contains(filter.Trim()));
                }
            }

            dto.ProductCount = query.Count();

            query = query.OrderBy(sort == null ? "name asc" : sort).Skip((page - 1) * pageSize).Take(pageSize);

            dto.PageOfProducts = query.Select(p => new ProductDto()
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                ProductCategoryName = p.ProductCategory.Name
            }).ToList();

            return Ok(dto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
