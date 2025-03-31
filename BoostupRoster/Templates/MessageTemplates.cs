namespace Boostup.API.Templates
{
    public static class MessageTemplates
    {
        public static string ForgotPasswordEmailTemplate(string userName, string link)
        {
            string message =  $"<h1>Hi {userName}</h1></br><p>You have requested a reset password email for logging to your account.</p><p>Please use the link below to reset your password</p><p><a href='{link}' target='_blank'>{link}</a></p><p>This link will expire in 24 hrs.</p><p>Please copy and paste this url in browser if it doesn't work.</p><div style=\"margin-top:20px\"><p style=\"margin:0\">Thankyou</p><p style=\"margin:5px 0\"><b>Boostup Cleaning Services</b></p></div>";
            return message ;
        }

       
    }
}
