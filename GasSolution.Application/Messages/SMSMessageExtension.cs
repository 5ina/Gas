
namespace GasSolution.Messages
{
    public static class SMSMessageExtension
    {
        /// <summary>
        /// 手机验证码
        /// </summary>
        /// <param name="_messageService"></param>
        /// <param name="content"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool SendMobileCode(this ISMSMessageService _messageService, string content, string mobile)
        {
            return _messageService.SendMessage(mobile, "石门油价", "SMS_109350046", "{\"code\":\"" + content + "\"}");
        }
    }
}
