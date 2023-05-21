namespace BlazorBasics.Captcha.Entities;

public class CaptchaItem
{
    public string Question { get; set; }
    public string Response { get; set; }
    public bool Selected { get; set; }

    public CaptchaItem() { }

    public CaptchaItem(string question, string response) : this(question, response, false) { }

    public CaptchaItem(string question, string response, bool selected)
    {
        Question = question;
        Response = response;
        Selected = selected;
    }
}
