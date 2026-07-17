using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Crud
{
    public class EasyCrudSingleton
    {
        // 使用 Lazy<T> 来保证延迟初始化和线程安全
        //Lazy<T> 通过内部的机制确保在多线程环境下的安全性，而且在首次访问时才会初始化对象实例，从而避免了重复创建和资源浪费。
        private static readonly Lazy<EasyCrud> lazyInstance = new Lazy<EasyCrud>(() =>
        {
            // 配置实例
            return new EasyCrud(conn: "Data Source=localhost;Initial Catalog=CompanyInfo;Integrated Security=True;TrustServerCertificate=True");
        });

        // 获取单例
        public static EasyCrud Instance => lazyInstance.Value;

        // 私有构造函数防止初始化
        private EasyCrudSingleton()
        {

        }
    }
}
