using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace demo27._05
{
    /// <summary>
    /// Логика взаимодействия для ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private User _currentUser;
        public ObservableCollection<ProductViewModel> Products { get; set; }
        private List<ProductViewModel> _allProducts;
        private int _currentSortedIndex = -1;
        private int _currentSupplierFilterIndex = -1;

        public ProductListWindow(User user)
        {
            InitializeComponent();
            LoadProducts();
            InitializeUser(user);
            LoadSupplierFilter();
            DataContext = this;
        }

        public void InitializeUser(User user)
        {
            _currentUser = user;
            UserNameTextBlock.Text = _currentUser?.full_name;
        }

        private void LoadProducts()
        {
            using (var context = new DemoContext())
            {
                var productsFromDb = context.products
                    .Include(p => p.Label)
                    .Include(p => p.Category)
                    .Include(p => p.Fabric)
                    .Include(p => p.Supplier)
                    .ToList();

                _allProducts = productsFromDb.Select(p => new ProductViewModel(p)).ToList();
                Products = new ObservableCollection<ProductViewModel>(_allProducts);
            }
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentSortedIndex = SortComboBox.SelectedIndex;
            ApplySortAndFilter();

        }

        private void UpdateProductsCollection(List<ProductViewModel> sortedList)
        {
            Products.Clear();
            foreach (var product in sortedList)
            {
                Products.Add(product);
            }
        }

        private void ExitButtonClick_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _currentSortedIndex = SortComboBox.SelectedIndex;
            ApplySortAndFilter();
        }

        private void ApplySortAndFilter()
        {
            var sortedList = _allProducts.ToList();

            /*if (_currentSupplierFilterIndex > 0) // Если выбран не "Все поставщики"
            {
                var selectedSupplierId = ((ComboBoxItem)SupplierFilterComboBox.SelectedItem).Tag;
                if (selectedSupplierId is int supplierId)
                {
                    sortedList = sortedList.Where(p => p. == supplierId).ToList();
                }
            }*/

            // Применяем сортировку
            if (_currentSortedIndex == 0)
            {
                sortedList = sortedList.OrderBy(p => p.Quantity).ToList();
            }
            else if (_currentSortedIndex == 1)
            {
                sortedList = sortedList.OrderByDescending(p => p.Quantity).ToList();
            }

            // Затем применяем фильтр поиска
            var searchText = SearchTextBox.Text.ToLower().Trim();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                sortedList = sortedList.Where(p =>
                    (p.Name?.ToLower().Contains(searchText) == true) ||
                    (p.Category?.ToLower().Contains(searchText) == true) ||
                    (p.Description?.ToLower().Contains(searchText) == true) ||
                    (p.Manufacture?.ToLower().Contains(searchText) == true) ||
                    (p.Supplier?.ToLower().Contains(searchText) == true) ||
                    (p.Unit?.ToLower().Contains(searchText) == true)
                ).ToList();
            }

            UpdateProductsCollection(sortedList);
        }

        private void SupplierFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentSupplierFilterIndex = SupplierFilterComboBox.SelectedIndex;
            ApplySortAndFilter();

        }

        private void LoadSupplierFilter()
        {
            using (var context = new DemoContext())
            {
                var supplierFromDb = context.suppliers.Select(s => new { s.supplier_id, s.supplier1 }).OrderBy(s => s.supplier1).ToList();

                SupplierFilterComboBox.Items.Clear();
                SupplierFilterComboBox.Items.Add(new ComboBoxItem { Content = "Все поставщики", Tag = -1 });
                try
                {
                    foreach (var supplier in supplierFromDb)
                        SupplierFilterComboBox.Items.Add(new ComboBoxItem { Content = supplier.supplier1, Tag = supplier.supplier_id });
                }
                catch (Exception ex)
                {
                    {
                        MessageBox.Show($"Не удалось загрузить список поставщиков.\t{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}