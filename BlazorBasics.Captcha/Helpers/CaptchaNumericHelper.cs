using BlazorBasics.Captcha.Entities;

namespace BlazorBasics.Captcha.Helpers;
internal static class CaptchaNumericHelper
{
    internal static void CreateQuestion(List<CaptchaItem> CaptchaItems)
    {
        Random random = new Random();
        int operator1 = random.Next(1, 10);
        int operator2 = random.Next(1, 10);
        CaptchaItems.Clear();
        CaptchaItems.Add(new CaptchaItem
        {
            Question = $"{operator1} + {operator2}",
            Response = $"{operator1 + operator2}"
        });
    }


}
