using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDbDemoApi1.MongodbHepler
{
    public interface IMongoDbBase
    {
        #region 查询
        /// <summary>
        /// 根据条件查询,获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetIEnumerableAsync<T>(Expression<Func<T, bool>> conditions = null);

        #endregion

        #region 插入
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienumerable"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> InsertManyAsync<T>(IEnumerable<T> ienumerable);

        #endregion
    }
}
