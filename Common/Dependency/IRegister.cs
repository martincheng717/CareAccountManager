using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Gdot.Care.Common.Dependency
{
    public interface IRegister
    {
        void Register(ContainerBuilder builder);
    }
}
