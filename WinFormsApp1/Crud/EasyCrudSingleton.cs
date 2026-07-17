using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Crud
{
    public class EasyCrudSingleton
    {
        static string sqliteSql = @$"CREATE TABLE IF NOT EXISTS CompanyDetails (
    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    CompanyName TEXT NOT NULL,
    Status INTEGER NULL,
    Stage INTEGER NULL,
    FirstTime TEXT NULL,
    LatestTime TEXT NULL,
    ExpectedValue INTEGER NULL,
    Salary INTEGER NULL,
    HRName TEXT NULL,
    Address TEXT NULL,
    Position TEXT NOT NULL,
    Channel TEXT NULL,
    CompanyType INTEGER NULL,
    Requirement TEXT NULL,
    Welfare TEXT NULL,
    Remark TEXT NULL,
    CreateTime TEXT NULL,
    UpdateTime TEXT NULL
);";


        // 使用 Lazy<T> 来保证延迟初始化和线程安全
        //Lazy<T> 通过内部的机制确保在多线程环境下的安全性，而且在首次访问时才会初始化对象实例，从而避免了重复创建和资源浪费。
        private static readonly Lazy<EasyCrud> lazyInstance = new Lazy<EasyCrud>(() =>
        {
            // sqlserver配置实例
            //return new EasyCrud(conn: "Data Source=localhost;Initial Catalog=CompanyInfo;Integrated Security=True;TrustServerCertificate=True");



            // sqllite配置实例
            return new EasyCrud( FreeSql.DataType.Sqlite, InitializeSQL: sqliteSql);
        });

        // 获取单例
        public static EasyCrud Instance => lazyInstance.Value;

        // 私有构造函数防止初始化
        private EasyCrudSingleton()
        {

        }
    }
}
