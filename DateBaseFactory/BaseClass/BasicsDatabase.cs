using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MyEntityFrameWork.DateBaseFactory.BaseClass
{
    public abstract class BasicsDatabase
    {
        protected IDbConnection Connection { get; set; }
        public IConfigurationRoot Configuration { get; }
        protected IDbCommand Command { get; set; }

        protected BasicsDatabase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        protected abstract string DatabaseConncetionString();
        public abstract List<T> GetAllInfo<T>() where T : new();
        public abstract bool Add(object data);
        public abstract bool Update(object data);
        public abstract bool Remove(object data);
    }
}
