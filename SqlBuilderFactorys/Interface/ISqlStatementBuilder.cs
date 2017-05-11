using System;
using System.Collections.Generic;
using System.Text;

namespace MyEntityFrameWork.SqlBuilderFactorys.Interface
{
    public interface ISqlStatementBuilder : ICreate, IRead, IUpdate, IDelete
    {

    }
}
