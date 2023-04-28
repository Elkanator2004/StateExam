using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class SeederManager : IDisposable 
    {
        private readonly IServiceScope _scope;

        private readonly IdentityManager manager;
        private bool seedDone = false;

        public SeederManager(IServiceProvider services)
        {
            _sope = services.CreateScope();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
