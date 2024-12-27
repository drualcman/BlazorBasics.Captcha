namespace BlazorBasics.Captcha.Helpers;
internal static class RandomHelper
{
    internal static void CreateQuestion(List<CaptchaItem> CaptchaItems)
    {
        TOTPGeneratorHelper totpGenerator = new();
        string totp = totpGenerator.GenerateTOTP();
        CaptchaItems.Clear();
        CaptchaItems.Add(new CaptchaItem
        {
            Question = totp,
            Response = totp
        });
    }
}
