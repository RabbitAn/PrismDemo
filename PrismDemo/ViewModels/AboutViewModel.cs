using Prism.Commands;
using PrismDemo.NavigationServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.ViewModels
{
    public class AboutViewModel : NavigationServerCore
    {
        private DelegateCommand _openGitHub;
        public DelegateCommand OpenGitHub
        {
            get { return _openGitHub; }
            set { SetProperty(ref _openGitHub, value); }
        }

        public AboutViewModel()
        {
            _openGitHub = new(GetOG);
        }

        private void GetOG()
        {
                Process.Start(new ProcessStartInfo("https://github.com/RabbitAn/PrismDemo") { UseShellExecute = true });
        }
    }
}
