﻿using ArtWorkWeb.Service.Interfaces;
using AutoMapper;
using DataTier.Repository.Interface;
using DataTier.View.Common;
using DataTier.View.Product;
using System.Collections.Generic;
using System.Net;

namespace ArtWorkWeb.Service.Implement
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _productRepository = repository;
            _mapper = mapper;
        }

        public KeyValuePair<MessageViewModel, List<UserProductViewModel>> GetAllProductUser(int id)
        {
            var response = _productRepository.GetAllProductUser(id);
            if (response == null)
            {
                return new KeyValuePair<MessageViewModel, List<UserProductViewModel>>(
                        new MessageViewModel
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "No product found"
                        },
                        new List<UserProductViewModel>()
                        );
            }
            return new KeyValuePair<MessageViewModel, List<UserProductViewModel>>(
                new MessageViewModel
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = string.Empty
                },
                response
                );
        }

        public KeyValuePair<MessageViewModel, List<ImageViewModel>> GetImage(int id)
        {
            var response = _productRepository.GetImageURL(id);
            if (response == null)
            {
                return new KeyValuePair<MessageViewModel, List<ImageViewModel>>(
                        new MessageViewModel
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "No product found"
                        },
                        new List<ImageViewModel>()
                        );
            }
            return new KeyValuePair<MessageViewModel, List<ImageViewModel>>(
                new MessageViewModel
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = string.Empty
                },
                response
                );
        }

        public KeyValuePair<MessageViewModel, List<ProductResponseModel>> GetHotProduct()
        {
            var response = _productRepository.GetHotProduct();
            if (response == null)
            {
                return new KeyValuePair<MessageViewModel, List<ProductResponseModel>>(
                        new MessageViewModel
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "No product found"
                        },
                        null
                        );
            }
            return new KeyValuePair<MessageViewModel, List<ProductResponseModel>>(
                new MessageViewModel
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = string.Empty
                },
                response
                );
        }

        public KeyValuePair<MessageViewModel, ProductResponseModel> GetProduct(int id)
        {
            var response = _productRepository.GetProduct(id);
            if (response == null)
            {
                return new KeyValuePair<MessageViewModel, ProductResponseModel>(
                        new MessageViewModel
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "No product found"
                        },
                        null
                        );
            }
            return new KeyValuePair<MessageViewModel, ProductResponseModel>(
                new MessageViewModel
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = string.Empty
                },
                response
                );
        }

        public KeyValuePair<MessageViewModel, ProductResponseModel> UpdateProduct(int id, ProductResponseModel productDto)
        {
            var updatedProduct = _productRepository.Update(id, productDto);

            if (updatedProduct == null)
            {
                return new KeyValuePair<MessageViewModel, ProductResponseModel>(
                    new MessageViewModel { StatusCode = HttpStatusCode.NotFound, Message = "Product not found" }, null);
            }

            return new KeyValuePair<MessageViewModel, ProductResponseModel>(
                new MessageViewModel { StatusCode = HttpStatusCode.OK, Message = "Product updated successfully" }, updatedProduct);
        }


    }
}
