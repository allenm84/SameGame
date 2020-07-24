using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace System.Common.Extensions
{
  public static partial class ControlExtensions
  {
    private const int WM_SETREDRAW = 11;
    private const int TCM_FIRST = 0x1300;
    private const int TCM_GETITEMRECT = (TCM_FIRST + 10);

    private static readonly Lazy<PropertyInfo> doubleBuffer = new Lazy<PropertyInfo>(() =>
      typeof(Control).GetProperty("DoubleBuffered",
        BindingFlags.Public |
          BindingFlags.NonPublic |
          BindingFlags.Instance), true);

    private static readonly Lazy<PropertyInfo> resizeRedraw = new Lazy<PropertyInfo>(() =>
      typeof(Control).GetProperty("ResizeRedraw",
        BindingFlags.Public |
          BindingFlags.NonPublic |
          BindingFlags.Instance), true);

    private static List<DataGridViewIncrementalSearchData> incrementalSearch = new List<DataGridViewIncrementalSearchData>();

    [DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

    [DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref RECT lParam);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

    public static void ResetItem<T>(this BindingList<T> list, T item)
    {
      var index = list.IndexOf(item);
      list.ResetItem(index);
    }

    public static IDisposable DeferBinding<T>(this BindingList<T> list)
    {
      return new BindingListSuspend<T>(list);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="list"></param>
    public static void SetAdvanceStyle(this ListView list)
    {
      if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
      {
        SetWindowTheme(list.Handle, "explorer", null);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tree"></param>
    public static void SetAdvanceStyle(this TreeView tree)
    {
      if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
      {
        SetWindowTheme(tree.Handle, "explorer", null);
        tree.ShowLines = false;
        tree.FullRowSelect = true;
        tree.ItemHeight = 20;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timer"></param>
    public static void Restart(this Timer timer)
    {
      timer.Stop();
      timer.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="child"></param>
    /// <param name="parent"></param>
    public static void CenterToParent(this Form child, Form parent)
    {
      child.Location = new Point(
        parent.Location.X + (parent.Width - child.Width) / 2,
        parent.Location.Y + (parent.Height - child.Height) / 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="column"></param>
    public static void EnableIncrementalSearch(this DataGridView grid, Func<DataGridViewRow, string> getText)
    {
      if (incrementalSearch.Any(d => ReferenceEquals(d.Grid, grid))) { return; }

      incrementalSearch.Add(new DataGridViewIncrementalSearchData
      {
        Buffer = new StringBuilder(),
        Grid = grid,
        Start = DateTime.Now,
        GetText = getText,
      });

      grid.KeyPress += dataGrid_KeyPress;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void dataGrid_KeyPress(object sender, KeyPressEventArgs e)
    {
      var data = incrementalSearch.SingleOrDefault(d => ReferenceEquals(d.Grid, sender));
      if (data == null) { return; }

      if ((DateTime.Now - data.Start).TotalSeconds >= 1.5)
      {
        data.Buffer.Clear();
      }

      data.Buffer.Append(e.KeyChar);
      var searchString = data.Buffer.ToString();

      foreach (DataGridViewRow row in data.Grid.Rows)
      {
        var text = data.GetText(row);
        if (text.StartsWith(searchString, StringComparison.InvariantCultureIgnoreCase))
        {
          data.Grid.ClearSelection();
          row.Selected = true;
          data.Grid.FirstDisplayedScrollingRowIndex = row.Index;
          break;
        }
      }

      data.Start = DateTime.Now;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static IDisposable DeferLayout(this Control control)
    {
      return new ControlDeferLayout(control);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public static int BinarySearch(this TreeNodeCollection nodes, string text)
    {
      var index = 0;
      var length = nodes.Count;

      var i = index;
      var num = index + length - 1;

      while (i <= num)
      {
        var num2 = i + (num - i >> 1);
        var num3 = string.Compare(nodes[num2].Text, text, true);
        if (num3 == 0)
        {
          return num2;
        }
        if (num3 < 0)
        {
          i = num2 + 1;
        }
        else
        {
          num = num2 - 1;
        }
      }
      return ~i;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="row"></param>
    /// <returns></returns>
    public static T SafeGetDataBoundItem<T>(this DataGridViewRow row) where T : class
    {
      T retval = null;
      try { retval = row.DataBoundItem as T; }
      catch (IndexOutOfRangeException)
      {
        retval = null;
      }
      return retval;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="property"></param>
    public static void AddDataBoundColumn(this DataGridViewColumnCollection columns, string property)
    {
      columns.Add(new DataGridViewTextBoxColumn
      {
        DataPropertyName = property,
        HeaderText = property,
        Name = property,
      });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static IDisposable DeferRefresh(this DataGridView grid)
    {
      return new DataGridViewDeferRefresh(grid);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="control"></param>
    /// <param name="action"></param>
    public static void InvokeIfNeeded(this Control control, Action action)
    {
      if (control.InvokeRequired)
      {
        control.Invoke(action);
      }
      else
      {
        action();
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="control"></param>
    /// <param name="action"></param>
    /// <param name="arg"></param>
    public static void InvokeIfNeeded<T>(this Control control, Action<T> action, T arg)
    {
      if (control.InvokeRequired)
      {
        control.Invoke(action, arg);
      }
      else
      {
        action(arg);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="combBox"></param>
    /// <param name="values"></param>
    public static void Populate<T>(this ComboBox combBox, IEnumerable<T> values)
    {
      Populate(combBox, values, x => x.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="combBox"></param>
    /// <param name="values"></param>
    /// <param name="getText"></param>
    public static void Populate<T>(this ComboBox combBox, IEnumerable<T> values, Func<T, string> getText)
    {
      var dataSource = values
        .Select(value =>
          new
          {
            Text = getText(value),
            Value = value,
          })
        .ToList();

      combBox.Items.Clear();
      combBox.DataSource = dataSource;
      combBox.DisplayMember = "Text";
      combBox.ValueMember = "Value";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="progressBar"></param>
    /// <returns></returns>
    public static double Percent(this ProgressBar progressBar)
    {
      double min = progressBar.Minimum;
      double max = progressBar.Maximum;
      double value = progressBar.Value;
      return ((value - min) / (max - min));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="form"></param>
    public static void BringToForeground(this Form form)
    {
      form.TopMost = true;
      Application.DoEvents();

      form.TopMost = false;
      Application.DoEvents();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="owner"></param>
    /// <param name="subControls"></param>
    /// <returns></returns>
    public static IEnumerable<T> GetControls<T>(this Control owner, bool subControls) where T : Control
    {
      foreach (Control control in owner.Controls)
      {
        if (control is T) { yield return control as T; }
        if (subControls)
        {
          foreach (var child in control.GetControls<T>(subControls))
          {
            yield return child;
          }
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tabControl"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static Rectangle GetTabHeaderRect(this TabControl tabControl, int index)
    {
      var tabRect = new RECT();
      SendMessage(tabControl.Handle, TCM_GETITEMRECT, index, ref tabRect);
      return Rectangle.FromLTRB(tabRect.Left, tabRect.Top, tabRect.Right, tabRect.Bottom + 2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="sequence"></param>
    /// <returns></returns>
    public static bool RemoveRange<T>(this BindingList<T> list, IEnumerable<T> sequence)
    {
      var removed = true;
      foreach (var item in sequence)
      {
        removed &= list.Remove(item);
      }
      return removed;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="sequence"></param>
    public static void AddRange<T>(this BindingList<T> list, IEnumerable<T> sequence)
    {
      foreach (var item in sequence)
      {
        list.Add(item);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="control"></param>
    /// <param name="value"></param>
    public static void SetDoubleBuffered(this Control control, bool value)
    {
      doubleBuffer.Value.SetValue(control, value, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="control"></param>
    /// <param name="value"></param>
    public static void SetResizeRedraw(this Control control, bool value)
    {
      resizeRedraw.Value.SetValue(control, value, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="control"></param>
    public static void SuspendDrawing(this Control control)
    {
      SendMessage(control.Handle, WM_SETREDRAW, false, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="control"></param>
    public static void ResumeDrawing(this Control control)
    {
      SendMessage(control.Handle, WM_SETREDRAW, true, 0);
      control.Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tabControl"></param>
    public static void HideTabPages(this TabControl tabControl)
    {
      var tabPage1 = tabControl.TabPages[0];
      var rect = new RectangleF(tabPage1.Left, tabPage1.Top, tabPage1.Width, tabPage1.Height);
      tabControl.Region = new Region(rect);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="numericUpDown"></param>
    public static void ForceUpdate(this NumericUpDown numericUpDown)
    {
      numericUpDown.UpButton();
      numericUpDown.DownButton();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="numericUpDown"></param>
    /// <returns></returns>
    public static bool IsZero(this NumericUpDown numericUpDown)
    {
      if (string.IsNullOrEmpty(numericUpDown.Text))
      {
        return true;
      }
      return numericUpDown.Value == 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataGridView"></param>
    /// <param name="columnIndex"></param>
    /// <param name="rowIndex"></param>
    /// <returns></returns>
    public static bool IsIndexValid(this DataGridView dataGridView, int columnIndex, int rowIndex)
    {
      return
        (-1 < columnIndex && columnIndex < dataGridView.Columns.Count) &&
          (-1 < rowIndex && rowIndex < dataGridView.Rows.Count);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="dataBoundItem"></param>
    /// <returns></returns>
    public static DataGridViewRow GetRow(this DataGridViewRowCollection collection, object dataBoundItem)
    {
      DataGridViewRow retval = null;
      for (var i = 0; i < collection.Count; ++i)
      {
        var row = collection[i];
        if (row.DataBoundItem == dataBoundItem)
        {
          retval = row;
          i = collection.Count;
        }
      }
      return retval;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="names"></param>
    public static void Hide(this DataGridViewColumnCollection columns, params string[] names)
    {
      foreach (DataGridViewColumn column in columns)
      {
        column.Visible = !(names.Contains(column.Name));
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="names"></param>
    public static void HideAllExcept(this DataGridViewColumnCollection columns, params string[] names)
    {
      foreach (DataGridViewColumn column in columns)
      {
        column.Visible = (names.Contains(column.Name));
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="grid"></param>
    public static void EnableCellUpdate(this DataGridView grid)
    {
      grid.CurrentCellDirtyStateChanged += grid_CurrentCellDirtyStateChanged;
    }

    private static void grid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      var dataGridView1 = sender as DataGridView;
      if (dataGridView1.IsCurrentCellDirty)
      {
        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="name"></param>
    /// <param name="width"></param>
    /// <param name="centerText"></param>
    public static void LockColumnWidth(this DataGridViewColumnCollection columns, string name, int width, bool centerText = true)
    {
      columns[name].Width = width;
      columns[name].Resizable = DataGridViewTriState.False;
      columns[name].MinimumWidth = width;
      columns[name].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
      if (centerText)
      {
        columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="multiselect"></param>
    /// <param name="allowDelete"></param>
    public static void SetPropertiesToList(this DataGridView grid, bool multiselect, bool allowDelete)
    {
      grid.AllowUserToAddRows = false;
      grid.AllowUserToDeleteRows = allowDelete;
      grid.AllowUserToOrderColumns = false;
      grid.AllowUserToResizeColumns = false;
      grid.AllowUserToResizeRows = false;
      grid.AutoGenerateColumns = true;
      grid.AutoSize = false;
      grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      grid.BackgroundColor = Color.White;
      grid.BorderStyle = BorderStyle.Fixed3D;
      grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
      grid.ColumnHeadersVisible = false;
      grid.MultiSelect = multiselect;
      grid.ReadOnly = true;
      grid.RowHeadersVisible = false;
      grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      grid.StandardTab = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="grid"></param>
    public static void SetPropertiesToHideSelection(this DataGridView grid)
    {
      grid.DefaultCellStyle.SelectionBackColor = grid.DefaultCellStyle.BackColor;
      grid.DefaultCellStyle.SelectionForeColor = grid.DefaultCellStyle.ForeColor;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    private static string getName(Expression expression)
    {
      MemberExpression mexp;
      if (expression is UnaryExpression)
      {
        mexp = ((UnaryExpression)expression).Operand as MemberExpression;
      }
      else
      {
        mexp = expression as MemberExpression;
      }
      return mexp.Member.Name;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    /// <typeparam name="TDataSource"></typeparam>
    /// <param name="property"></param>
    /// <param name="dataSource"></param>
    /// <param name="dataMember"></param>
    /// <param name="formattingEnabled"></param>
    /// <param name="dataSourceUpdateMode"></param>
    /// <param name="nullValue"></param>
    /// <param name="formatString"></param>
    /// <param name="formatInfo"></param>
    /// <returns></returns>
    private static Binding createBinding<TControl, TDataSource>(Expression<Func<TControl, object>> property, TDataSource dataSource, Expression<Func<TDataSource, object>> dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString, IFormatProvider formatInfo)
    {
      return new Binding(getName(property.Body), dataSource, getName(dataMember.Body), formattingEnabled, dataSourceUpdateMode, nullValue, formatString, formatInfo);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    /// <typeparam name="TDataSource"></typeparam>
    /// <param name="bindings"></param>
    /// <param name="property"></param>
    /// <param name="dataSource"></param>
    /// <param name="dataMember"></param>
    public static void Add<TControl, TDataSource>(this ControlBindingsCollection bindings, Expression<Func<TControl, object>> property, TDataSource dataSource, Expression<Func<TDataSource, object>> dataMember)
    {
      bindings.Add(createBinding(property, dataSource, dataMember, false, DataSourceUpdateMode.OnValidation, null, string.Empty, null));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    /// <typeparam name="TDataSource"></typeparam>
    /// <param name="bindings"></param>
    /// <param name="property"></param>
    /// <param name="dataSource"></param>
    /// <param name="dataMember"></param>
    /// <param name="formattingEnabled"></param>
    public static void Add<TControl, TDataSource>(this ControlBindingsCollection bindings, Expression<Func<TControl, object>> property, TDataSource dataSource, Expression<Func<TDataSource, object>> dataMember, bool formattingEnabled)
    {
      bindings.Add(createBinding(property, dataSource, dataMember, formattingEnabled, DataSourceUpdateMode.OnValidation, null, string.Empty, null));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    /// <typeparam name="TDataSource"></typeparam>
    /// <param name="bindings"></param>
    /// <param name="property"></param>
    /// <param name="dataSource"></param>
    /// <param name="dataMember"></param>
    /// <param name="formattingEnabled"></param>
    /// <param name="dataSourceUpdateMode"></param>
    public static void Add<TControl, TDataSource>(this ControlBindingsCollection bindings, Expression<Func<TControl, object>> property, TDataSource dataSource, Expression<Func<TDataSource, object>> dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode)
    {
      bindings.Add(createBinding(property, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, null, string.Empty, null));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    /// <typeparam name="TDataSource"></typeparam>
    /// <param name="bindings"></param>
    /// <param name="property"></param>
    /// <param name="dataSource"></param>
    /// <param name="dataMember"></param>
    /// <param name="formattingEnabled"></param>
    /// <param name="dataSourceUpdateMode"></param>
    /// <param name="nullValue"></param>
    public static void Add<TControl, TDataSource>(this ControlBindingsCollection bindings, Expression<Func<TControl, object>> property, TDataSource dataSource, Expression<Func<TDataSource, object>> dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue)
    {
      bindings.Add(createBinding(property, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, string.Empty, null));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    /// <typeparam name="TDataSource"></typeparam>
    /// <param name="bindings"></param>
    /// <param name="property"></param>
    /// <param name="dataSource"></param>
    /// <param name="dataMember"></param>
    /// <param name="formattingEnabled"></param>
    /// <param name="dataSourceUpdateMode"></param>
    /// <param name="nullValue"></param>
    /// <param name="formatString"></param>
    public static void Add<TControl, TDataSource>(this ControlBindingsCollection bindings, Expression<Func<TControl, object>> property, TDataSource dataSource, Expression<Func<TDataSource, object>> dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString)
    {
      bindings.Add(createBinding(property, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, formatString, null));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    /// <typeparam name="TDataSource"></typeparam>
    /// <param name="bindings"></param>
    /// <param name="property"></param>
    /// <param name="dataSource"></param>
    /// <param name="dataMember"></param>
    /// <param name="formattingEnabled"></param>
    /// <param name="dataSourceUpdateMode"></param>
    /// <param name="nullValue"></param>
    /// <param name="formatString"></param>
    /// <param name="formatInfo"></param>
    public static void Add<TControl, TDataSource>(this ControlBindingsCollection bindings, Expression<Func<TControl, object>> property, TDataSource dataSource, Expression<Func<TDataSource, object>> dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString, IFormatProvider formatInfo)
    {
      bindings.Add(createBinding(property, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, formatString, formatInfo));
    }

    #region Nested type: BindingListSuspend

    private class BindingListSuspend<T> : IDisposable
    {
      private BindingList<T> list;

      public BindingListSuspend(BindingList<T> list)
      {
        this.list = list;
        list.RaiseListChangedEvents = false;
      }

      #region IDisposable Members

      public void Dispose()
      {
        list.RaiseListChangedEvents = true;
        list.ResetBindings();
      }

      #endregion
    }

    #endregion

    #region Nested type: ControlDeferLayout

    private class ControlDeferLayout : IDisposable
    {
      private Control control;

      public ControlDeferLayout(Control control)
      {
        this.control = control;
        control.SuspendLayout();
      }

      #region IDisposable Members

      public void Dispose()
      {
        control.ResumeLayout(true);
      }

      #endregion
    }

    #endregion

    #region Nested type: DataGridViewDeferRefresh

    private class DataGridViewDeferRefresh : IDisposable
    {
      private DataGridView dataGrid;
      private object dataSource;

      public DataGridViewDeferRefresh(DataGridView grid)
      {
        dataSource = grid.DataSource;
        dataGrid = grid;
        dataGrid.DataSource = null;
      }

      #region IDisposable Members

      public void Dispose()
      {
        dataGrid.DataSource = dataSource;
      }

      #endregion
    }

    #endregion

    #region Nested type: DataGridViewIncrementalSearchData

    private class DataGridViewIncrementalSearchData
    {
      public DataGridView Grid { get; set; }
      public StringBuilder Buffer { get; set; }
      public DateTime Start { get; set; }
      public Func<DataGridViewRow, string> GetText { get; set; }
    }

    #endregion

    #region Nested type: RECT

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
      internal RECT(int X, int Y, int Width, int Height)
      {
        this.Left = X;
        this.Top = Y;
        this.Right = Width;
        this.Bottom = Height;
      }

      internal int Left;
      internal int Top;
      internal int Right;
      internal int Bottom;
    }

    #endregion
  }
}