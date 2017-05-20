using MyEntityFrameWork.SqlBuilderFactorys.Interface;
using System;

namespace MyEntityFrameWork.SqlBuilderFactorys.Implement
{
    internal class MySqlSqlStatement : ISqlStatementBuilder
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
