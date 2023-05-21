using BlazorBasics.Captcha.Entities;

namespace BlazorBasics.Captcha.Validdators;
internal class CaptchaValidaor
{
    readonly CaptchaItem Item;

    public CaptchaValidaor(CaptchaItem item) => Item = item;

    public bool Validate(string ToValidate) =>
        ToValidate == Item.Response;
}
