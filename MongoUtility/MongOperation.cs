using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MongoUtility
{
    /// <summary>
    /// MongoDB操作实现
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class MongOperation<T> : IMongOperation<T> where T : class, new()
    {
        private readonly static IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        private readonly static IConfigurationRoot Configuration = builder.Build();
        private readonly static MongoClient client = new MongoClient(Configuration["MongoDB"]);
        private readonly static IMongoDatabase database = client.GetDatabase(Configuration["DataBase"]);
        private readonly static T obj = Activator.CreateInstance<T>();
        private readonly static Type type = obj.GetType();
        private readonly IMongoCollection<T> collection = database.GetCollection<T>(type.Name.Substring(0, type.Name.Length - 4).ToLower());
        /// <summary>
        /// 获取单例
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        public T Get(string ID)
        {
            FilterDefinition<T> filter = "{\"_id\": ObjectId(\"" + ID + "\")}";
            T cli = collection.Find(filter).SingleOrDefault();
            return cli;
        }
        /// <summary>
        /// 异步获取单例
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        public async Task<T> GetAsync(string ID)
        {
            FilterDefinition<T> filter = "{\"_id\": ObjectId(\"" + ID + "\")}";
            T cli = await collection.Find(filter).SingleOrDefaultAsync();
            return cli;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="startRowIndex">首记录索引</param>
        /// <param name="maximumRows">最大项数</param>
        /// <returns></returns>
        public IList<T> Get(int startRowIndex, int maximumRows)
        {
            FilterDefinition<T> filter = "{\"IsDeleted\": false}";
            SortDefinition<T> sort = "{\"_id\": -1}";
            List<T> li = collection.Find(filter).Sort(sort).Skip(startRowIndex).Limit(maximumRows).ToList();
            return li;
        }
        /// <summary>
        /// 异步获取列表
        /// </summary>
        /// <param name="startRowIndex">首记录索引</param>
        /// <param name="maximumRows">最大项数</param>
        /// <returns></returns>
        public async Task<IList<T>> GetAsync(int startRowIndex, int maximumRows)
        {
            FilterDefinition<T> filter = "{\"IsDeleted\": false}";
            SortDefinition<T> sort = "{\"_id\": -1}";
            List<T> li = await collection.Find(filter).Sort(sort).Skip(startRowIndex).Limit(maximumRows).ToListAsync();
            return li;
        }
        /// <summary>
        /// 按条件获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="startRowIndex">首记录索引</param>
        /// <param name="maximumRows">最大项数</param>
        /// <returns></returns>
        public IList<T> Get(string condition, int startRowIndex, int maximumRows)
        {
            SortDefinition<T> sort = "{\"_id\": -1}";
            FilterDefinition<T> filter = condition;
            List<T> li = collection.Find(filter).Sort(sort).Skip(startRowIndex).Limit(maximumRows).ToList();
            return li;
        }
        /// <summary>
        /// 异步按条件获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="startRowIndex">首记录索引</param>
        /// <param name="maximumRows">最大项数</param>
        /// <returns></returns>
        public async Task<IList<T>> GetAsync(string condition, int startRowIndex, int maximumRows)
        {
            SortDefinition<T> sort = "{\"_id\": -1}";
            FilterDefinition<T> filter = condition;
            List<T> li = await collection.Find(filter).Sort(sort).Skip(startRowIndex).Limit(maximumRows).ToListAsync();
            return li;
        }
        /// <summary>
        /// 按条件获取列表
        /// </summary>
        /// <param name="curPage">当前页号</param>
        /// <param name="recordsPerPage">每页记录数</param>
        /// <param name="condition">检索条件</param>
        /// <param name="sortBy">排序字段</param>
        /// <param name="dir">排序方式</param>
        /// <returns></returns>
        public IList<T> Get(int curPage, int recordsPerPage, string condition, string sortBy, string dir)
        {
            string fields = string.Empty;
            int startRowIndex = 0, maximumRows = recordsPerPage;
            if (curPage == 0)
                startRowIndex = 0;
            else
                startRowIndex = (curPage - 1) * recordsPerPage;
            SortDefinition<T> sort;
            if (dir == "asc")
                sort = "{\"" + sortBy + "\": 1}";
            else
                sort = "{\"" + sortBy + "\": -1}";
            FilterDefinition<T> filter = condition;
            List<T> li = collection.Find(filter).Sort(sort).Skip(startRowIndex).Limit(maximumRows).ToList();
            return li;
        }
        /// <summary>
        /// 异步按条件获取列表
        /// </summary>
        /// <param name="curPage">当前页号</param>
        /// <param name="recordsPerPage">每页记录数</param>
        /// <param name="condition">检索条件</param>
        /// <param name="sortBy">排序字段</param>
        /// <param name="dir">排序方式</param>
        /// <returns></returns>
        public async Task<IList<T>> GetAsync(int curPage, int recordsPerPage, string condition, string sortBy, string dir)
        {
            string fields = string.Empty;
            int startRowIndex = 0, maximumRows = recordsPerPage;
            if (curPage == 0)
                startRowIndex = 0;
            else
                startRowIndex = (curPage - 1) * recordsPerPage;
            SortDefinition<T> sort;
            if (dir == "asc")
                sort = "{\"" + sortBy + "\": 1}";
            else
                sort = "{\"" + sortBy + "\": -1}";
            FilterDefinition<T> filter = condition;
            List<T> li = await collection.Find(filter).Sort(sort).Skip(startRowIndex).Limit(maximumRows).ToListAsync();
            return li;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        public void Insert(T Entity)
        {
            collection.InsertOne(Entity);
        }
        /// <summary>
        /// 异步添加
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        public async Task InsertAsync(T Entity)
        {
            await collection.InsertOneAsync(Entity);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        public void Update(T Entity)
        {
            System.Reflection.PropertyInfo[] pis = type.GetProperties();
            FilterDefinition<T> filter = "{\"_id\": ObjectId(\"" + pis[0].GetValue(Entity, null).ToString() + "\")}";
            var builder = Builders<T>.Update;
            UpdateDefinition<T> update = Builders<T>.Update.Set(pis[1].Name, pis[1].GetValue(Entity, null));
            if (pis.Length >= 2)
            {
                for (int i = 2; i < pis.Length; i++)
                {
                    update = builder.Combine(update, builder.Set(pis[i].Name, pis[i].GetValue(Entity, null)));
                }
            }
            collection.UpdateOne(filter, update);
        }
        /// <summary>
        /// 异步修改
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        public async Task UpdateAsync(T Entity)
        {
            System.Reflection.PropertyInfo[] pis = type.GetProperties();
            FilterDefinition<T> filter = "{\"_id\": ObjectId(\"" + pis[0].GetValue(Entity, null).ToString() + "\")}";
            var builder = Builders<T>.Update;
            UpdateDefinition<T> update = Builders<T>.Update.Set(pis[1].Name, pis[1].GetValue(Entity, null));
            if (pis.Length >= 2)
            {
                for (int i = 2; i < pis.Length; i++)
                {
                    update = builder.Combine(update, builder.Set(pis[i].Name, pis[i].GetValue(Entity, null)));
                }
            }
            await collection.UpdateOneAsync(filter, update);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        public void Delete(T Entity)
        {
            System.Reflection.PropertyInfo[] pis = type.GetProperties();
            FilterDefinition<T> filter = "{\"_id\": ObjectId(\"" + pis[0].GetValue(Entity, null).ToString() + "\")}";
            UpdateDefinition<T> update = Builders<T>.Update.Set("IsDeleted", true);
            collection.UpdateOne(filter, update);
        }
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        public async Task DeleteAsync(T Entity)
        {
            System.Reflection.PropertyInfo[] pis = type.GetProperties();
            FilterDefinition<T> filter = "{\"_id\": ObjectId(\"" + pis[0].GetValue(Entity, null).ToString() + "\")}";
            UpdateDefinition<T> update = Builders<T>.Update.Set("IsDeleted", true);
            await collection.UpdateOneAsync(filter, update);
        }
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        public void Erase(T Entity)
        {
            System.Reflection.PropertyInfo[] pis = type.GetProperties();
            FilterDefinition<T> filter = "{\"_id\": ObjectId(\"" + pis[0].GetValue(Entity, null).ToString() + "\")}";
            collection.DeleteOne(filter);
        }
        /// <summary>
        /// 异步物理删除
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        public async Task EraseAsync(T Entity)
        {
            System.Reflection.PropertyInfo[] pis = type.GetProperties();
            FilterDefinition<T> filter = "{\"_id\": ObjectId(\"" + pis[0].GetValue(Entity, null).ToString() + "\")}";
            await collection.DeleteOneAsync(filter);
        }
        /// <summary>
        /// 统计记录数量
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            FilterDefinition<T> filter = "{}";
            int counter = unchecked((int)collection.Count(filter));
            return counter;
        }
        /// <summary>
        /// 按条件统计记录数量
        /// </summary>
        /// <param name="condition">检索条件</param>
        /// <returns></returns>
        public int Count(string condition)
        {
            FilterDefinition<T> filter = condition;
            int counter = unchecked((int)collection.Count(condition));
            return counter;
        }
    }
}
