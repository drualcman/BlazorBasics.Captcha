namespace BlazorBasics.Captcha.ViewModel;

internal class CaptchaViewModel
{
    public string ToValidate { get; set; }
    public bool IsValidated { get; private set; }
    public bool IsValid { get; private set; }
    public string Question =>
        CaptchaItems.FirstOrDefault(q => q.Selected == true).Question ?? "";

    readonly List<CaptchaItem> CaptchaItems;
    readonly CaptchaType Type;
    public CaptchaViewModel(CaptchaType type, IEnumerable<CaptchaItem> dataSource)
    {
        Type = type;
        CaptchaItems = new List<CaptchaItem>(dataSource);
        IsValidated = false;
        Initialize();
    }

    private CaptchaItem Selected =>
        CaptchaItems.FirstOrDefault(q => q.Selected == true);

    void Initialize()
    {
        ToValidate = string.Empty;
        switch (Type)
        {
            case CaptchaType.Numeric:
                NumericHelper.CreateQuestion(CaptchaItems);
                break;
            case CaptchaType.CountryCapitals:
                CapitalsHelper.CreateQuestions(CaptchaItems);
                break;
            case CaptchaType.Random:
                RandomHelper.CreateQuestion(CaptchaItems);
                break;
            case CaptchaType.Custom:
            default:
                if (!HasItems())
                    throw new ArgumentException("Captcha questions must be have some to show.");
                break;
        }
        SelectRandomOption();
    }

    private bool HasItems()
    {
        bool result;
        if (CaptchaItems is null) result = false;
        else result = CaptchaItems.Any();
        return result;
    }

    private void SelectRandomOption()
    {
        if (CaptchaItems.Count > 1)
        {
            Random random = new Random();
            int totalItems = CaptchaItems.Count;
            for (int i = 0; i < totalItems; i++)
            {
                CaptchaItems[i].Selected = false;
            }
            int indexToSelect = random.Next(0, CaptchaItems.Count);
            CaptchaItems[indexToSelect].Selected = true;
        }
        else
            CaptchaItems[0].Selected = true;
    }

    public void Validate()
    {
        IsValidated = true;
        CaptchaValidaor validator = new CaptchaValidaor(Selected);
        IsValid = validator.Validate(ToValidate);
        if (!IsValid) Initialize();
    }

    public string SetPlaceholder(string placeholder)
    {
        string result = Type switch
        {
            CaptchaType.Numeric => Question,
            CaptchaType.Random => Question,
            _ => placeholder,
        };
        return result;
    }

}
