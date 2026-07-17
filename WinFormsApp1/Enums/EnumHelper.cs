using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Enums
{
    public class EnumHelper
    {
        /// <summary>
        /// 枚举-投递阶段(0:已投递,1:评估中,2:已面试,3:已有结果)
        /// </summary>
        [Description("投递阶段")]
        public enum EnumStage
        {
            /// <summary>
            ///已投递
            /// </summary>
            [Description("已投递")]
            Delivered = 0,
            /// <summary>
            ///评估中
            /// </summary>
            [Description("评估中")]
            Evaluating = 1,
            /// <summary>
            ///已面试
            /// </summary>
            [Description("已面试")]
            Interviewed = 2,
            /// <summary>
            ///已有结果
            /// </summary>
            [Description("已有结果")]
            ResultsAlreadyAvailable = 3,
        }


        /// <summary>
        /// 枚举-公司类型(0:国企,1:私企,2:外企)
        /// </summary>
        [Description("公司类型")]
        public enum EnumCompanyType
        {
            /// <summary>
            ///国企
            /// </summary>
            [Description("国企")]
            StateownedEnterprise = 0,
            /// <summary>
            ///私企
            /// </summary>
            [Description("私企")]
            PrivateEnterprise = 1,
            /// <summary>
            ///外企
            /// </summary>
            [Description("外企")]
            ForeignCompany = 2,
        }


        public class EnumDetails<T> where T : Enum
        {
            /// <summary>
            /// 枚举索引
            /// </summary>
            public int Index { get; set; }

            /// <summary>
            /// 枚举值（字符串）
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 枚举值
            /// </summary>
            public Enum EnumData { get; set; }

            /// <summary>
            /// 枚举描述
            /// </summary>

            public string Description { get; set; }
        }

        public static List<EnumDetails<T>> GetEnumDetails<T>() where T : Enum
        {
            Type enumType = typeof(T);
            var list = enumType.GetFields().Where(x => !x.IsSpecialName).ToList();
            var res = new List<EnumDetails<T>>();
            foreach (var item in list)
            {
                var entity = new EnumDetails<T>()
                {
                    Name = item.Name,
                    Index = (int)item.GetValue(null),
                    Description = enumType.GetField(item.Name).GetCustomAttribute<DescriptionAttribute>()?.Description ?? "无注释",
                    EnumData = (T)item.GetValue(null)
                };
                res.Add(entity);
            }
            return res;
        }
    }


}
