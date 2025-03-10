using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PrismDemo.Data.Entities;
using System;
using System.IO;

namespace PrismDemo.Data
{
    /// <summary>
    /// 应用数据库上下文类
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">数据库上下文选项</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 客户实体集合
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// 订单实体集合
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// 产品实体集合
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// 模型创建配置
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置客户和订单的一对多关系
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // 为Product实体添加索引
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);

            // MySQL的decimal类型配置
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // 添加种子数据
            SeedData(modelBuilder);
        }

        /// <summary>
        /// 添加种子数据
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // 添加示例客户数据
            modelBuilder.Entity<Customer>().HasData(
                new Customer 
                { 
                    CustomerId = 1, 
                    Name = "张三", 
                    Email = "zhangsan@example.com", 
                    Phone = "13800138001", 
                    Address = "北京市海淀区", 
                    CreatedDate = new DateTime(2023, 1, 15), 
                    IsActive = true 
                },
                new Customer 
                { 
                    CustomerId = 2, 
                    Name = "李四", 
                    Email = "lisi@example.com", 
                    Phone = "13900139002", 
                    Address = "上海市浦东新区", 
                    CreatedDate = new DateTime(2023, 2, 20), 
                    IsActive = true 
                },
                new Customer 
                { 
                    CustomerId = 3, 
                    Name = "王五", 
                    Email = "wangwu@example.com", 
                    Phone = "13700137003", 
                    Address = "广州市天河区", 
                    CreatedDate = new DateTime(2023, 3, 10), 
                    IsActive = false 
                }
            );

            // 添加示例产品数据
            modelBuilder.Entity<Product>().HasData(
                new Product 
                { 
                    ProductId = 1, 
                    Name = "笔记本电脑", 
                    Description = "高性能商务笔记本电脑", 
                    Price = 6999.00M, 
                    StockQuantity = 50, 
                    Category = "电子产品", 
                    IsAvailable = true 
                },
                new Product 
                { 
                    ProductId = 2, 
                    Name = "智能手机", 
                    Description = "新一代智能手机", 
                    Price = 4999.00M, 
                    StockQuantity = 100, 
                    Category = "电子产品", 
                    IsAvailable = true 
                },
                new Product 
                { 
                    ProductId = 3, 
                    Name = "无线耳机", 
                    Description = "降噪无线蓝牙耳机", 
                    Price = 999.00M, 
                    StockQuantity = 200, 
                    Category = "配件", 
                    IsAvailable = true 
                },
                new Product 
                { 
                    ProductId = 4, 
                    Name = "智能手表", 
                    Description = "健康监测智能手表", 
                    Price = 1599.00M, 
                    StockQuantity = 80, 
                    Category = "穿戴设备", 
                    IsAvailable = true 
                }
            );

            // 添加示例订单数据
            modelBuilder.Entity<Order>().HasData(
                new Order 
                { 
                    OrderId = 1, 
                    OrderNumber = "ORD20230001", 
                    OrderDate = new DateTime(2023, 4, 5), 
                    TotalAmount = 6999.00M, 
                    Status = "已完成", 
                    CustomerId = 1 
                },
                new Order 
                { 
                    OrderId = 2, 
                    OrderNumber = "ORD20230002", 
                    OrderDate = new DateTime(2023, 4, 10), 
                    TotalAmount = 5998.00M, 
                    Status = "处理中", 
                    CustomerId = 2 
                },
                new Order 
                { 
                    OrderId = 3, 
                    OrderNumber = "ORD20230003", 
                    OrderDate = new DateTime(2023, 4, 15), 
                    TotalAmount = 2598.00M, 
                    Status = "已发货", 
                    CustomerId = 1 
                }
            );
        }
    }

    /// <summary>
    /// 设计时 DbContext 工厂
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // 构建配置
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // 创建 DbContext 选项
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("PrismDemoDb");
            var serverVersion = new MySqlServerVersion(new Version(6, 0, 3));

            optionsBuilder.UseMySql(connectionString, serverVersion, mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });

            return new AppDbContext(optionsBuilder.Options);
        }
    }
} 