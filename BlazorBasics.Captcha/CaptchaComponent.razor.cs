using BlazorBasics.Captcha.Entities;
using BlazorBasics.Captcha.Validdators;
using BlazorBasics.Captcha.ViewModel;
using Microsoft.AspNetCore.Components;

namespace BlazorBasics.Captcha
{
    public partial class CaptchaComponent
    {
        [Parameter, EditorRequired] public CaptchaProperties Properties { get; set; }
        [Parameter, EditorRequired] public EventCallback<bool> OnValidate { get; set; }
        [Parameter] public Func<Task<IEnumerable<CaptchaItem>>> DataSource { get; set; }
        [Parameter] public RenderFragment BeforeValidate { get; set; }
        [Parameter] public RenderFragment AfterValidate { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> WrapperAttributes { get; set; }

        CaptchaViewModel ViewModel;

        protected override async Task OnParametersSetAsync()
        {
            IEnumerable<CaptchaItem> questions = default;
            if(DataSource is not null)
            {
                questions = await DataSource();
            }
            ViewModel = new CaptchaViewModel(Properties.Type, questions);
        }

        private async Task OnValidate_Click()
        {
            ViewModel.Validate();
            if(OnValidate.HasDelegate)
                await OnValidate.InvokeAsync(ViewModel.IsValidated);
        }
    }
}