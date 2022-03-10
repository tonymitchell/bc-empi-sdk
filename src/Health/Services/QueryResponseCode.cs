using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Services
{
    ///<summary>
    ///Represents a response code from the Client Registry
    ///</summary>
    ///<example>BCHCIM.GD.0.0012 | The search completed successfully.</example>
    ///<example>BCHCIM.GD.2.0018 | Error: The identifier you used in the Get Demographics transaction does not exist in the EMPI.</example>
    public class QueryResponseCode
    {
        public enum ResponseType
        {
            Informational = 0,
            Warning = 1,
            Error = 2
        }

        public string Source { get; private set; }
        public ResponseType Type { get; private set; }
        public string Value { get; private set; }
        public string Message { get; private set; }

        public static QueryResponseCode Parse(string responseCodeString)
        {
            QueryResponseCode code = new QueryResponseCode();

            if (string.IsNullOrEmpty(responseCodeString))
                throw new ArgumentException("No response code value was provided.", "responseCodeValue");

            // Store message, if present
            string[] fields = responseCodeString.Split('|');
            code.Message = fields.ElementAtOrDefault(1)?.Trim();
            
            // P
            string codeString = fields.ElementAtOrDefault(0).Trim();
            string[] components = codeString.Split('.');
            if (components.Length < 4)
                throw new ArgumentException("Invalid response code: Does not contain at least 4 components.", "responseCodeValue");

            code.Source = components[0] + "." + components[1];
            code.Type = (ResponseType) int.Parse(components[2]);
            code.Value = string.Join(".", components, 3, components.Length - 3);

            return code;
        }

        /// <summary>
        /// Does the response represent an error?
        /// </summary>
        /// <returns>true if the response is an error</returns>
        public bool IsError()
        {
            return (Type == ResponseType.Error);
        }
    }
}
