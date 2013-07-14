using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace RMC.BussinessService
{
    public class BSSerialization
    {

        #region Serialization Methods
        public static MemoryStream Serialize<T>(T obj)
        {
            DataContractSerializer s = new DataContractSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            s.WriteObject(ms, obj);
            return ms;
        }
        public static T Deserialize<T>(MemoryStream ms)
        {
            DataContractSerializer s = new DataContractSerializer(typeof(T));
            ms.Position = 0;
            T retObject = (T)s.ReadObject(ms);
            return retObject;
        }
             
        #endregion
    }
}
