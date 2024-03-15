using DataTier.View.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Repository.Interface
{
    public interface IProductRepository
    {
        List<UserProductViewModel> GetAllProductUser(int id);
        List<ProductResponseModel> GetHotProduct();
        List<ImageViewModel> GetImageURL(int artworkid);
        ProductResponseModel GetProduct(int id);
    }
}
