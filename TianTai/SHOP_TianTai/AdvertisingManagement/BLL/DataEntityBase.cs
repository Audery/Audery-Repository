using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Xml;

namespace AdvertisingManagement.BLL
{
    [Serializable]
    public abstract class DataEntityBase : IDisposable
    {
        private bool _IsChanged;
        private bool _IsEmpty = true;
        private int _ReturnValue;

        public void Dispose()
        {
        }

        public DataRow GetMetaDataRow()
        {
            DataTable schema = this.GetSchema();
            DataRow row = schema.NewRow();
            foreach (DataColumn column in schema.Columns)
            {
                string columnName = column.ColumnName;
                row[columnName] = base.GetType().GetProperty(columnName).GetValue(this, null);
            }
            return row;
        }

        public DataTable GetMetaDataTable()
        {
            DataTable schema = this.GetSchema();
            DataRow row = schema.NewRow();
            foreach (DataColumn column in schema.Columns)
            {
                string columnName = column.ColumnName;
                row[columnName] = base.GetType().GetProperty(columnName).GetValue(this, null);
            }
            schema.Rows.Add(row);
            return schema;
        }

        public DataTable GetSchema()
        {
            DataTable table = new DataTable(base.GetType().Name);
            foreach (PropertyInfo info in base.GetType().GetProperties())
            {
                string typeName = info.PropertyType.ToString();
                if (((!(info.Name == "IsEmpty") && !(info.Name == "MetaDataTable")) && (!(info.Name == "MetaDataRow") && !(info.Name == "ReturnValue"))) && ((!(info.Name == "Schema") && !(info.Name == "XmlDom")) && !(info.Name == "IsChanged")))
                {
                    table.Columns.Add(info.Name, Type.GetType(typeName));
                    table.Columns[info.Name].AllowDBNull = true;
                }
            }
            return table;
        }

        protected void InitMetaData()
        {
            foreach (PropertyInfo info in base.GetType().GetProperties())
            {
                string str2 = info.PropertyType.ToString();
                if (str2 != null)
                {
                    if (!(str2 == "System.String"))
                    {
                        if (str2 == "System.DateTime")
                        {
                            goto Label_0061;
                        }
                        if (str2 == "System.Byte[]")
                        {
                            goto Label_007C;
                        }
                    }
                    else
                    {
                        info.SetValue(this, "", null);
                    }
                }
                goto Label_008A;
            Label_0061:
                info.SetValue(this, new DateTime(0x76c, 1, 1), null);
                goto Label_008A;
            Label_007C:
                info.SetValue(this, new byte[0], null);
            Label_008A: ;
            }
            this.IsChanged = false;
        }

        private bool IsInTableColumn(DataTable dt, string colName)
        {
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName == colName)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetMetaDataRow(DataRow row)
        {
            if (row == null)
            {
                this._IsEmpty = true;
            }
            else
            {
                this._IsEmpty = false;
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    if (this.IsInTableColumn(row.Table, columnName))
                    {
                        try
                        {
                            PropertyInfo property = base.GetType().GetProperty(columnName);
                            if (property != null)
                            {
                                property.SetValue(this, row[columnName], null);
                            }
                            continue;
                        }
                        catch (Exception exception)
                        {
                            throw new DataTransformException(exception.Message, exception);
                        }
                    }
                }
                this._IsChanged = false;
            }
        }

        public void SetMetaDataTable(DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                this._IsEmpty = true;
            }
            else
            {
                this._IsEmpty = false;
                DataTable metaDataTable = this.GetMetaDataTable();
                DataRow row = metaDataTable.Rows[0];
                DataRow row2 = table.Rows[0];
                foreach (DataColumn column in metaDataTable.Columns)
                {
                    string columnName = column.ColumnName;
                    if (this.IsInTableColumn(table, columnName))
                    {
                        try
                        {
                            row[columnName] = row2[columnName];
                            base.GetType().GetProperty(columnName).SetValue(this, row[columnName], null);
                            continue;
                        }
                        catch (Exception exception)
                        {
                            throw new DataTransformException(exception.Message, exception);
                        }
                    }
                }
                this._IsChanged = false;
            }
        }

        public void WriteXmlData(string fileName)
        {
            DataSet set = new DataSet("DataEntity");
            DataTable metaDataTable = this.GetMetaDataTable();
            set.Tables.Add(metaDataTable);
            set.WriteXml(fileName, XmlWriteMode.WriteSchema);
        }

        public XmlDocument XmlDom()
        {
            DataSet set = new DataSet("DataEntity");
            DataTable metaDataTable = this.GetMetaDataTable();
            set.Tables.Add(metaDataTable);
            XmlDocument document = new XmlDocument();
            document.LoadXml(set.GetXml());
            return document;
        }

        public bool IsChanged
        {
            get
            {
                return this._IsChanged;
            }
            set
            {
                this._IsChanged = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this._IsEmpty;
            }
            set
            {
                this._IsEmpty = value;
            }
        }

        public int ReturnValue
        {
            get
            {
                return this._ReturnValue;
            }
            set
            {
                this._ReturnValue = value;
            }
        }
    }
}