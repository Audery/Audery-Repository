using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using the Compent Comspac
using System.IO;
using System.Text;
using System.Threading;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
namespace SOSOshop.BLL.Common
{  
    public class ICSharpZipHelper
    {
        /**/
        /// <summary>
        /// 实现压缩功能
        /// </summary>
        /// <param name="filenameToZip">要压缩文件(绝对文件路径)</param>
        /// <param name="Zipedfiledname">压缩(绝对文件路径)</param>
        /// <param name="CompressionLevel">压缩比</param>
        /// <param name="password">加密密码</param>
        /// <param name="comment">压缩文件描述</param>
        /// <returns>异常信息</returns>
        public static string MakeZipFile(string[] filenameToZip, string Zipedfiledname, int CompressionLevel,
            string password, string comment)
        {
            try
            {
                //使用正则表达式-判断压缩文件路径
                System.Text.RegularExpressions.Regex newRegex = new System.Text.
                    RegularExpressions.Regex(@"^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w   ]*.*))");
                if (!newRegex.Match(Zipedfiledname).Success)
                {
                    File.Delete(Zipedfiledname);
                    return "压缩文件的路径有误!";
                }
                //创建ZipFileOutPutStream
                ZipOutputStream newzipstream = new ZipOutputStream(File.Open(Zipedfiledname,
                    FileMode.OpenOrCreate));

                //判断Password
                if (password != null && password.Length > 0)
                {
                    newzipstream.Password = password;
                }
                if (comment != null && comment.Length > 0)
                {
                    newzipstream.SetComment(comment);
                }
                //设置CompressionLevel
                newzipstream.SetLevel(CompressionLevel); //-查看0 - means store only to 9 - means best compression 

                //执行压缩
                foreach (string filename in filenameToZip)
                {
                    FileStream newstream = File.OpenRead(filename);//打开预压缩文件
                    //判断路径
                    if (!newRegex.Match(Zipedfiledname).Success)
                    {
                        File.Delete(Zipedfiledname);
                        return "压缩文件目标路径不存在!";
                    }
                    byte[] setbuffer = new byte[newstream.Length];
                    newstream.Read(setbuffer, 0, setbuffer.Length);//读入文件
                    //新建ZipEntrity
                    ZipEntry newEntry = new ZipEntry(filename);
                    //设置时间-长度
                    newEntry.DateTime = DateTime.Now;
                    newEntry.Size = newstream.Length;
                    newstream.Close();
                    newzipstream.PutNextEntry(newEntry);//压入
                    newzipstream.Write(setbuffer, 0, setbuffer.Length);

                }
                //重复压入操作
                newzipstream.Finish();
                newzipstream.Close();

            }
            catch (Exception e)
            {
                //出现异常
                File.Delete(Zipedfiledname);
                return e.Message.ToString();
            }

            return "";
        }
        /**/
        /// <summary>
        /// 实现解压操作
        /// </summary>
        /// <param name="zipfilename">要解压文件Zip(物理路径)</param>
        /// <param name="UnZipDir">解压目的路径(物理路径)</param>
        /// <param name="password">解压密码</param>
        /// <returns>异常信息</returns>
        public static string UnMakeZipFile(string zipfilename, string UnZipDir, string password)
        {
            //判断待解压文件路径
            if (!File.Exists(zipfilename))
            {
                File.Delete(UnZipDir);
                return "待解压文件路径不存在!";
            }

            //创建ZipInputStream
            ZipInputStream newinStream = new ZipInputStream(File.OpenRead(zipfilename));

            //判断Password
            if (password != null && password.Length > 0)
            {
                newinStream.Password = password;
            }
            //执行解压操作
            try
            {
                ZipEntry theEntry;
                //获取Zip中单个File
                while ((theEntry = newinStream.GetNextEntry()) != null)
                {
                    //判断目的路径
                    if (Directory.Exists(UnZipDir))
                    {
                        Directory.CreateDirectory(UnZipDir);//创建目的目录
                    }
                    //获得目的目录信息
                    string Driectoryname = Path.GetDirectoryName(UnZipDir);
                    string pathname = Path.GetDirectoryName(theEntry.Name);//获得子级目录
                    string filename = Path.GetFileName(theEntry.Name);//获得子集文件名
                    //处理文件盘符问题
                    pathname = pathname.Replace(":", "$");//处理当前压缩出现盘符问题
                    Driectoryname = Driectoryname + "\\" + pathname;
                    //创建
                    Directory.CreateDirectory(Driectoryname);
                    //解压指定子目录
                    if (filename != string.Empty)
                    {
                        FileStream newstream = File.Create(Driectoryname + "\\" + pathname);
                        int size = 2048;
                        byte[] newbyte = new byte[size];
                        while (true)
                        {
                            size = newinStream.Read(newbyte, 0, newbyte.Length);
                            if (size > 0)
                            {
                                //写入数据
                                newstream.Write(newbyte, 0, size);
                            }
                            else
                            {
                                break;
                            }
                            newstream.Close();
                        }
                    }
                }
                newinStream.Close();
            }
            catch (Exception se)
            {
                return se.Message.ToString();
            }
            finally
            {
                newinStream.Close();
            }

            return "";
        }
    }
}
