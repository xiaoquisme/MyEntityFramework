using System;
using System.Collections.Generic;
using System.Text;
using MyEntityFrameWork.DateBaseFactory.BaseClass;
using MyEntityFrameWork.SqlBuilderFactorys;

namespace MyEntityFrameWork.DateBaseFactory.Implement
{
    public class MySqlDataBase : BasicsDatabase
    {
        /// <summary>
        /// 这是MySql数据库的实现方式
        /// 只用于演示并没有实现功能
        /// 时间：2017-5-12
        /// Author：曲
        /// </summary>
        public MySqlDataBase() : base()
        {
            //base.Connection = new MySqlConnection();
            //base.Connection.Open();
            //base.Command = new MySqlCommand();
            //base.Command.Connection = base.Connection;
            //base.SqlBuilder = SqlBuilderFactory.GetInstance(DataBaseType.MySql);
        }
        public override bool Add(object data)
        {
            base.Command.CommandText = SqlBuilder.CreateSqlString(data);
            return base.Command.ExecuteNonQuery() > 0;
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
