using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VueStore.Repository.Models;
using VueStore.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace VueStore.Repository
{
    public class ItemRepo : IItemRepo
    {
        private readonly IConfiguration _config;
        public ItemRepo(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            using (IDbConnection cnn = new System.Data.SqlClient.SqlConnection(LoadConnectionString()))
            {
                return await cnn.QueryAsync<Item>("Select * FROM Items");
            }
        }
        public async Task<Item> GetItemByID(int id)
        {
            using (IDbConnection cnn = new System.Data.SqlClient.SqlConnection(LoadConnectionString()))
            {
                //Would use stored procedures for everything for security but sqlite doesn't have them
                return await cnn.QueryFirstAsync<Item>("Select * FROM Items where Id =" + id + ";");
            }
        }
        public async Task<int> CreateItem(Item item)
        {
            using (IDbConnection cnn = new System.Data.SqlClient.SqlConnection(LoadConnectionString()))
            {
                var sql = "INSERT INTO Items (ItemName, Cost) Values (@ItemName, @Cost);";
                var output = cnn.ExecuteAsync(sql, new { ItemName = item.ItemName, Cost = item.Cost });
                return await output;
            }
        }

        public async Task<int> DeleteItem(int id)
        {
            using (IDbConnection cnn = new System.Data.SqlClient.SqlConnection(LoadConnectionString()))
            {
                var sql = "DELETE FROM Items WHERE Id = @Id;";
                var output = cnn.ExecuteAsync(sql, new { Id = id });
                return await output;
            }
        }


        public async Task<int> UpdateItem(Item item)
        {
            using (IDbConnection cnn = new System.Data.SqlClient.SqlConnection(LoadConnectionString()))
            {
                var sql = "UPDATE Items SET ItemName = @ItemName, Cost = @Cost WHERE Id = @Id;";
                var output = cnn.ExecuteAsync(sql, new { ItemName = item.ItemName, Cost = item.Cost, Id = item.Id });
                return await output;
            }
        }
        public async Task<IEnumerable<Item>> GetMaxPriceItems()
        {
            using (IDbConnection cnn = new System.Data.SqlClient.SqlConnection(LoadConnectionString()))
            {
                var output = cnn.QueryAsync<Item>("SELECT ItemName,MAX(Cost) FROM Items GROUP BY ItemName");
                return await output;
            }
        }
        public async Task<Item> GetMaxPriceItem(string itemName)
        {
            using (IDbConnection cnn = new System.Data.SqlClient.SqlConnection(LoadConnectionString()))
            {
                var sql = "Select ItemName,MAX(Cost) FROM Items WHERE ItemName = @ItemName GROUP BY ItemName";
                var output = cnn.QueryFirstAsync<Item>(sql, new { ItemName = itemName });
                return await output;
            }
        }
        private string LoadConnectionString(string key = "Azure")
        {
            return _config.GetConnectionString(key);
        }

    }
}
