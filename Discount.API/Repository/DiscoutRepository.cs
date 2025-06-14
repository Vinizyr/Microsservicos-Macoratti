﻿using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var connection = GetConectionPostgres();

            var affected = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) " +
                "VALUES (@ProductName, @Description, @Amount)", 
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affected == 0)
            {
                return false;
            }
            return true;

        }


        public async Task<bool> DeleteDiscount(string productName)
        {
            var connection = GetConectionPostgres();

            var affected = await connection.ExecuteAsync
                ("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            if (affected == 0)
            {
                return false;
            }

            return true;
        }
        public async Task<Coupon> GetDiscount(string productName)
        {
            var connection = GetConectionPostgres();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            if (coupon == null)
            {
                return new Coupon
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = "No Discount Desc"
                };
            }
            return coupon;

        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var connection = GetConectionPostgres();

            var affected = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount " +
                "WHERE Id = @Id", 
                new { Id = coupon.Id, ProductName = coupon.ProductName,
                    Description = coupon.Description, Amount = coupon.Amount });

            if (affected == 0)
                return false;

            return true;
        }
        private NpgsqlConnection GetConectionPostgres()
        {
            return new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }
    }
}
