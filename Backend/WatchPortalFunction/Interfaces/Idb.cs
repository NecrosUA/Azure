using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPortalFunction.Interfaces;

internal interface Idb<T> where T : class
{
    T GetUser(string pid);
    void UpdateUser(T item);
    void Save();
}
