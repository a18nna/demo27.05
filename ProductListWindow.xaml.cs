using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace demo27._05
{
    /// <summary>
    /// Логика взаимодействия для ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        public ObservableCollection<ProductViewModel> Products { get; set; }

        public ProductListWindow()
        {
            InitializeComponent();
            LoadProducts();
            DataContext = this;
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

                Products = new ObservableCollection<ProductViewModel>(productsFromDb.Select(p => new ProductViewModel(p)));
            }
        }
    }
}
