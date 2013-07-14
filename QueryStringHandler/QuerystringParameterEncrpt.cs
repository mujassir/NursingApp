using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryStringHandler
{
    /// <summary>
    /// Encrpt Querystring parameter.
    /// </summary>
    public class QuerystringParameterEncrpt
    {

        #region Constant

        private const string Parameter_Name = "enc=";

        #endregion

        #region Public Methods

        public string EncrptQuerystringParam(string inputText)
        {
            try
            {
                EncrptDecrpt.Encrption encrption = new EncrptDecrpt.Encrption();

                return Parameter_Name + encrption.Encrypt(inputText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
