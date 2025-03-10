using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace PrismDemo.ViewModels
{
    public class DialogDemoViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private string _dialogResult;

        public string DialogResult
        {
            get => _dialogResult;
            set => SetProperty(ref _dialogResult, value);
        }

        public DelegateCommand ShowSimpleDialogCommand { get; }
        public DelegateCommand ShowCustomDialogCommand { get; }
        public DelegateCommand ShowConfirmationDialogCommand { get; }

        public DialogDemoViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            ShowSimpleDialogCommand = new DelegateCommand(ShowSimpleDialog);
            ShowCustomDialogCommand = new DelegateCommand(ShowCustomDialog);
            ShowConfirmationDialogCommand = new DelegateCommand(ShowConfirmationDialog);
        }

        private void ShowSimpleDialog()
        {
            var message = "This is a simple dialog message.";
            _dialogService.ShowDialog("MessageDialog", new DialogParameters($"message={message}"), r =>
            {
                DialogResult = $"Simple Dialog Result: {r.Result}";
            });
        }

        private void ShowCustomDialog()
        {
            _dialogService.ShowDialog("CustomDialog", null, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    var result = r.Parameters.GetValue<string>("result");
                    DialogResult = $"Custom Dialog Result: {result}";
                }
                else
                {
                    DialogResult = "Custom Dialog was cancelled";
                }
            });
        }

        private void ShowConfirmationDialog()
        {
            var message = "Are you sure you want to proceed?";
            _dialogService.ShowDialog("ConfirmationDialog", new DialogParameters($"message={message}"), r =>
            {
                DialogResult = $"Confirmation Dialog Result: {r.Result}";
            });
        }
    }
} 