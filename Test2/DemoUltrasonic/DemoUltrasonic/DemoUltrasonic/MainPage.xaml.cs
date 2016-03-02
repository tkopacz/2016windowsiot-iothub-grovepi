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

namespace DemoUltrasonic
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

        DispatcherTimer m_t;
        IUltrasonicRangerSensor m_ut;
        IBuzzer m_buz;
        private async void setup()
        {
            m_buz=DeviceFactory.Build.Buzzer(Pin.DigitalPin2);
            m_ut=DeviceFactory.Build.UltraSonicSensor(Pin.DigitalPin3);
            m_t = new DispatcherTimer();
            m_t.Interval = TimeSpan.FromMilliseconds(100);
            m_t.Tick += M_t_Tick;
            m_t.Start();
        }

        private void M_t_Tick(object sender, object e)
        {
            
            if (m_ut.MeasureInCentimeters()<10)
            {
                m_buz.ChangeState(SensorStatus.On);
            } else
            {
                m_buz.ChangeState(SensorStatus.Off);
            }
        }
    }
}
