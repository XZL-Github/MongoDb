using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDbDemoApi1.MongodbHepler
{
    public class MongoDBBase: IMongoDbBase
    {
        private readonly IMongoDatabase _mongoDatabase;
        public MongoDBBase(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetIEnumerableAsync<T>(Expression<Func<T, bool>> conditions = null)
        {
            var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
            if (collection!=null)
            {
                var result= await collection.FindAsync(conditions);
                return result.ToEnumerable();
            }
            var result2 = await collection.FindAsync(_ => true);
            return result2.ToEnumerable();
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienumerable"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> InsertManyAsync<T>(IEnumerable<T> ienumerable)
        {
            var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
            await collection.InsertManyAsync(ienumerable);
            return ienumerable;
        }
    }
}
