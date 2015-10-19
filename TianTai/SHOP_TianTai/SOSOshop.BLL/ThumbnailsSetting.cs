using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类ThumbnailsSetting。
    /// </summary>
    public class ThumbnailsSetting : Db
    {
        public ThumbnailsSetting()
        { }
        #region  成员方法

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.ThumbnailsSetting model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update yxs_ThumbnailsSetting set ");
            strSql.Append("ThumbnailsWidth=@ThumbnailsWidth,");
            strSql.Append("ThumbnailsHeight=@ThumbnailsHeight,");
            strSql.Append("ImageWidth=@ImageWidth,");
            strSql.Append("ImageHeight=@ImageHeight,");
            strSql.Append("Type=@Type,");
            strSql.Append("WatermarkPicturePath=@WatermarkPicturePath,");
            strSql.Append("Characters=@Characters,");
            strSql.Append("ImgTransparent=@ImgTransparent,");
            strSql.Append("CharTransparent=@CharTransparent,");
            strSql.Append("WatermarkPosition=@WatermarkPosition");
            strSql.Append(" where Id=@Id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Id", DbType.Int32, model.Id);
            db.AddInParameter(dbCommand, "ThumbnailsWidth", DbType.AnsiString, model.ThumbnailsWidth);
            db.AddInParameter(dbCommand, "ThumbnailsHeight", DbType.AnsiString, model.ThumbnailsHeight);
            db.AddInParameter(dbCommand, "ImageWidth", DbType.AnsiString, model.ImageWidth);
            db.AddInParameter(dbCommand, "ImageHeight", DbType.AnsiString, model.ImageHeight);
            db.AddInParameter(dbCommand, "Type", DbType.AnsiString, model.Type);
            db.AddInParameter(dbCommand, "WatermarkPicturePath", DbType.AnsiString, model.WatermarkPicturePath);
            db.AddInParameter(dbCommand, "Characters", DbType.AnsiString, model.Characters);
            db.AddInParameter(dbCommand, "ImgTransparent", DbType.AnsiString, model.ImgTransparent);
            db.AddInParameter(dbCommand, "CharTransparent", DbType.Int32, model.CharTransparent);
            db.AddInParameter(dbCommand, "WatermarkPosition", DbType.AnsiString, model.WatermarkPosition);

            return 0 < db.ExecuteNonQuery(dbCommand);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ThumbnailsSetting GetModel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,ThumbnailsWidth,ThumbnailsHeight,ImageWidth,ImageHeight,Type,WatermarkPicturePath,Characters,ImgTransparent,CharTransparent,WatermarkPosition from yxs_ThumbnailsSetting ");

            Model.ThumbnailsSetting model = new Model.ThumbnailsSetting();
            DataSet ds = ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.ThumbnailsWidth = ds.Tables[0].Rows[0]["ThumbnailsWidth"].ToString();
                model.ThumbnailsHeight = ds.Tables[0].Rows[0]["ThumbnailsHeight"].ToString();
                model.ImageWidth = ds.Tables[0].Rows[0]["ImageWidth"].ToString();
                model.ImageHeight = ds.Tables[0].Rows[0]["ImageHeight"].ToString();
                model.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                model.WatermarkPicturePath = ds.Tables[0].Rows[0]["WatermarkPicturePath"].ToString();
                model.Characters = ds.Tables[0].Rows[0]["Characters"].ToString();
                model.ImgTransparent = ds.Tables[0].Rows[0]["ImgTransparent"].ToString();
                if (ds.Tables[0].Rows[0]["CharTransparent"].ToString() != "")
                {
                    model.CharTransparent = int.Parse(ds.Tables[0].Rows[0]["CharTransparent"].ToString());
                }
                model.WatermarkPosition = ds.Tables[0].Rows[0]["WatermarkPosition"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion  成员方法
    }
}
