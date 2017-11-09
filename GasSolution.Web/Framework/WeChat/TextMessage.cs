namespace GasSolution.Web.Framework.WeChat
{
    /// <summary>
    /// 微信文本消息
    /// </summary>
    public class TextMessage
    {

        public TextMessage()
        {
            this.msgtype = "text";
        }
        /// <summary>
        /// 
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msgtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Content text { get; set; }

        public class Content
        {

            /// <summary>
            /// 
            /// </summary>
            public string content { get; set; }
        }

    }
}