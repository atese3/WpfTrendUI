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

namespace TrendViewTest.UI
{
    /// <summary>
    /// Interaction logic for DataCellUC.xaml
    /// </summary>
    public partial class DataCellUC : UserControl
    {
        private string _bindingValue = string.Empty;
        private int _minValue = int.MinValue;
        private int _maxValue = int.MaxValue;
        private int _iter = 0;
        private float _ort = 0;
        private int _sum = 0;
        private bool _isStop = false;
        private bool _canStop = true;

        public bool CanStop
        {
            get
            {
                return _canStop;
            }
            set
            {
                _canStop = value;
            }
        }

        public bool IsStop
        {
            get
            {
                return _isStop;
            }
            set
            {
                if (CanStop)
                {
                    _isStop = value;
                }
            }
        }
        public string BindingValue
        {
            get
            {
                return _bindingValue;
            }
            set
            {
                _bindingValue = value;

                ConstructDataContext();
            }
        }
        public DataCellUC()
        {
            this.InitializeComponent();

        }

        private void ConstructDataContext()
        {
            this.lblName.Content = BindingValue;

            this.SetBinding(DataCellUC.ValueChangedProperty, new Binding(BindingValue)
            {
                Mode = BindingMode.TwoWay
            });
        }
        #region YagisTipiState
        public static readonly DependencyProperty ValueChangedProperty =
            DependencyProperty.Register("ViewValueChangedState", typeof(int),
            typeof(DataCellUC), new UIPropertyMetadata(0, ValueChangedPropertyChanged));

        public int ViewValueChangedState
        {
            get
            {
                return (int)GetValue(ValueChangedProperty);
            }
            set
            {
                SetValue(ValueChangedProperty, value);
            }
        }

        private static void ValueChangedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataCellUC originator = d as DataCellUC;
            if (originator != null)
            {
                if (!originator.IsStop)
                {
                    originator.lblCurrent.Content = originator.ViewValueChangedState.ToString();

                    if (originator.ViewValueChangedState < originator._minValue || originator._minValue.Equals(int.MinValue))
                    {
                        originator._minValue = originator.ViewValueChangedState;
                        originator.lblMin.Content = originator._minValue;
                    }

                    if (originator.ViewValueChangedState > originator._maxValue || originator._maxValue.Equals(int.MaxValue))
                    {
                        originator._maxValue = originator.ViewValueChangedState;
                        originator.lblMax.Content = originator._maxValue;
                    }

                    originator._sum += originator.ViewValueChangedState;
                    originator._iter++;
                    originator._ort = originator._sum / originator._iter;
                    originator.lblOrt.Content = originator._ort;
                }
            }
        }
        #endregion

    }
}
