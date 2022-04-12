namespace OneDay.Core.Ui.Dialogs
{
    public class ConfirmDialogController : UiController<ConfirmDialogView>
    {
        protected override void Awake()
        {
            base.Awake();
            View.ConfirmButton.onClick.AddListener(() => Hide());
        }

        protected override void OnShow(KeyValueData data)
        {
            View.SetText(data.Get<string>("text"));
        }
    }
}
