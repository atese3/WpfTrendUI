using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
using TrendViewTest.Other;

namespace TrendViewTest.UI
{
    /// <summary>
    /// Interaction logic for TrendUCView.xaml
    /// </summary>
    public partial class TrendUCView : UserControl
    {
        System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
        System.Windows.Forms.DataVisualization.Charting.SeriesChartType chartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        UserControl UCTemp;
        PropertyInfo[] propInfos;
        Type type;
        List<string> checkedList = new List<string>();
        private double[] cpuArray = new double[60];
        private Dictionary<PropertyInfo, double[]> cpuArrayList = new Dictionary<PropertyInfo, double[]>();
        private bool _oper = false;

        public bool Oper
        {
            get
            {
                return _oper;
            }
            set
            {
                _oper = value;
                Operation(value);
            }
        }
        public TrendUCView()
        {
            InitializeComponent();

            /// initialize charts
            chartArea1.Name = "ChartArea1";
            this.chartArea2.Name = "ChartArea2";
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.cpuChart.ChartAreas.Add(chartArea1);
            this.cpuChart2.ChartAreas.Add(chartArea2);

            /// initialize combobox
            cmb.ItemsSource = Enum.GetValues(typeof(Utils.SeriesChartType));
        }

        private Thread cpuThread;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cpuThread == null)
            {
                cpuThread = new Thread(new ThreadStart(this.getPerformanceCounters));
                cpuThread.IsBackground = true;
                cpuThread.Start();
                Oper = true;
            }
            else
            {
                if (MessageBox.Show("Trend Görselini Kapatmak Mı İstiyorsunuz?", "Trend Mesajı", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    cpuThread.Abort();
                    cpuThread = null;
                    Oper = false;
                }
            }
        }

        private void Operation(bool oper)
        {
            if (oper)
            {
                btn.Content = "Durdur";
                this.panelData.Visibility = Visibility.Visible;
            }
            else
            {
                btn.Content = "Başlat";
            }

            foreach (DataCellUC item in this.panelData.Children)
            {
                item.IsStop = !oper;
            }
        }
        private void getPerformanceCounters()
        {

            while (true)
            {
                this.Dispatcher.Invoke(() =>
                {
                    /// show all checked properties
                    foreach (string item in checkedList)
                    {
                        PropertyInfo tempProp = type.GetProperty(item); /// get values from checked property 
                        if (cpuArrayList.ContainsKey(tempProp))
                        {
                            double[] tempArray = cpuArrayList[tempProp];
                            tempArray[tempArray.Length - 1] = Double.Parse(tempProp.GetValue(UCTemp.DataContext).ToString());
                            Array.Copy(tempArray, 1, tempArray, 0, tempArray.Length - 1);
                            cpuArrayList.Remove(tempProp);
                            cpuArrayList.Add(tempProp, tempArray);
                        }
                        else /// if array list empty fill with the selected items property
                        {
                            double[] tempArray = new double[60]; /// span last 1 minute 
                            tempArray[tempArray.Length - 1] = Double.Parse(tempProp.GetValue(UCTemp.DataContext).ToString()); /// get latest value from item
                            Array.Copy(tempArray, 1, tempArray, 0, tempArray.Length - 1);
                            cpuArrayList.Add(tempProp, tempArray);
                        }
                    }
                    if (cpuChart.IsHandleCreated)
                    {
                        UpdateCpuChart(); /// update chart to see the latest values
                    }
                });

                Thread.Sleep(1000); /// refresh in 1 second
            }
        }
        private void UpdateCpuChart()
        {
            /// show properties and their values
            foreach (var item in cpuArrayList)
            {
                cpuChart.Series[item.Key.Name].Points.Clear(); /// seelect and clear series by property name

                for (int i = 0; i < cpuArray.Length - 1; i++)
                {
                    cpuChart.Series[item.Key.Name].Points.AddY(item.Value[i]); /// refresh series by property name
                }
            }
        }

        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Effects == DragDropEffects.All)
            {
                //var asd = e.Data.GetData(e.Data.GetFormats().FirstOrDefault());
                UCTemp = e.Data.GetData(e.Data.GetFormats().FirstOrDefault()) as UserControl; /// get component with base inheritance
                type = UCTemp.DataContext.GetType(); /// pass selected component datacontext 
                // Get the public properties.
                propInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                /// fill the checkboxes with property values
                int iter = 0;
                foreach (PropertyInfo item in propInfos)
                {
                    CheckBox cmb = new CheckBox();
                    cmb.Name = item.Name;
                    cmb.Uid = (iter++).ToString();
                    cmb.Content = item.Name;
                    /// use spesific check and uncheck event to control changes
                    cmb.Checked += cmb_Checked;
                    cmb.Unchecked += cmb_Unchecked;
                    this.cmbc.stackPanel.Children.Add(cmb);
                }
            }
        }
        void cmb_Checked(object sender, RoutedEventArgs e)
        {
            AddProperties(sender);
        }
        void cmb_Unchecked(object sender, RoutedEventArgs e)
        {
            DeleteProperties(sender);
        }
        private void AddProperties(object sender)
        {
            AddPropertiestoCheckBox(sender as CheckBox);
            RefreshColors();
        }
        private void DeleteProperties(object sender)
        {
            DeletePropertiesfromCheckBox(sender as CheckBox);
            RefreshColors();
        }
        private void DeletePropertiesfromCheckBox(CheckBox sender)
        {
            checkedList.Remove(sender.Name.ToString()); /// Remove from checked list 
            this.cpuChart.Series.Remove(this.cpuChart.Series.Where(x => x.Name.Equals(sender.Name)).FirstOrDefault()); /// Remove From first chart series
            this.cpuChart2.Series.Remove(this.cpuChart2.Series.Where(x => x.Name.Equals(sender.Name)).FirstOrDefault()); /// Remove from second chart series
            this.cpuChart2.Legends.Remove(this.cpuChart2.Legends.Where(x => x.Name.Equals("Legend" + sender.Uid)).FirstOrDefault()); /// Remove from legends
            foreach (DataCellUC item in this.panelData.Children) /// remove from table
            {
                if (item.BindingValue.Equals(sender.Name.ToString()))
                {
                    this.panelData.Children.Remove(item);
                    break;
                }
            }

            if (this.cpuArrayList.Where(x => x.Key.Name.Equals(sender.Name)).Count() > 0) /// Remove from properties list
            {
                this.cpuArrayList.Remove(this.cpuArrayList.Where(x => x.Key.Name.Equals((sender as CheckBox).Content.ToString())).FirstOrDefault().Key);
            }

            if (this.panelData.Children.Count < 2)
            {
                this.panelData.Visibility = Visibility.Collapsed;
            }
        }

        private void AddPropertiestoCheckBox(CheckBox sender)
        {
            if (!checkedList.Contains(sender.Content.ToString()))
            {
                checkedList.Add(sender.Content.ToString());

                System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
                System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
                System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();

                legend1.Name = "Legend" + sender.Uid; /// spesific name with list iteration
                legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left; /// legend left alignment (alternatives: right, top, bottom)
                legend1.Alignment = System.Drawing.StringAlignment.Near; /// legend position should be near the chart area
                
                //series2.ChartArea = "ChartArea2"; 
                series2.Legend = "Legend" + sender.Uid;
                series2.ChartType = chartType;
                series2.Name = sender.Name; /// spesific name with checkbox name
                this.cpuChart2.Legends.Add(legend1);
                this.cpuChart2.Series.Add(series2);

                //this.cpuChart2.TabIndex = 0;
                //this.cpuChart2.Text = "chart" + sender.Uid;
                //this.cpuChart.Name = "cpuChart";
                series1.ChartArea = "ChartArea1";

                series1.ChartType = chartType;
                series1.Name = sender.Content.ToString();
                this.cpuChart.Series.Add(series1);

                //this.cpuChart.TabIndex = 0;
                //this.cpuChart.Text = "chart" + sender.Uid;

                DataCellUC tempDataCell = new DataCellUC();
                tempDataCell.DataContext = UCTemp.DataContext;
                tempDataCell.BindingValue = (sender as CheckBox).Content.ToString();
                this.panelData.Children.Add(tempDataCell);

            }
        }

        private void RefreshColors()
        {
            cpuChart.ApplyPaletteColors();
            foreach (DataCellUC item in this.panelData.Children)
            {
                System.Windows.Forms.DataVisualization.Charting.Series tempSeries = this.cpuChart.Series.Where(x => x.Name.Equals(item.lblName.Content.ToString())).FirstOrDefault();
                if (tempSeries != null)
                {
                    item.lblName.Background = new SolidColorBrush(Color.FromArgb(tempSeries.Color.A, tempSeries.Color.R, tempSeries.Color.G, tempSeries.Color.B));
                }
            }
        }

        private void cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)Enum.Parse(typeof(Utils.SeriesChartType), e.AddedItems[0].ToString());
            foreach (System.Windows.Forms.DataVisualization.Charting.Series item in cpuChart.Series)
            {
                item.ChartType = chartType;
            }
        }
    }
}
