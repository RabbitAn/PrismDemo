using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrismDemo.Data.Entities
{
    /// <summary>
    /// 产品实体类
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 产品ID，主键
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        // MySQL使用HasPrecision在Fluent API中配置
        public decimal Price { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// 产品类别
        /// </summary>
        [MaxLength(50)]
        public string Category { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public bool IsAvailable { get; set; }
    }
} 