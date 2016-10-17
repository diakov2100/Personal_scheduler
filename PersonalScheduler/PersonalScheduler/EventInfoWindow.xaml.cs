using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace PersonalScheduler
{
    /// <summary>
    /// Interaction logic for EventInfoWindow.xaml
    /// </summary>
    public partial class EventInfoWindow : Window
    {
        EventManager _eventManager;

        public EventInfoWindow(EventManager eventManager)
        {
            InitializeComponent();
            _eventManager = eventManager;

            // By default put the next day as the date
            datePicker.SelectedDate = DateTime.Now.Date.AddDays(1);
            textBoxTime.Text = _previousTimeValue;
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            // The GetDateTime function (implemented inside the template region below)
            // combines data from two UI controls (datePicker and time textbox)
            // to produce a single DateTime? (nullable DateTime) object. Check that it
            // is not null before creating a new event
            var date = GetDateTime();
            if (date != null)
            {
                try
                {
                    List<NotificationType> Lis = new List<NotificationType>();
                    if (checkBoxSound.IsChecked == true)
                    {
                        Lis.Add(NotificationType.Sound);
                    }
                    if (checkBoxVisual.IsChecked == true)
                    {
                        Lis.Add(NotificationType.Visual);
                    }
                    if (checkBoxEmail.IsChecked == true)
                    {
                        Lis.Add(NotificationType.Email);
                    }
                    if (checkBoxRepeat.IsChecked != true)
                        _eventManager.AddEvent(new ScheduledEvent(textBoxName.Text, date.Value, textBoxDescription.Text, textBoxPlace.Text, Lis));
                    else
                    {
                        int a;
                        if (!int.TryParse(textBoxRepeat.Text, out a))
                        {
                            throw new ArgumentException("Repeate time should be positive integer");
                        }
                        switch (comboBoxUnits.SelectedIndex)
                        {
                            case 0:
                                _eventManager.AddEvent(new RegularEvent(new TimeSpan(0, Convert.ToInt32(textBoxRepeat.Text), 0), textBoxName.Text, date.Value, textBoxDescription.Text, textBoxPlace.Text, Lis));
                                break;
                            case 1:
                                _eventManager.AddEvent(new RegularEvent(new TimeSpan(Convert.ToInt32(textBoxRepeat.Text), 0, 0), textBoxName.Text, date.Value, textBoxDescription.Text, textBoxPlace.Text, Lis));
                                break;
                            case 2:
                                _eventManager.AddEvent(new RegularEvent(new TimeSpan(Convert.ToInt32(textBoxRepeat.Text), 0, 0, 0), textBoxName.Text, date.Value, textBoxDescription.Text, textBoxPlace.Text, Lis));
                                break;
                        }
                    }
                    DialogResult = true;
                }
                catch (Exception exeption)
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show(
                        @"" + exeption.Message + "/n"
                        + "Retry?/n"
                        + "Press No if you don't need to add event", "Authentication error", MessageBoxButton.YesNo, MessageBoxImage.Error);
                    if (result == MessageBoxResult.Yes)
                    {
                        switch (exeption.Message)
                        {
                            case "Name cannot be set as an empty string or a string of whitespaces.":
                                textBoxName.Text = "";
                                break;
                            case "Name cannot be set as null.":
                                textBoxName.Text = "";
                                break;
                            case "13":
                                textBoxTime.Text= _previousTimeValue;
                                datePicker.SelectedDate = DateTime.Now.Date.AddDays(1);
                                break;
                            case "Minimum one notification should be selected.":
                                break;
                            case "Repeate time should be positive integer":
                                textBoxRepeat.Text = "";
                                break;
                            

                        }
                    }
                    else
                    {
                        DialogResult = true;
                    }
                }

            }


        }

        #region Template code - don't change it
        private DateTime? GetDateTime()
        {
            if (!datePicker.SelectedDate.HasValue || string.IsNullOrWhiteSpace(textBoxTime.Text))
                return null;
            Match match = _timeRegex.Match(textBoxTime.Text);
            if (!match.Success)
                return null;

            DateTime value = datePicker.SelectedDate.Value;

            value = value.AddHours(int.Parse(match.Groups[1].Value))
                .AddMinutes(int.Parse(match.Groups[2].Value));

            return value;
        }

        Regex _timeRegex = new Regex(@"^(\d{0,2}):(\d{0,2})$");
        string _previousTimeValue = "00:00";

        private void textBoxTime_LostFocus(object sender, RoutedEventArgs e)
        {
            Match match = _timeRegex.Match(textBoxTime.Text);
            if (!match.Success || int.Parse(match.Groups[1].Value) > 23
                || int.Parse(match.Groups[2].Value) > 59)
                textBoxTime.Text = _previousTimeValue;
            else
                _previousTimeValue = textBoxTime.Text;
        }

        private void checkBoxRepeat_Checked(object sender, RoutedEventArgs e)
        {
            textBoxRepeat.IsEnabled = true;
            comboBoxUnits.IsEnabled = true;
        }

        private void checkBoxRepeat_Unchecked(object sender, RoutedEventArgs e)
        {
            textBoxRepeat.IsEnabled = false;
            comboBoxUnits.IsEnabled = false;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        #endregion
    }
}
