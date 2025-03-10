using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PrismDemo.ViewModels
{
    public class FormDemoViewModel : BindableBase, INotifyDataErrorInfo
    {
        private string _name;
        private string _email;
        private string _age;
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private ObservableCollection<string> _validationErrors = new ObservableCollection<string>();

        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                ValidateName();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                ValidateEmail();
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                SetProperty(ref _age, value);
                ValidateAge();
            }
        }

        public ObservableCollection<string> ValidationErrors
        {
            get => _validationErrors;
            set => SetProperty(ref _validationErrors, value);
        }

        public DelegateCommand SubmitCommand { get; }

        public FormDemoViewModel()
        {
            SubmitCommand = new DelegateCommand(OnSubmit, CanSubmit)
                .ObservesProperty(() => HasErrors);
        }

        private void OnSubmit()
        {
            // Handle form submission
            ValidationErrors.Clear();
            ValidationErrors.Add("Form submitted successfully!");
        }

        private bool CanSubmit()
        {
            return !HasErrors;
        }

        private void ValidateName()
        {
            ClearErrors(nameof(Name));
            if (string.IsNullOrWhiteSpace(Name))
            {
                AddError(nameof(Name), "Name is required");
            }
            else if (Name.Length < 2)
            {
                AddError(nameof(Name), "Name must be at least 2 characters long");
            }
        }

        private void ValidateEmail()
        {
            ClearErrors(nameof(Email));
            if (string.IsNullOrWhiteSpace(Email))
            {
                AddError(nameof(Email), "Email is required");
            }
            else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                AddError(nameof(Email), "Invalid email format");
            }
        }

        private void ValidateAge()
        {
            ClearErrors(nameof(Age));
            if (string.IsNullOrWhiteSpace(Age))
            {
                AddError(nameof(Age), "Age is required");
            }
            else if (!int.TryParse(Age, out int age) || age < 0 || age > 120)
            {
                AddError(nameof(Age), "Age must be a number between 0 and 120");
            }
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }
    }
} 