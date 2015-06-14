using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritimApp.Libs
{
    public interface ISettings
    {
        T GetValues<T>(string key);
        void SetValue<T>(string key, T value);
    }
}
