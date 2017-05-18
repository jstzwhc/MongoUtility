using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoUtility
{
    /// <summary>
    /// MongoDB操作接口
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public interface IMongOperation<T> where T : class, new()
    {
        /// <summary>
        /// 获取单例
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        T Get(string ID);
        /// <summary>
        /// 异步获取单例
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        Task<T> GetAsync(string ID);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="startRowIndex">首记录索引</param>
        /// <param name="maximumRows">最大项数</param>
        /// <returns></returns>
        IList<T> Get(int startRowIndex, int maximumRows);
        /// <summary>
        /// 异步获取列表
        /// </summary>
        /// <param name="startRowIndex">首记录索引</param>
        /// <param name="maximumRows">最大项数</param>
        /// <returns></returns>
        Task<IList<T>> GetAsync(int startRowIndex, int maximumRows);
        /// <summary>
        /// 按条件获取列表
        /// </summary>
        /// <param name="condition">检索条件</param>
        /// <param name="startRowIndex">首记录索引</param>
        /// <param name="maximumRows">最大项数</param>
        /// <returns></returns>
        IList<T> Get(string condition, int startRowIndex, int maximumRows);
        /// <summary>
        /// 异步按条件获取列表
        /// </summary>
        /// <param name="condition">检索条件</param>
        /// <param name="startRowIndex">首记录索引</param>
        /// <param name="maximumRows">最大项数</param>
        /// <returns></returns>
        Task<IList<T>> GetAsync(string condition, int startRowIndex, int maximumRows);
        /// <summary>
        /// 按条件获取列表
        /// </summary>
        /// <param name="curPage">当前页号</param>
        /// <param name="recordsPerPage">每页记录数</param>
        /// <param name="condition">检索条件</param>
        /// <param name="sortBy">排序字段</param>
        /// <param name="dir">排序方式</param>
        /// <returns></returns>
        IList<T> Get(int curPage, int recordsPerPage, string condition, string sortBy, string dir);
        /// <summary>
        /// 异步按条件获取列表
        /// </summary>
        /// <param name="curPage">当前页号</param>
        /// <param name="recordsPerPage">每页记录数</param>
        /// <param name="condition">检索条件</param>
        /// <param name="sortBy">排序字段</param>
        /// <param name="dir">排序方式</param>
        /// <returns></returns>
        Task<IList<T>> GetAsync(int curPage, int recordsPerPage, string condition, string sortBy, string dir);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Entity">实体</param>
        void Insert(T Entity);
        /// <summary>
        /// 异步添加
        /// </summary>
        /// <param name="Entity">实体</param>
        Task InsertAsync(T Entity);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        void Update(T Entity);
        /// <summary>
        /// 异步修改
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        Task UpdateAsync(T Entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        void Delete(T Entity);
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        Task DeleteAsync(T Entity);
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        void Erase(T Entity);
        /// <summary>
        /// 异步物理删除
        /// </summary>
        /// <param name="Entity">实体</param>
        /// <returns></returns>
        Task EraseAsync(T Entity);
        /// <summary>
        /// 统计记录数量
        /// </summary>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// 按条件统计记录数量
        /// </summary>
        /// <param name="condition">检索条件</param>
        /// <returns></returns>
        int Count(string condition);
    }
}
