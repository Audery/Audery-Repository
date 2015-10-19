using System.Data.Linq;

namespace Note
{
    public class NoteDataContext : DataContext
    {
        // 数据库链接字符串
        public static string DBConnectionString = "Data Source=isostore:/Note.sdf";

        // 传递数据库连接字符串到DataContext基类
        public NoteDataContext(string connectionString)
            : base(connectionString)
        { }

        // 定义一个员工信息表
        public Table<NoteTable> Notes;
    }
}
