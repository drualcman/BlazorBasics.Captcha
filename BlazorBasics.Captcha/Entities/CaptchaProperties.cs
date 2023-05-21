namespace BlazorBasics.Captcha.Entities;

public record struct CaptchaProperties(CaptchaType Type, string Description, string Placeholder, string ErrorMessage = "Not Match!");