namespace BlazorBasics.Captcha.Entities;

public record struct CaptchaProperties(CaptchaType Type, string Description, string Placeholder, ButtonType Button = ButtonType.Submit, string ErrorMessage = "Not Match!");