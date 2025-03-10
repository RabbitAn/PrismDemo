using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Mvvm;
using PrismDemo.Data;
using PrismDemo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PrismDemo.ViewModels
{
    /// <summary>
    /// 数据访问演示的ViewModel
    /// 展示如何在Prism应用中使用EF Core进行数据访问操作
    /// </summary>
    public class DataAccessDemoViewModel : BindableBase
    {
        private readonly Func<AppDbContext> _dbContextFactory;
        private readonly ILogger<DataAccessDemoViewModel> _logger;
        private bool _isLoading;
        private string _statusMessage;
        private ObservableCollection<Customer> _customers;
        private ObservableCollection<Product> _products;
        private string _customerSearchText;
        private bool _showOnlyActiveCustomers;
        private string _selectedProductCategory;
        private List<string> _productCategories;
        private bool _showOnlyAvailableProducts;
        private Customer _selectedCustomer;
        private Product _selectedProduct;
        private object _selectedItemDetails;
        private bool _isItemSelected;

        /// <summary>
        /// 是否正在加载数据
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        /// <summary>
        /// 状态消息
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        /// <summary>
        /// 产品列表
        /// </summary>
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        /// <summary>
        /// 客户搜索文本
        /// </summary>
        public string CustomerSearchText
        {
            get => _customerSearchText;
            set => SetProperty(ref _customerSearchText, value);
        }

        /// <summary>
        /// 是否只显示活跃客户
        /// </summary>
        public bool ShowOnlyActiveCustomers
        {
            get => _showOnlyActiveCustomers;
            set
            {
                if (SetProperty(ref _showOnlyActiveCustomers, value))
                {
                    LoadCustomersAsync();
                }
            }
        }

        /// <summary>
        /// 所有产品类别
        /// </summary>
        public List<string> ProductCategories
        {
            get => _productCategories;
            set => SetProperty(ref _productCategories, value);
        }

        /// <summary>
        /// 选择的产品类别
        /// </summary>
        public string SelectedProductCategory
        {
            get => _selectedProductCategory;
            set => SetProperty(ref _selectedProductCategory, value);
        }

        /// <summary>
        /// 是否只显示有库存的产品
        /// </summary>
        public bool ShowOnlyAvailableProducts
        {
            get => _showOnlyAvailableProducts;
            set
            {
                if (SetProperty(ref _showOnlyAvailableProducts, value))
                {
                    LoadProductsAsync();
                }
            }
        }

        /// <summary>
        /// 选择的客户
        /// </summary>
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (SetProperty(ref _selectedCustomer, value))
                {
                    SelectedProduct = null;
                    UpdateSelectedItemDetails();
                }
            }
        }

        /// <summary>
        /// 选择的产品
        /// </summary>
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (SetProperty(ref _selectedProduct, value))
                {
                    SelectedCustomer = null;
                    UpdateSelectedItemDetails();
                }
            }
        }

        /// <summary>
        /// 选择项的详细信息
        /// </summary>
        public object SelectedItemDetails
        {
            get => _selectedItemDetails;
            set => SetProperty(ref _selectedItemDetails, value);
        }

        /// <summary>
        /// 是否选择了项目
        /// </summary>
        public bool IsItemSelected
        {
            get => _isItemSelected;
            set => SetProperty(ref _isItemSelected, value);
        }

        /// <summary>
        /// 搜索客户命令
        /// </summary>
        public DelegateCommand SearchCustomersCommand { get; }

        /// <summary>
        /// 加载所有客户命令
        /// </summary>
        public DelegateCommand LoadAllCustomersCommand { get; }

        /// <summary>
        /// 按类别筛选产品命令
        /// </summary>
        public DelegateCommand FilterProductsByCategoryCommand { get; }

        /// <summary>
        /// 加载所有产品命令
        /// </summary>
        public DelegateCommand LoadAllProductsCommand { get; }

        /// <summary>
        /// 构造函数，注入依赖服务
        /// </summary>
        /// <param name="dbContextFactory">数据库上下文工厂</param>
        /// <param name="logger">日志记录器</param>
        public DataAccessDemoViewModel(Func<AppDbContext> dbContextFactory, ILogger<DataAccessDemoViewModel> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;

            Customers = new ObservableCollection<Customer>();
            Products = new ObservableCollection<Product>();
            ProductCategories = new List<string>();

            SearchCustomersCommand = new DelegateCommand(SearchCustomersAsync);
            LoadAllCustomersCommand = new DelegateCommand(LoadCustomersAsync);
            FilterProductsByCategoryCommand = new DelegateCommand(FilterProductsByCategoryAsync);
            LoadAllProductsCommand = new DelegateCommand(LoadProductsAsync);

            // 初始化数据
            InitializeAsync();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private async void InitializeAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "正在初始化数据...";

                LoadCustomersAsync();
                LoadProductsAsync();
                await LoadProductCategoriesAsync();

                StatusMessage = "数据初始化完成";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化数据时发生错误");
                StatusMessage = $"初始化数据失败: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 加载客户数据
        /// </summary>
        private async void LoadCustomersAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "正在加载客户数据...";

                using (var context = _dbContextFactory())
                {
                    var query = context.Customers.AsQueryable();
                    if (ShowOnlyActiveCustomers)
                    {
                        query = query.Where(c => c.IsActive);
                    }

                    var customers = await query.ToListAsync();
                    Customers.Clear();
                    foreach (var customer in customers)
                    {
                        Customers.Add(customer);
                    }
                }

                StatusMessage = $"成功加载 {Customers.Count} 个客户";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载客户数据时发生错误");
                StatusMessage = $"加载客户数据失败: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 搜索客户
        /// </summary>
        private async void SearchCustomersAsync()
        {
            if (string.IsNullOrWhiteSpace(CustomerSearchText))
            {
                LoadCustomersAsync();
                return;
            }

            try
            {
                IsLoading = true;
                StatusMessage = "正在搜索客户...";

                using (var context = _dbContextFactory())
                {
                    var searchText = CustomerSearchText.ToLower();
                    var customers = await context.Customers
                        .Where(c => c.Name.ToLower().Contains(searchText) ||
                                  c.Email.ToLower().Contains(searchText) ||
                                  c.Phone.Contains(searchText) ||
                                  c.Address.ToLower().Contains(searchText))
                        .ToListAsync();

                    Customers.Clear();
                    foreach (var customer in customers)
                    {
                        Customers.Add(customer);
                    }
                }

                StatusMessage = $"搜索到 {Customers.Count} 个客户";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "搜索客户时发生错误");
                StatusMessage = $"搜索客户失败: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 加载产品数据
        /// </summary>
        private async void LoadProductsAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "正在加载产品数据...";

                using (var context = _dbContextFactory())
                {
                    var query = context.Products.AsQueryable();
                    if (ShowOnlyAvailableProducts)
                    {
                        query = query.Where(p => p.IsAvailable);
                    }

                    var products = await query.ToListAsync();
                    Products.Clear();
                    foreach (var product in products)
                    {
                        Products.Add(product);
                    }
                }

                StatusMessage = $"成功加载 {Products.Count} 个产品";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载产品数据时发生错误");
                StatusMessage = $"加载产品数据失败: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 加载产品类别
        /// </summary>
        private async Task LoadProductCategoriesAsync()
        {
            try
            {
                using (var context = _dbContextFactory())
                {
                    var categories = await context.Products
                        .Select(p => p.Category)
                        .Distinct()
                        .ToListAsync();

                    ProductCategories = new List<string>(categories);
                    ProductCategories.Insert(0, "所有类别");
                    SelectedProductCategory = ProductCategories[0];
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载产品类别时发生错误");
                StatusMessage = $"加载产品类别失败: {ex.Message}";
            }
        }

        /// <summary>
        /// 按类别筛选产品
        /// </summary>
        private async void FilterProductsByCategoryAsync()
        {
            if (SelectedProductCategory == null || SelectedProductCategory == "所有类别")
            {
                LoadProductsAsync();
                return;
            }

            try
            {
                IsLoading = true;
                StatusMessage = "正在筛选产品...";

                using (var context = _dbContextFactory())
                {
                    var products = await context.Products
                        .Where(p => p.Category == SelectedProductCategory)
                        .ToListAsync();

                    Products.Clear();
                    foreach (var product in products)
                    {
                        Products.Add(product);
                    }
                }

                StatusMessage = $"找到 {Products.Count} 个{SelectedProductCategory}类别的产品";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "筛选产品时发生错误");
                StatusMessage = $"筛选产品失败: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 更新选择项的详细信息
        /// </summary>
        private async void UpdateSelectedItemDetails()
        {
            try
            {
                if (SelectedCustomer != null)
                {
                    IsLoading = true;
                    StatusMessage = "正在加载客户详情...";

                    using (var context = _dbContextFactory())
                    {
                        // 加载包含订单的客户信息
                        var customer = await context.Customers
                            .Include(c => c.Orders)
                            .FirstOrDefaultAsync(c => c.CustomerId == SelectedCustomer.CustomerId);

                        if (customer != null)
                        {
                            var details = new
                            {
                                customer.CustomerId,
                                customer.Name,
                                customer.Email,
                                customer.Phone,
                                customer.Address,
                                CreatedDate = customer.CreatedDate.ToString("yyyy-MM-dd"),
                                IsActive = customer.IsActive ? "是" : "否",
                                OrderCount = customer.Orders?.Count ?? 0,
                                TotalOrders = customer.Orders?.Sum(o => o.TotalAmount) ?? 0
                            };

                            SelectedItemDetails = details;
                            IsItemSelected = true;
                            StatusMessage = "已加载客户详情";
                        }
                    }
                }
                else if (SelectedProduct != null)
                {
                    var details = new
                    {
                        SelectedProduct.ProductId,
                        SelectedProduct.Name,
                        SelectedProduct.Description,
                        Price = $"￥{SelectedProduct.Price:N2}",
                        SelectedProduct.Category,
                        SelectedProduct.StockQuantity,
                        Status = SelectedProduct.IsAvailable ? "上架中" : "已下架"
                    };

                    SelectedItemDetails = details;
                    IsItemSelected = true;
                    StatusMessage = "已加载产品详情";
                }
                else
                {
                    SelectedItemDetails = null;
                    IsItemSelected = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新选择项详情时发生错误");
                StatusMessage = $"加载详情失败: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
} 