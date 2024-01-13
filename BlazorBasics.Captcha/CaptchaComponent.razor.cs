namespace BlazorBasics.Captcha
{
    public partial class CaptchaComponent : ComponentBase
    {
        [Parameter] public CaptchaProperties Properties { get; set; }
        [Parameter] public EventCallback<bool> OnValidate { get; set; }
        [Parameter] public Func<Task<IEnumerable<CaptchaItem>>> DataSource { get; set; }
        [Parameter] public RenderFragment BeforeValidate { get; set; }
        [Parameter] public RenderFragment AfterValidate { get; set; }
        [Parameter] public EventCallback OnSubmit { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> WrapperAttributes { get; set; }

        CaptchaViewModel ViewModel;

        protected override async Task OnInitializedAsync() 
        {
            IEnumerable<CaptchaItem> questions = new List<CaptchaItem>();
            if(DataSource is not null)
            {
                if(Properties.Type != CaptchaType.Custom)
                {
                    questions = new List<CaptchaItem>();
                }
                else
                {
                    questions = await DataSource();
                }
            }
            ViewModel = new CaptchaViewModel(Properties.Type, questions);
        }

        private async Task OnValidate_Click()
        {
            ViewModel.Validate();
            if(OnValidate.HasDelegate)
                await OnValidate.InvokeAsync(ViewModel.IsValid);
        }

        private async Task Submit_Click()
        {
            if(Properties.Button != ButtonType.Submit && OnSubmit.HasDelegate)
                await OnSubmit.InvokeAsync();
        }
    }
}