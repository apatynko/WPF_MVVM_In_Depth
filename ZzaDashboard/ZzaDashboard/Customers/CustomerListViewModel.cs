﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    class CustomerListViewModel: BindableBase
    {
        private ICustomersRepository _repository = new CustomersRepository();

        private ObservableCollection<Customer> _customers;

        public CustomerListViewModel()
        {
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
        }
        public ObservableCollection<Customer> Customers
        {
            get { return _customers;}
            set { SetProperty(ref _customers, value);}
        }

        public async void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(
                await _repository.GetCustomersAsync());
        }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
        public RelayCommand AddCustomerCommand { get; private set; }
        public RelayCommand<Customer> EditCustomerCommand { get; private set; }

        public event Action<Guid> PlaceOrderRequested = delegate { };
        public event Action<Customer> AddCustomerRequested = delegate { };
        public event Action<Customer> EditCustomerRequested = delegate { };

        private void OnPlaceOrder(Customer customer)
        {
            PlaceOrderRequested(customer.Id);
        }

        private void OnAddCustomer()
        {
            AddCustomerRequested(new Customer{ Id = new Guid() });
        }

        private void OnEditCustomer(Customer cust)
        {
            EditCustomerRequested(cust);
        }
    }
}
