using MyEntityFrameWork.DateBaseFactory.BaseClass;
using MyEntityFrameWork.SqlBuilderFactorys;
using System;
using System.Collections.Generic;

namespace MyEntityFrameWork.DateBaseFactory.Implement
{
    /// <summary>
    /// Oracle数据库的实现方式
    /// 这是只是用于演示并没有真正实现
    /// 时间：2017-5-12
    /// Author：曲
    /// </summary>
    public class OracleDataBase : BasicsDatabase
    {

        public OracleDataBase() : base()
        {
            //base.Connection = new OracleConnection(DatabaseConncetionString());
            //Connection.Open();
            //base.Command = new OracleCommmand();
            //Command.Connection = Connection;
            //base.SqlBuilder = SqlBuilderFactory.GetInstance(DataBaseType.Oracle);
        }
        public override bool Add(object data)
        {
            Command.CommandText = SqlBuilder.CreateSqlString(data);
            return Command.ExecuteNonQuery() > 0;
        }

        public override List<T> GetAllInfo<T>()
        {
            throw new NotImplementedException();
        }

        public override bool Remove(object data)
        {
            throw new NotImplementedException();
        }

        public override bool Update(object data)
        {
            throw new NotImplementedException();
        }

        protected override string DatabaseConncetionString()
        {
            throw new NotImplementedException();
        }
    }
}
