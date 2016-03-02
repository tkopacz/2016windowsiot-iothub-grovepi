using GrovePi;
using GrovePi.Sensors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DemoRelayApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            setup();
        }

        IRelay m_relay; 
        private async void setup()
        {
            var buzzer = DeviceFactory.Build.Buzzer(Pin.DigitalPin2);
            buzzer.ChangeState(SensorStatus.On);
            buzzer.ChangeState(SensorStatus.Off);
            //m_relay = DeviceFactory.Build.Relay(Pin.DigitalPin8);
            //m_relay.ChangeState(SensorStatus.On);
            //for (int i = 0; i < 10; i++) DeviceFactory.Build.Relay((Pin)i).ChangeState(SensorStatus.On);

        }

        private void tgSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (tgSwitch.IsOn) m_relay.ChangeState(SensorStatus.On); else m_relay.ChangeState(SensorStatus.Off);
        }
    }
}
