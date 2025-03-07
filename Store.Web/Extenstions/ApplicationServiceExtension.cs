﻿using Microsoft.AspNetCore.Mvc;
using Store.Repository.Basket;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Services.HandleResponses;
using Store.Services.Services.BasketService;
using Store.Services.Services.BasketService.Dto;
using Store.Services.Services.CacheService;
using Store.Services.Services.OrderService;
using Store.Services.Services.OrderService.Dtos;
using Store.Services.Services.PaymentService;
using Store.Services.Services.ProductServices.Dto;
using Store.Services.Services.TokenService;
using Store.Services.Services.UserService;

namespace Store.Web.Extenstions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketService, BasketServie>();
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped<ITokenService , TokenService>();
            services.AddScoped<IUserService , UserService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketService, BasketServie>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));

            services.AddAutoMapper(typeof(OrderProfile));
           


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(model => model.Value?.Errors.Count > 0)
                    .SelectMany(model => model.Value?.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList();

                    var errorResponse = new ValidationErrorRespose
                    {
                        Errors = errors

                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
