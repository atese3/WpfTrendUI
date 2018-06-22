using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TrendViewTest.VM
{
    public class TestViewModel : INotifyPropertyChanged
    {
        private int _property1 = 1;

        public int Property1
        {
            get
            {
                return _property1;
            }
            set
            {
                _property1 = value;
                OnPropertyChanged("Property1");
            }
        }
        private int _property2 = 2;

        public int Property2
        {
            get
            {
                return _property2;
            }
            set
            {
                _property2 = value;
                OnPropertyChanged("Property2");

            }
        }
        private int _property3 = 3;

        public int Property3
        {
            get
            {
                return _property3;
            }
            set
            {
                _property3 = value;
                OnPropertyChanged("Property3");

            }
        }
        private int _property4 = 4;

        public int Property4
        {
            get
            {
                return _property4;
            }
            set
            {
                _property4 = value;
                OnPropertyChanged("Property4");

            }
        }
        private int _property5 = 5;

        public int Property5
        {
            get
            {
                return _property5;
            }
            set
            {
                _property5 = value;
                OnPropertyChanged("Property5");

            }
        }
        private int _property6 = 6;

        public int Property6
        {
            get
            {
                return _property6;
            }
            set
            {
                _property6 = value;
                OnPropertyChanged("Property6");

            }
        }
        private int _property7 = 7;

        public int Property7
        {
            get
            {
                return _property7;
            }
            set
            {
                _property7 = value;
                OnPropertyChanged("Property7");

            }
        }
        ////-----------------
        Timer cycleTimer;
        protected void TimerInit()
        {
            cycleTimer = new Timer();
            cycleTimer.Elapsed += new ElapsedEventHandler(CycleTimedEvent);
            cycleTimer.Interval = 1000;
            cycleTimer.Enabled = true;
        }
        private void CycleTimedEvent(object source, ElapsedEventArgs e)
        {
            Random temp = new Random();
            Property1 = temp.Next(0, 100);
            Property2 = temp.Next(0, 100);
            Property3 = temp.Next(0, 100);
            Property4 = temp.Next(0, 100);
            Property5 = temp.Next(0, 100);
            Property6 = temp.Next(0, 100);
            Property7 = temp.Next(0, 100);
        }
        ////-----------------
        public TestViewModel()
        {
            TimerInit();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
