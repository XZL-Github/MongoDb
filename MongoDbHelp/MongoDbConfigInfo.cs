using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbHelp
{
    /// <summary>
    /// MongoDb配置消息
    /// </summary>
    public class MongoDbConfigInfo
    {
        /// <summary>
        /// 连接 127.0.1:27017
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库 
        /// </summary>
        public string DatabaseName { get; set; }
    }
}
