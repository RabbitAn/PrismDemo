using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrismDemo.Data.Entities
{
    /// <summary>
    /// 客户实体类
    /// 演示EF Core的实体模型
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 客户ID，主键
        /// </summary>
        [Key]
        public int CustomerId { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 联系邮箱
        /// </summary>
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(200)]
        public string Address { get; set; }

        /// <summary>
        /// 客户创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 是否活跃客户
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 客户的订单集合，一对多关系
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; }
    }
} 