using System;
using System.Collections.Generic;

namespace Dapper.Extensions.Microsoft.DependencyInjection
{
    public abstract class DapperContextOptions
    {
        public readonly Dictionary<Type, dynamic> Extensions;

        protected DapperContextOptions(Dictionary<Type, dynamic> pExtensions)
        {
            Extensions = pExtensions;
        }
    }
    public class DapperContextOptions<TDapperContext> : DapperContextOptions
        where TDapperContext : DapperContext
    {
        public DapperContextOptions(Dictionary<Type, dynamic> pExtensions) : base(pExtensions)
        {
        }
    }

}