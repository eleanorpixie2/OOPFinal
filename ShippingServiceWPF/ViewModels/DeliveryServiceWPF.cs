﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClassLibraryFinal;

namespace ShippingServiceWPF.ViewModels
{
    class DeliveryServiceWPF:INotifyPropertyChanged
    {
        //update shipping service
        public ICommand UpdateDeliveryService { get; set; }
        //the shipping service
        public IShippingService ShippingService;

        public DeliveryServiceWPF()
        {
            UpdateDeliveryService = new WPFShippingCommand(ExecuteCommandUpdateDeliveryService, CanExecuteCommand);

            DeliveryServices = new ObservableCollection<IDeliveryService>()
            {
                new AirExpress(new Plane()),
                new UnclesTruck(new Truck()),
                new SnailService(new ShippingSnail())
            };
            SelectedService = DeliveryServices[0];
            ShippingService = new DefaultShippingService(SelectedService, new List<IProduct>(), new ShippingLocation(StartZip, DestZip));
        }
        private bool CanExecuteCommand(object parameter)
        {
            //makes sure the command can execute
            return true;
        }

        //update the shipping service values
        private void ExecuteCommandUpdateDeliveryService(object parameter)
        {
            ShippingService = new DefaultShippingService(SelectedService, new List<IProduct>(), new ShippingLocation(StartZip, DestZip));
            RaisePropertyChanged("NumOfRefuels");
            RaisePropertyChanged("Distance");
            RaisePropertyChanged("CostOfRefuels");
        }

        
        //delivery service list
        public ObservableCollection<IDeliveryService> DeliveryServices
        {
            get;
            set;
        }

        //selected delivery service
        public IDeliveryService SelectedService
        {
            get;
            set;
        }


        private uint _startZip=60605;
        //zip to ship from
        public uint StartZip
        {
            get { return _startZip; }
            set
            {
                if(_startZip!=value)
                {
                    _startZip = value;
                    RaisePropertyChanged("StartZip");
                }
            }
        }

        private uint _destZip=60090;
        //zip to ship to
        public uint DestZip
        {
            get {return _destZip; }
            set
            {
                //set from gui
                if(_destZip!=value)
                {
                    _destZip = value;
                    RaisePropertyChanged("DestZip");
                }
            }
        }

        //The cost of the total number of refuels
        public string CostOfRefuels
        {
            get { return String.Format("{0:C}", ShippingService.NumRefuels * ShippingService.DeliveryService.CostPerRefuel); }
        }

        //number of refuels needed
        public string NumOfRefuels
        {
            get { return String.Format("{0:n0}",ShippingService.NumRefuels); }
        }

        //distance from start to finish
        public string Distance
        {
            get { return String.Format("{0:n0}", ShippingService.ShippingDistance); }
        }

        //Event management
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class WPFShippingCommand : ICommand
    {
        public delegate void ICommandOnExecute(object parameter);
        public delegate bool ICommandOnCanExecute(object parameter);

        private ICommandOnExecute _execute;
        private ICommandOnCanExecute _canExecute;

        public WPFShippingCommand(ICommandOnExecute onExecuteMethod, ICommandOnCanExecute onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        #endregion
    }
}
