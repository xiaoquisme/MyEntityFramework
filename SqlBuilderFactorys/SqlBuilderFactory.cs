using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using MyEntityFrameWork.SqlBuilderFactorys.Interface;

namespace MyEntityFrameWork.SqlBuilderFactorys
{

    
    public static class SqlBuilderFactory
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
