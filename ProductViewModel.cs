using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace demo27._05
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly Product _product;

        public ProductViewModel(Product product)
        {
            _product = product;
        }

        public string Description => _product.description;
        public decimal Price => _product.price;
        public string Unit => _product.unit;
        public int Quantity => _product.amount;
        public int Discount => _product.discount;

        public string Name => _product.Label?.label1 ?? "-"; //Наименование
        public string Category => _product.Category?.category1 ?? "-";
        public string Manufacture => _product.Fabric?.fabric1 ?? "-";
        public string Supplier => _product.Supplier?.supplier1 ?? "-";

        public string ImagePath => "\\Images\\" + (_product.photo ?? "picture.png");

        

        // Вычисляемые свойства для отображения цены
        public bool IsDiscounted => Discount > 0;
        public decimal FinalPrice => Price * (1 - Discount / 100m);

        public string OldPriceString => $"{Price:F2} руб.";
        public string FinalPriceString => $"{FinalPrice:F2} руб.";

        public bool HasStock => Quantity > 0;

        // Фон строки в зависимости от размера скидки
        public Brush RowBackground
        {
            get
            {
                if (Quantity <= 0)
                    return Brushes.LightBlue;
                if (Quantity > 15)
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#2E8B57");
                return Brushes.Transparent;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
