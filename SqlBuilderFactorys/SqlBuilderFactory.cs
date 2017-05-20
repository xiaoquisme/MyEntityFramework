using MyEntityFrameWork.SqlBuilderFactorys.Interface;
using System;
using System.Reflection;

namespace MyEntityFrameWork.SqlBuilderFactorys
{


    internal static class SqlBuilderFactory
    {
        public static ISqlStatementBuilder GetInstance(DataBaseType DbType)
        {         
            string DataBaseTypeName = Enum.Parse(DbType.GetType(), DbType.ToString()).ToString();
            var NamespaceName = "MyEntityFrameWork.SqlBuilderFactorys.Implement";
            string InstanceClassName = DataBaseTypeName + "SqlStatement";
            return (ISqlStatementBuilder)Assembly.Load(new AssemblyName("MyEntityFrameWork"))
                                              .CreateInstance(NamespaceName+"."+InstanceClassName);
        }
        
    }
}
