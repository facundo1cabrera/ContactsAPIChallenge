using System.Net.Mail;

namespace ContactsAPI.Utils
{
    public static class ValidateIsEmailAddress
    {
        public static bool IsEmailAddress(this string emailAddress)
        {
            try
            {
                var email = new MailAddress(emailAddress);
                return email.Address == emailAddress.Trim();
            }
            catch
            {
                return false;
            }
        }
    }
}
