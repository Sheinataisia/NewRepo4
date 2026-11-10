using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace Sheina41
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            var currentProduct = шеина41Entities1.GetContext().Product.ToList();
            ServiceListView.ItemsSource = currentProduct;
            ComboType.SelectedIndex = 0;
            UpdateProduct();
        }
        public ProductPage(User user)
        {
            InitializeComponent();
            FIOTB.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            switch (user.UserRole)
            {
                case 1:
                    RoleTB.Text = "Клиент"; break;
                case 2:
                    RoleTB.Text = "Менеджер"; break;
                case 3:
                    RoleTB.Text = "Администратор"; break;
            }
            var currentProduct = шеина41Entities1.GetContext().Product.ToList();
            ServiceListView.ItemsSource = currentProduct;
            TotalRecords = currentProduct.Count;
            UpdateProduct();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager1.MainFrame.Navigate(new AddEditPage());
        }

        private void UpdateProduct()
        {
            var currentProduct = шеина41Entities1.GetContext().Product.ToList();
            if(ComboType.SelectedIndex == 0)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscount >= 0 && p.ProductDiscount <= 100)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscount >= 0 && p.ProductDiscount <= 9.99)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscount >= 10 && p.ProductDiscount <= 14.99)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscount >= 15)).ToList();
            }
            ServiceListView.ItemsSource = currentProduct;
            currentProduct = currentProduct.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            if(RButtonDown.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
            }
            if (RButtonUp.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderBy(p => p.ProductCost).ToList();
            }
            ServiceListView.ItemsSource = currentProduct;
            TableList = currentProduct;
            ChangPage();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        int CountRecords;
        int TotalRecords;
        List<Product> TableList; 
        private void ChangPage()
        {
            CountRecords = TableList.Count;
            TBCount.Text = "кол-во " + CountRecords + " из " + TotalRecords.ToString();
        }
    }
}