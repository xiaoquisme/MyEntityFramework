using Microsoft.Extensions.Configuration;
using MyEntityFrameWork.DateBaseFactory.BaseClass;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MyEntityFrameWork.SqlBuilderFactorys;
using MyEntityFrameWork.SqlBuilderFactorys.Interface;
using MyEntityFrameWork.TypeHelperFactorys;


namespace MyEntityFrameWork.DateBaseFactory.Implement
{
    public class SqlServerDatabase : BasicsDatabase
    {
        private ISqlStatementBuilder SqlBuilder;

        public SqlServerDatabase() : base()
        {
            base.Connection = new SqlConnection(DatabaseConncetionString());
            Connection.Open();
            base.Command = new SqlCommand();
            Command.Connection = base.Connection;
            //这里重要
            SqlBuilder = SqlBuilderFactory.GetInstance(DataBaseType.SqlServer);
        }

        public override List<T> GetAllInfo<T>()
        {
            Command.CommandText = SqlBuilder.ReadSqlString(new T());
            var Result = Command.ExecuteReader(CommandBehavior.SingleResult);
            var AllInfoList = new List<T>();
            while (Result.Read())
            {
                var Record = (IDataRecord)Result;
                AddOneObjectToList(ref AllInfoList, Record);
            }
           
            Result.Dispose();
            return AllInfoList;
        }

        protected override void AddOneObjectToList<T>(ref List<T> objectList, IDataRecord record) 
        {
            //获取所有的特性名称
            //查找record中的字段名称是否相同
            //如果相同将其值赋给该字段
            var PropertyNames = TypeHelperFactory.GetAllPropertyList(typeof(T));
            var Obj = new T();
            for (int i = 0; i < record.FieldCount; i++)
            {
                var PropertyName = PropertyNames.FirstOrDefault(name => name == record.GetName(i));
                if (PropertyName != null)
                {
                    TypeHelperFactory.SetPropertyValue(Obj, PropertyName, record[i]);
                }

            }
            objectList.Add(Obj);
        }
        public override bool Add(object data)
        {
            Command.CommandText = SqlBuilder.CreateSqlString(data);
            return Command.ExecuteNonQuery() > 0;
        }
        public override bool Remove(object data)
        {
            Command.CommandText = SqlBuilder.DeleteSqlString(data);
            return Command.ExecuteNonQuery() > 0;
        }

        public override bool Update(object data)
        {
            Command.CommandText = SqlBuilder.UpdateSqlString(data);
            return Command.ExecuteNonQuery() > 0;
        }

        protected override string DatabaseConncetionString()
        {
            return base.Configuration.GetConnectionString("UserInfoContext");
        }

    }
}
