using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 实体类ThumbnailsSetting 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ThumbnailsSetting
    {
        public ThumbnailsSetting()
        { }
        #region Model
        private int _id;
        private string _thumbnailswidth;
        private string _thumbnailsheight;
        private string _imagewidth;
        private string _imageheight;
        private string _type;
        private string _watermarkpicturepath;
        private string _characters;
        private string _imgtransparent;
        private int? _chartransparent;
        private string _watermarkposition;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailsWidth
        {
            set { _thumbnailswidth = value; }
            get { return _thumbnailswidth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailsHeight
        {
            set { _thumbnailsheight = value; }
            get { return _thumbnailsheight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImageWidth
        {
            set { _imagewidth = value; }
            get { return _imagewidth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImageHeight
        {
            set { _imageheight = value; }
            get { return _imageheight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WatermarkPicturePath
        {
            set { _watermarkpicturepath = value; }
            get { return _watermarkpicturepath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Characters
        {
            set { _characters = value; }
            get { return _characters; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImgTransparent
        {
            set { _imgtransparent = value; }
            get { return _imgtransparent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CharTransparent
        {
            set { _chartransparent = value; }
            get { return _chartransparent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WatermarkPosition
        {
            set { _watermarkposition = value; }
            get { return _watermarkposition; }
        }
        #endregion Model

    }
}
