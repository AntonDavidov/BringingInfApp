using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ViewLayerWpfApp.Views
{
    public partial class ExtDataGrid : DataGrid
    {
        #region Fields
        public static DependencyProperty SelectedItemListProperty =
            DependencyProperty.Register("SelectedItemList", typeof(IList), typeof(ExtDataGrid));
        #endregion


        #region Constructors
        public ExtDataGrid() : base()
        {
            SelectionChanged += (sender, e) => { SelectedItemList = SelectedItems; };
        }
        #endregion


        #region Properties
        public IList SelectedItemList
        {
            get { return (IList)GetValue(SelectedItemListProperty); }
            set { SetValue(SelectedItemListProperty, value); }
        }
        #endregion
    }

    /*
    public partial class ExtPasswordBox : PasswordBox
    {
        #region Fields
        public static DependencyProperty PasswordTextProperty =
            DependencyProperty.Register("PasswordText", typeof(string), typeof(ExtPasswordBox));
        #endregion


        #region Constructors
        public ExtPasswordBox() : base()
        {
            PasswordBox.
            // += (sender, e) => { SelectedItemList = Password; };
        }
        #endregion


        #region Properties
        public string PasswordText
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        #endregion
    }
    */
}
