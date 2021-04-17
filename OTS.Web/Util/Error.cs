using System.Collections.Generic;

namespace OTS.Web.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> Messages { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        public Error(List<string> messages)
        {
            Messages = messages ?? new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public Error(string message)
        {
            Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }
        }
    }
}