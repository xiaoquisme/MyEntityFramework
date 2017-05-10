using System;
using System.Collections.Generic;
using System.Text;
using MyEntityFrameWork.SqlBuilderFactory.Interface;
using MyEntityFrameWork.TypeHelperFactorys;

namespace MyEntityFrameWork.SqlBuilderFactory.Implement
{
    public class SqlBuilderFactory : ICreate, IRead, IUpdate, IDelete
    {

        #region  获取Insertsql语句 实现ICreate接口

        public string CreateSqlString(object obj)
        {
            var type = obj.GetType();
            var TableName = TypeHelperFactory.GetTableName(type);
            var PrimaryKeyName = TypeHelperFactory.GetPrimaryKey(type);
            var PropertyNameAndValueDictionary = TypeHelperFactory.GetAllPropertyNameAndValueDictionary(obj);
            PropertyNameAndValueDictionary.Remove(PrimaryKeyName);
            var PropertyNameList = new List<string>();
            var PropertyValueList = new List<object>();
            foreach (var item in PropertyNameAndValueDictionary)
            {
                PropertyNameList.Add(item.Key);
                PropertyValueList.Add(item.Value);
            }
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
            var NameAndValueList = new List<string>();
            foreach (var item in PropertyNameAndValueDictionary)
            {
                NameAndValueList.Add($"{item.Key}='{item.Value}'");
            }
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
