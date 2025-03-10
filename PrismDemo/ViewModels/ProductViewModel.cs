using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismDemo.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace PrismDemo.ViewModels
{
    public class ProductViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private Product _selectedProduct;
        private bool _isProductSelected;

        public ObservableCollection<Product> Products { get; private set; }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                SetProperty(ref _selectedProduct, value);
                IsProductSelected = value != null;
            }
        }

        public bool IsProductSelected
        {
            get => _isProductSelected;
            set => SetProperty(ref _isProductSelected, value);
        }

        public DelegateCommand<Product> ViewDetailsCommand { get; }

        public ProductViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            ViewDetailsCommand = new DelegateCommand<Product>(ViewProductDetails);
            
            // 初始化示例产品数据
            InitializeProducts();
        }

        private void ViewProductDetails(Product product)
        {
            if (product == null) return;

            var parameters = new DialogParameters
            {
                { "title", $"产品详情 - {product.Name}" },
                { "message", $"产品: {product.Name}\n描述: {product.Description}\n价格: ¥{product.Price}" }
            };

            _dialogService.ShowDialog("MessageDialog", parameters, _ => { });
        }

        private void InitializeProducts()
        {
            Products = new ObservableCollection<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "高级笔记本电脑",
                    Description = "16GB内存, 512GB SSD, 15.6寸显示屏",
                    Price = 6999.00M,
                    Color = new SolidColorBrush(Colors.Blue)
                },
                new Product
                {
                    Id = 2,
                    Name = "智能手机",
                    Description = "6.7寸OLED屏幕, 256GB存储空间, 双卡双待",
                    Price = 5999.00M,
                    Color = new SolidColorBrush(Colors.Green)
                },
                new Product
                {
                    Id = 3,
                    Name = "平板电脑",
                    Description = "10.9寸屏幕, 128GB存储空间, Wi-Fi + 蜂窝数据",
                    Price = 3599.00M,
                    Color = new SolidColorBrush(Colors.Orange)
                },
                new Product
                {
                    Id = 4,
                    Name = "智能手表",
                    Description = "GPS, 心率监测, 血氧监测, 防水",
                    Price = 2199.00M,
                    Color = new SolidColorBrush(Colors.Red)
                },
                new Product
                {
                    Id = 5,
                    Name = "无线耳机",
                    Description = "主动降噪, 长达24小时续航, 高品质音效",
                    Price = 1299.00M,
                    Color = new SolidColorBrush(Colors.Purple)
                }
            };
        }
    }
} 