using Database.Contexts;
using Database.Entity;
using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace PrismFinal.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            
        }
    }
}
