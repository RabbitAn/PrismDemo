using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace PrismDemo.ViewModels
{
    public class CustomDialogViewModel : BindableBase, IDialogAware
    {
        private string _userInput;
        public string UserInput
        {
            get => _userInput;
            set => SetProperty(ref _userInput, value);
        }

        public string Title => "Custom Dialog";

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand OKCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public CustomDialogViewModel()
        {
            OKCommand = new DelegateCommand(OnOK);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        private void OnOK()
        {
            var parameters = new DialogParameters
            {
                { "result", UserInput }
            };
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameters));
        }

        private void OnCancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
} 