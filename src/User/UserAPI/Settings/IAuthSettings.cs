using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Settings
{
    public interface IAuthSettings
    {
        string Secret { get; set; }
    }
}
