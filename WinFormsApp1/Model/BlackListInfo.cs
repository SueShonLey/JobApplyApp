using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Model
{
    [Table("BlackListInfo")]
    public class BlackListInfo
    {

        [FreeSql.DataAnnotations.Column(IsPrimary = true, IsIdentity = true, Name = "ID")]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; } = string.Empty;

        [Column("AvoidCount")]
        public int? AvoidCount { get; set; }

        [Column("AvoidReason")]
        public string AvoidReason { get; set; } = string.Empty;

        [Column("RelatedLink")]
        public string RelatedLink { get; set; } = string.Empty;

        [Column("Remark")]
        public string Remark { get; set; } = string.Empty;

    }
}
