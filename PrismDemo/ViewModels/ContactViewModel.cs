using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace PrismDemo.ViewModels
{
    public class ContactViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        
        private string _name;
        private string _email;
        private string _phone;
        private string _message;
        private string _submitResult;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public string SubmitResult
        {
            get => _submitResult;
            set => SetProperty(ref _submitResult, value);
        }

        public DelegateCommand SubmitCommand { get; }

        public ContactViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            SubmitCommand = new DelegateCommand(SubmitForm, CanSubmitForm)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => Email)
                .ObservesProperty(() => Message);
        }

        private bool CanSubmitForm()
        {
            return !string.IsNullOrWhiteSpace(Name) && 
                   !string.IsNullOrWhiteSpace(Email) && 
                   !string.IsNullOrWhiteSpace(Message);
        }

        private void SubmitForm()
        {
            // 模拟提交表单
            SubmitResult = "您的信息已成功提交！我们会尽快与您联系。";
            
            // 显示成功消息
            var parameters = new DialogParameters
            {
                { "message", "感谢您的留言！我们会尽快回复您。" }
            };
            
            _dialogService.ShowDialog("MessageDialog", parameters, _ => {
                // 重置表单
                ResetForm();
            });
        }

        private void ResetForm()
        {
            Name = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Message = string.Empty;
        }
    }
} 