using System;
using System.Collections.Generic;
using System.Text;
using MyEntityFrameWork.SqlBuilderFactorys.Interface;

namespace MyEntityFrameWork.SqlBuilderFactorys.Implement
{
    public class OracleSqlStatement : ISqlStatementBuilder
    {
        public string CreateSqlString(object obj)
        {
            throw new NotImplementedException();
        }

        public string DeleteSqlString(object obj)
        {
            throw new NotImplementedException();
        }

        public string ReadSqlString(object obj)
        {
            throw new NotImplementedException();
        }

        public string UpdateSqlString(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
