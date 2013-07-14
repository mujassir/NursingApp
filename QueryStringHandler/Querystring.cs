using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace QueryStringHandler
{
    class Querystring : IHttpModule
    {

        #region Constant

        private const string Parameter_Name = "enc=";

        #endregion
                
        #region IHttpModule Members
        
        // Classes that inherit IHttpModule 
        // must implement the Init and Dispose methods. 
        public void Dispose()
        {
            // Nothing to dispose   
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        #endregion

        #region Events

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            string query = string.Empty;
            string path = string.Empty;

            try
            {
                if (context.Request.Url.OriginalString.Contains("aspx") && context.Request.RawUrl.Contains("?"))
                {
                    query = ExtractQuery(context.Request.RawUrl);
                    path = GetVirtualPath();

                    if (query.StartsWith(Parameter_Name, StringComparison.OrdinalIgnoreCase))
                    {
                        // Decrypts the query string and rewrites the path.   
                        string rawQuery = query.Replace(Parameter_Name, string.Empty);
                        string decryptedQuery = Decrypt(rawQuery);
                        context.RewritePath(path, string.Empty, decryptedQuery);
                    }
                    else if (context.Request.HttpMethod == "GET")
                    {
                        // Encrypt the query string and redirects to the encrypted URL.   
                        // Remove if you don't want all query strings to be encrypted automatically.   
                        string encryptedQuery = Encrypt(query);
                        context.Response.Redirect(path + encryptedQuery, false);
                    }
                }
            }
            catch (Exception ex)
            {
                // m_Logger.Error("An error occurred while parsing the query string in the URL: " + path, ex);   
                context.Response.Redirect("~/Home.aspx", false);
            }

        } 

        #endregion

        #region Private Methods
        
        /// <summary>   
        /// Parses the current URL and extracts the virtual path without query string.   
        /// </summary>   
        /// <returns>The virtual path of the current URL.</returns>   
        private static string GetVirtualPath()
        {
            try
            {
                string path = HttpContext.Current.Request.RawUrl;
                path = path.Substring(0, path.IndexOf("?"));
                path = path.Substring(path.LastIndexOf("/") + 1);
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>   
        /// Parses a URL and returns the query string.   
        /// </summary>   
        /// <param name="url">The URL to parse.</param>   
        /// <returns>The query string without the question mark.</returns>   
        private static string ExtractQuery(string url)
        {
            try
            {
                int index = url.IndexOf("?") + 1;
                return url.Substring(index);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        #endregion

        #region Encryption/decryption

        /// <summary>   
        /// Use encrption method in Encrption Class.
        /// </summary>   
        /// <param name="inputText">The string to encrypt.</param>   
        /// <returns>A Base64 encrypted string.</returns>   
        private static string Encrypt(string inputText)
        {
            EncrptDecrpt.Encrption encrption = new EncrptDecrpt.Encrption();

            return "?" + Parameter_Name + encrption.Encrypt(inputText);
        }

        /// <summary>   
        /// Decrypts a previously encrypted string.   
        /// </summary>   
        /// <param name="inputText">The encrypted string to decrypt.</param>   
        /// <returns>A decrypted string.</returns>   
        private static string Decrypt(string inputText)
        {
            EncrptDecrpt.Encrption encrption = new EncrptDecrpt.Encrption();

            return encrption.Decrypt(inputText);
        }

        #endregion

    }
}
