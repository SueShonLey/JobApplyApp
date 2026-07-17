using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Model
{
    [Table("CompanyDetails")]
    public class CompanyDetails
    {

        /// <summary>
        ///  自增主键
        /// </summary>
        [Description("自增主键")]
        [FreeSql.DataAnnotations.Column(IsPrimary = true, IsIdentity = true, Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        ///  公司名称
        /// </summary>
        [Description("公司名称")]
        [Column("CompanyName")]
        public string CompanyName { get; set; } = string.Empty;

        [Column("Status")]
        public int? Status { get; set; }

        /// <summary>
        ///  求职阶段
        /// </summary>
        [Description("求职阶段")]
        [Column("Stage")]
        public int? Stage { get; set; }

        /// <summary>
        ///  初次投递时间
        /// </summary>
        [Description("初次投递时间")]
        [Column("FirstTime")]
        public DateTime? FirstTime { get; set; }

        /// <summary>
        ///  最新反馈时间
        /// </summary>
        [Description("最新反馈时间")]
        [Column("LatestTime")]
        public DateTime? LatestTime { get; set; }

        /// <summary>
        ///  期望薪资
        /// </summary>
        [Description("期望薪资")]
        [Column("ExpectedValue")]
        public int? ExpectedValue { get; set; }

        [Column("Salary")]
        public int? Salary { get; set; }

        /// <summary>
        ///  HR姓名
        /// </summary>
        [Description("HR姓名")]
        [Column("HRName")]
        public string HRName { get; set; } = string.Empty;

        /// <summary>
        ///  公司地址
        /// </summary>
        [Description("公司地址")]
        [Column("Address")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        ///  应聘岗位
        /// </summary>
        [Description("应聘岗位")]
        [Column("Position")]
        public string Position { get; set; } = string.Empty;

        /// <summary>
        ///  投递渠道
        /// </summary>
        [Description("投递渠道")]
        [Column("Channel")]
        public string Channel { get; set; } = string.Empty;

        /// <summary>
        ///  公司类型
        /// </summary>
        [Description("公司类型")]
        [Column("CompanyType")]
        public int? CompanyType { get; set; }

        /// <summary>
        ///  岗位要求
        /// </summary>
        [Description("岗位要求")]
        [Column("Requirement")]
        public string Requirement { get; set; } = string.Empty;

        /// <summary>
        ///  公司福利
        /// </summary>
        [Description("公司福利")]
        [Column("Welfare")]
        public string Welfare { get; set; } = string.Empty;

        /// <summary>
        ///  备注
        /// </summary>
        [Description("备注")]
        [Column("Remark")]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        ///  记录创建时间
        /// </summary>
        [Description("记录创建时间")]
        [FreeSql.DataAnnotations.Column(ServerTime = DateTimeKind.Local, CanUpdate = false, Name = "CreateTime")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        ///  记录更新时间
        /// </summary>
        [Description("记录更新时间")]
        [FreeSql.DataAnnotations.Column(ServerTime = DateTimeKind.Local, CanInsert = false, Name = "UpdateTime")]
        public DateTime? UpdateTime { get; set; }

    }

    public class CompanyDetailsDto
    {

        /// <summary>
        ///  自增主键
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///  公司名称
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        public string Status { get; set; }

        /// <summary>
        ///  求职阶段
        /// </summary>
        public string Stage { get; set; }

        /// <summary>
        ///  初次投递时间
        /// </summary>
        public DateTime? FirstTime { get; set; }

        /// <summary>
        ///  最新反馈时间
        /// </summary>
        public string LatestTime { get; set; }

        /// <summary>
        ///  期望薪资
        /// </summary>
        public int? ExpectedValue { get; set; }

        public int? Salary { get; set; }

        /// <summary>
        ///  HR姓名
        /// </summary>
        public string HRName { get; set; } = string.Empty;

        /// <summary>
        ///  公司地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        ///  应聘岗位
        /// </summary>
        public string Position { get; set; } = string.Empty;

        /// <summary>
        ///  投递渠道
        /// </summary>
        public string Channel { get; set; } = string.Empty;

        /// <summary>
        ///  公司类型
        /// </summary>
        public string CompanyType { get; set; }

        /// <summary>
        ///  岗位要求
        /// </summary>
        public string Requirement { get; set; } = string.Empty;

        /// <summary>
        ///  公司福利
        /// </summary>
        public string Welfare { get; set; } = string.Empty;

        /// <summary>
        ///  备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        ///  记录创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        ///  记录更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

    }

}
