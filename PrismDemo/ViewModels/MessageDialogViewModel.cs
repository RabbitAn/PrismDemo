using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace PrismDemo.ViewModels
{
    public class MessageDialogViewModel : BindableBase, IDialogAware
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public string Title => "消息";

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand CloseCommand { get; }

        public MessageDialogViewModel()
        {
            CloseCommand = new DelegateCommand(OnClose);
        }

        private void OnClose()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
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
            Message = parameters.GetValue<string>("message");
        }
    }
} 