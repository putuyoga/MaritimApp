using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritimApp.Libs
{
    /// <summary>
    /// Digunakan untuk mendapatkan prefix tiap-tiap resources,
    /// Windows Phone diletakkan di subfolder Assets dan Android
    /// di root /drawable
    /// </summary>
    public interface IPrefixName
    {
        /// <summary>
        /// Dapatkan prefix masing-masing platform
        /// </summary>
        /// <param name="name">nama asset spesifik.</param>
        /// <returns></returns>
        string GetPrefixName(string name);
    }
}
