using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;                       //Add
using System.Windows.Forms;                 //Add
using System.Windows.Forms.VisualStyles;    //Add

namespace WinFormLab.Grid
{
    public static class DataGridViewLib
    {
        #region DataGridView 欄位新增
        public enum ColTypeEnum { Text, Button }
        /// <summary>DataGridView 欄位新增
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <param name="HeaderText"></param>
        /// <param name="DataPropertyName"></param>
        /// <param name="ColumnWidth"></param>
        /// <param name="Visible"></param>
        /// <param name="Grid"></param>
        public static void ColumnAdd(string ColumnName, string HeaderText, string DataPropertyName, int ColumnWidth, bool Visible, DataGridView Grid)
        {
            ColumnAdd(ColumnName, HeaderText, DataPropertyName, ColumnWidth, ColTypeEnum.Text, Grid);
            Grid.Columns[ColumnName].Visible = Visible;
        }
        /// <summary>DataGridView 欄位新增
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <param name="HeaderText"></param>
        /// <param name="DataPropertyName"></param>
        /// <param name="ColumnWidth"></param>
        /// <param name="ForeColor"></param>
        /// <param name="BackColor"></param>
        /// <param name="Grid"></param>
        public static void ColumnAdd(string ColumnName, string HeaderText, string DataPropertyName, int ColumnWidth, Color ForeColor, Color BackColor, DataGridView Grid)
        {
            ColumnAdd(ColumnName, HeaderText, DataPropertyName, ColumnWidth, ColTypeEnum.Text, Grid);
            Grid.Columns[ColumnName].DefaultCellStyle.BackColor = BackColor;
            Grid.Columns[ColumnName].DefaultCellStyle.ForeColor = ForeColor;
        }
        /// <summary>DataGridView 欄位新增
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <param name="HeaderText"></param>
        /// <param name="DataPropertyName"></param>
        /// <param name="ColumnWidth"></param>
        /// <param name="ColumnType"></param>
        /// <param name="Grid"></param>
        public static void ColumnAdd(string ColumnName, string HeaderText, string DataPropertyName, int ColumnWidth, ColTypeEnum ColumnType, DataGridView Grid)
        {
            switch (ColumnType)
            {
                case ColTypeEnum.Button:
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.HeaderText = HeaderText;
                    btn.Name = ColumnName;
                    btn.Text = HeaderText;
                    btn.UseColumnTextForButtonValue = true;
                    Grid.Columns.Add(btn);
                    Grid.Columns[ColumnName].DefaultCellStyle.BackColor = Color.LightGray;
                    Grid.Columns[ColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    Grid.Columns[ColumnName].Resizable = DataGridViewTriState.False;
                    break;
                default:
                    Grid.Columns.Add(ColumnName, HeaderText);

                    Grid.Columns[ColumnName].Resizable = DataGridViewTriState.True;
                    break;
            }
            Grid.Columns[ColumnName].DataPropertyName = DataPropertyName;
            if (ColumnWidth <= 0)
                Grid.Columns[ColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            else
                Grid.Columns[ColumnName].Width = ColumnWidth;
            Grid.Columns[ColumnName].ReadOnly = true;
            Grid.Columns[ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns[ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion
    }


    #region public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
    public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
    {
        /// <summary>DataGridView 按鈕 欄位，支援隱藏設定
        /// </summary>
        public DataGridViewDisableButtonColumn()
        {
            this.CellTemplate = new DataGridViewDisableButtonCell();
        }
        /// <summary>DataGridView 按鈕 欄位，支援隱藏設定
        /// </summary>
        /// <param name="HeaderText">標題</param>
        /// <param name="Text">按鈕的字</param>
        /// <param name="Name"></param>
        public DataGridViewDisableButtonColumn(string HeaderText, string Text, string Name)
        {
            this.CellTemplate = new DataGridViewDisableButtonCell();
            this.HeaderText = HeaderText;
            this.Text = Text;
            this.Name = Name;
            this.UseColumnTextForButtonValue = true;
        }
    }
    #endregion
    #region public class DataGridViewDisableButtonCell : DataGridViewButtonCell
    public class DataGridViewDisableButtonCell : DataGridViewButtonCell
    {
        private bool hideValue;

        public bool Hide
        {
            get
            {
                return hideValue;
            }
            set
            {
                hideValue = value;
            }
        }

        // Override the Clone method so that the Hide property is copied.
        public override object Clone()
        {
            DataGridViewDisableButtonCell cell = (DataGridViewDisableButtonCell)base.Clone();
            cell.Hide = this.Hide;
            return cell;
        }

        // By default, display the button cell.
        public DataGridViewDisableButtonCell()
        {
            this.hideValue = false;
        }

        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (this.hideValue)
            {
                #region The button cell is hide, so paint the border, background, and hide button for the cell.

                // Draw the cell background, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                }

                #region // if we don't want to draw the button at all, comment out the following lines.
                //// Calculate the area in which to draw the button.
                //Rectangle buttonArea = cellBounds;
                //Rectangle buttonAdjustment = this.BorderWidths(advancedBorderStyle);
                //buttonArea.X += buttonAdjustment.X;
                //buttonArea.Y += buttonAdjustment.Y;
                //buttonArea.Height -= buttonAdjustment.Height;
                //buttonArea.Width -= buttonAdjustment.Width;

                //// Draw the disabled button.               
                //ButtonRenderer.DrawButton(graphics, buttonArea,PushButtonState.Disabled);

                //// Draw the disabled button text.
                //if (this.FormattedValue is String)
                //{
                //    TextRenderer.DrawText(graphics,
                //        (string)this.FormattedValue,
                //        this.DataGridView.Font,
                //        buttonArea, SystemColors.GrayText);
                //}
                #endregion //comment

                #endregion
            }
            else
            {
                #region The button cell isn't hide, so let the base class handle the painting.
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
                #endregion
            }
        } //Paint
    }
    #endregion

    #region public class DataGridViewDisableCheckBoxColumn : DataGridViewCheckBoxColumn
    public class DataGridViewDisableCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        /// <summary>DataGridView CheckBox 欄位，支援隱藏設定
        /// </summary>
        public DataGridViewDisableCheckBoxColumn()
        {
            this.CellTemplate = new DataGridViewDisableCheckBoxCell();
        }
        /// <summary>DataGridView CheckBox 欄位，支援隱藏設定
        /// </summary>
        /// <param name="HeaderText">標題</param>
        /// <param name="Text">按鈕的字</param>
        /// <param name="Name"></param>
        public DataGridViewDisableCheckBoxColumn(string HeaderText, string Name, string DataPropertyName)
        {
            this.CellTemplate = new DataGridViewDisableCheckBoxCell();
            this.HeaderText = HeaderText;
            this.Name = Name;
            this.DataPropertyName = DataPropertyName;

        }
    }
    #endregion
    #region public class DataGridViewDisableCheckBoxCell : DataGridViewCheckBoxCell
    public class DataGridViewDisableCheckBoxCell : DataGridViewCheckBoxCell
    {
        private bool hideValue;
        public bool Hide
        {
            get
            {
                return hideValue;
            }
            set
            {
                hideValue = value;
            }
        }

        /// <summary>Override the Clone method so that the Hide property is copied.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            DataGridViewDisableCheckBoxCell cell = (DataGridViewDisableCheckBoxCell)base.Clone();
            cell.Hide = this.Hide;
            return cell;
        }

        /// <summary>By default, display the CheckBox cell.
        /// </summary>
        public DataGridViewDisableCheckBoxCell()
        {
            this.hideValue = false;
        }

        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (this.hideValue)
            {
                #region The CheckBox cell is hide, so paint the border, background, and hide button for the cell.

                // Draw the cell background, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                }

                #endregion
            }
            else
            {
                #region The CheckBox cell isn't hide, so let the base class handle the painting.
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
                #endregion
            }
        } //Paint

    }
    #endregion

}
