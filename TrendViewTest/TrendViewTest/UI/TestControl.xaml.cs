using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrendViewTest.VM;

namespace TrendViewTest.UI
{
    /// <summary>
    /// Interaction logic for TestControl.xaml
    /// </summary>
    public partial class TestControl : UserControl
    {
        //public TestControl()
        //{
        //    InitializeComponent();
        //    this.DataContext = new TestViewModel();
        //}
        protected override void OnInitialized(EventArgs e)
        {
            InitializeComponent();
            base.OnInitialized(e);
            this.DataContext = new TestViewModel();
        }

        private void TestControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragDrop.DoDragDrop(sender as UserControl, sender as UserControl, DragDropEffects.All);
            }
            catch
            {

            }
        }
    }
}
