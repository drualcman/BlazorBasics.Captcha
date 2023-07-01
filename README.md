# BlazorBasics.Captcha
Add a captcha control into your Balzor Server or Blazor Webassembly application

# How to use
Clone this repository.<br/>
Add project to your solution.<br/>
Use the component CaptchaComponent into the component you want to check a real user.
``` RAZOR
<CaptchaComponent />
```

## Parameters
``` CSHARP
CaptchaProperties Properties;						//how to configure the captcha
EventCallback<bool> OnValidate;						//delegate to execute when validate the captcha
Func<Task<IEnumerable<CaptchaItem>>> DataSource;	//delegate from where get the CaptchaItems to validate
RenderFragment BeforeValidate;						//content to show before validate
RenderFragment AfterValidate;						//content to show after validate
EventCallback OnSubmit;								//delegate to use when default button is set different of submit
Dictionary<string, object> WrapperAttributes;		//attributes for the container of the HTML
```

## Entities
``` CSHARP
namespace BlazorBasics.Captcha.Entities;

public enum ButtonType
{
    Submit, Button, Reset
}

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

public record struct CaptchaProperties(
    CaptchaType Type = CaptchaType.Numeric, 
    string Description = "", 
    string Placeholder = "", 
    ButtonType Button = ButtonType.Submit, 
    string ErrorMessage = "Not Match!");

public enum CaptchaType
{
    Numeric, CountryCapitals, Custom
}
```

## Personalize content
``` RAZOR
<EditForm Model="ContactUsForm" OnValidSubmit="SendForm">
    <div class="columns">
        <div class="column">
            <div class="field">
                <label class="label"></label>
                <div class="control">
                    <InputText class="input" type="email" placeholder="Email Address" @bind-Value=Email />
                </div>
            </div>
        </div>
    </div>
    <div class="columns">
        <CaptchaComponent class="column" Properties=MyCaptcha>
            <BeforeValidate>
                <span class="button is-link">Solve math</span>
            </BeforeValidate>
            <AfterValidate>
                <div class="column">
                    <button type="submit" class="button is-info">SEND MESSAGE</button>
                </div>
            </AfterValidate>
        </CaptchaComponent>
    </div>
</EditForm>

@code{
    string Email;

    CaptchaProperties MyCaptcha = new CaptchaProperties(
        Type: CaptchaType.Numeric,
        ErrorMessage: "Solve the math correctly please...");

    void SendForm(){
        //some code...
    }
}
```


