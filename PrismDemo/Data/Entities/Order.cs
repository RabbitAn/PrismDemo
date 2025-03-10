using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrismDemo.Data.Entities
{
    /// <summary>
    /// 订单实体类
    /// 演示EF Core的一对多关系
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 订单ID，主键
        /// </summary>
        [Key]
        public int OrderId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        // MySQL使用HasPrecision在Fluent API中配置
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [MaxLength(20)]
        public string Status { get; set; }

        /// <summary>
        /// 客户ID，外键
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 导航属性，关联的客户
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
} 