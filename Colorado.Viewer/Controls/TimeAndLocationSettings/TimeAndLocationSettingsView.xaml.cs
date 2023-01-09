using System;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace Colorado.Viewer.Controls.TimeAndLocationSettings
{
    /// <summary>
    /// Interaction logic for TimeAndLocationSettingsView.xaml
    /// </summary>
    public partial class TimeAndLocationSettingsView : UserControl
    {
        public TimeAndLocationSettingsView()
        {
            InitializeComponent();
            dateTimePicker.Value = DateTime.Now;

            dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "HH:mm"; // Only use hours and minutes
            dateTimePicker.ShowUpDown = true;

            dateTimePicker.ValueChanged += DateTimePicker_ValueChanged;
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ((ITimeAndLocationSettingsViewModel)DataContext).CurrentDateTime = dateTimePicker.Value;
        }
    }
}
