using MaritimApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MaritimApp.Commands
{
    public class HapusLokasiCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var selected = (Lokasi)parameter;
            App.LokasiVM.HapusLokasi(selected);
        }
    }
}
