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
using System.Windows.Shapes;

namespace PersonalScheduler
{
    /// <summary>
    /// Логика взаимодействия для VisualNotifier.xaml
    /// </summary>
    public partial class VisualNotifierWindow : Window
    {
        public VisualNotifierWindow(ScheduledEvent ev)
        {
            InitializeComponent();
            EventName.Text = ev.Name;
            EventPlace.Text = ev.Place;
            EventDescription.Text = ev.Description;
        }
    }
}
