using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EinsteinRiddle
{
    public class EnumHelper<T>
    {

        public static T Random(List<T> ts)
        {
            int z = RandomHelper.Random.Next(ts.Count);
            T r = ts[z];
            ts.Remove(r);
            return r;
        }

        private static T[] _list = (T[])Enum.GetValues(typeof(T));
        public static List<T> List()
        {
            List<T> tsl = new List<T>(_list);
            return tsl;

        }
    }
}
