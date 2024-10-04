using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Services.Services.ProductServices.Dto;
using ProductEntity = Store.Data.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.Repository.Specification.ProductSpecs;
using Store.Services.Helper;

namespace Store.Services.Services.ProductServices.Dto
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsyns()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsNoTraAsync();
            //IReadOnlyList<BrandTypeDetailsDto> mappedBrands = brands.Select(x => new BrandTypeDetailsDto 
            //    {
            //    Id= x.Id,
            //    Name= x.Name,
            //    CreateAt=x.CreateAt
            //}).ToList();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return mappedBrands;

        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductAsync(ProductSpecification input )
        {
            var specs=new ProductWithSpecifications(input);
            var products=await _unitOfWork.Repository<ProductEntity, int>().GetAllWithSpecificationAsync(specs);
            #region MyRegion
            //var mappedProduts = products.Select(x => new ProductDetailsDto
            //{
            //    Id= x.Id,
            //    Name= x.Name,
            //    Description= x.Description,
            //    PictureUrl= x.PictureUrl,
            //    Price= x.Price,
            //    CreateAt= x.CreateAt,
            //    BrandName=x.Brand.Name,
            //    TypeName=x.Type.Name


            //}
            //).ToList(); 
            #endregion
            var countSpecs = new ProductWithCountSpecification(input);
            var count = await _unitOfWork.Repository<ProductEntity, int>().GetCountSpecificationAsync(countSpecs);
            var mappedProduts=_mapper.Map<IReadOnlyList< ProductDetailsDto >>(products);
            return new PaginatedResultDto<ProductDetailsDto>(input.PageIndex, input.PageSize, count, mappedProduts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {

            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsNoTraAsync();

            #region MyRegion
            //IReadOnlyList<BrandTypeDetailsDto> mappedTypes = types.Select(x => new BrandTypeDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CreateAt = x.CreateAt
            //}).ToList(); 
            #endregion
            var mappedTypes= _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
            return mappedTypes;

        }

        public async Task<ProductDetailsDto> GetProductByIdsAsync(int? productId)
        {
            if (productId is null)
                throw new Exception("Id is Null");
            var specs = new ProductWithSpecifications(productId);
            var product = await _unitOfWork.Repository<ProductEntity, int>().GetWithSpecificationByIdAsync(specs);
            if (product is null)
                throw new Exception("Product Not Found");

            var mappedProduct=_mapper.Map<ProductDetailsDto>(product);
            //= new ProductDetailsDto
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    CreateAt = product.CreateAt,
            //    BrandName = product.Brand.Name,
            //    TypeName = product.Type.Name

            //};
            return mappedProduct;

        }
    }
}
