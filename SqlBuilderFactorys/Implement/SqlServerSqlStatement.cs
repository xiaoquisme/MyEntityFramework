using MyEntityFrameWork.SqlBuilderFactorys.Interface;
using MyEntityFrameWork.TypeHelperFactorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyEntityFrameWork.SqlBuilderFactorys.Implement
{
    internal class SqlServerSqlStatement :ISqlStatementBuilder
    {

        #region  获取Insertsql语句 实现ICreate接口

        public string CreateSqlString(object obj)
        {
            var type = obj.GetType();
            var TableName = TypeHelperFactory.GetTableName(type);
            var PrimaryKeyName = TypeHelperFactory.GetPrimaryKey(type);
            var PropertyNameAndValueDictionary = TypeHelperFactory.GetAllPropertyNameAndValueDictionary(obj);
            PropertyNameAndValueDictionary.Remove(PrimaryKeyName);

            #region 这里进行了修改 2017-5-17 17：10 以后考虑用元组实现

            //这是原来的代码
            //var PropertyNameList = new List<string>();
            //var PropertyValueList = new List<object>();           
            //foreach (var item in PropertyNameAndValueDictionary)
            //{
            //    PropertyNameList.Add(item.Key);
            //    PropertyValueList.Add(item.Value);
            //}
            //这是新的代码 测试已通过 以后考虑用元组实现 2017-5-17 17：15
            
            var PropertyNameList = from item in PropertyNameAndValueDictionary
                                   select item.Key;
            var PropertyValueList = from item in PropertyNameAndValueDictionary
                                    select item.Value;
            //这是新的代码 用元组实现 作用似乎不大
            //已经注释掉
            //var  result = from item in PropertyNameAndValueDictionary
            //             select new Tuple<string , object>(item.Key, item.Value);
          
            #endregion

            string sql1 = string.Join(",", PropertyNameList);
            string sql2 = "'";
            sql2 += string.Join("','", PropertyValueList);
            sql2 += "'";
            var SqlStatement = new StringBuilder();
            SqlStatement.AppendFormat($"insert into {TableName} ({sql1}) values ({sql2})");
            return SqlStatement.ToString();

        }

        #endregion
         
        #region 获取Readsql语句 实现IRead接口

        public string ReadSqlString(object obj)
        {
            var type = obj.GetType();
            var PropertyList = TypeHelperFactory.GetAllPropertyList(type);
            var TableName = TypeHelperFactory.GetTableName(type);
            string SelectString = string.Join(",", PropertyList);
            StringBuilder SqlStatement = new StringBuilder();
            SqlStatement.AppendFormat($"select {SelectString} from {TableName}");
            return SqlStatement.ToString();
        }

        #endregion

        #region 获取Updatesql语句 实现IUpdate接口
        public string UpdateSqlString(object obj)
        {
            var type = obj.GetType();
            var TableName = TypeHelperFactory.GetTableName(type);
            var PrimaryKeyName = TypeHelperFactory.GetPrimaryKey(type);
            if (PrimaryKeyName == null)
            {
                throw new Exception("不存在主键");
            }
            var PrimaryKeyValue = TypeHelperFactory.GetPrimaryKeyValue(obj, PrimaryKeyName);
            var PropertyNameAndValueDictionary = TypeHelperFactory.GetAllPropertyNameAndValueDictionary(obj);
            PropertyNameAndValueDictionary.Remove(PrimaryKeyName);

            #region 这里进行了修改 2017-5-17 17：00      
            //
            //这是原来的代码
            //var NameAndValueList = new List<string>();
            //foreach (var item in PropertyNameAndValueDictionary)
            //{
            //    NameAndValueList.Add($"{item.Key}='{item.Value}'");
            //}

            //这是新的代码 2017-5-17 17：00 测试已通过
            var NameAndValueList = from item in PropertyNameAndValueDictionary
                                   select $"{item.Key}='{item.Value}'";
            #endregion

            string sql = string.Join(",", NameAndValueList);
            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.AppendFormat(
                                      $"update {TableName} set {sql} " +
                                      $"where {PrimaryKeyName}='{PrimaryKeyValue}'"
                                     );
            return sqlStatement.ToString();
        }



        #endregion

        #region 获取Deletesql语句 实现IDelete接口
        public string DeleteSqlString(object obj)
        {
            var type = obj.GetType();
            var TableName = TypeHelperFactory.GetTableName(type);
            var PrimaryKey = TypeHelperFactory.GetPrimaryKey(type);
            if (PrimaryKey == null)
            {
                throw new Exception("不存在主键");
            }
            var PrimaryKeyValue = TypeHelperFactory.GetPrimaryKeyValue(obj, PrimaryKey);
            StringBuilder SqlStatement = new StringBuilder();
            SqlStatement.AppendFormat($"delete from {TableName} where {PrimaryKey}='{PrimaryKeyValue}'");
            return SqlStatement.ToString();
        }


        #endregion
    }
}
