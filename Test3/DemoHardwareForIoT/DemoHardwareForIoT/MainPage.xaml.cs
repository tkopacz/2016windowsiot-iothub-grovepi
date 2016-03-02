using GrovePi;
using GrovePi.I2CDevices;
using GrovePi.Sensors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace DemoHardwareForIoT
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
        IRgbLcdDisplay m_lcd;
        ISoundSensor m_sound;
        IRotaryAngleSensor m_rotary;
        private async void setup()
        {
            m_buz = DeviceFactory.Build.Buzzer(Pin.DigitalPin2);
            m_ut = DeviceFactory.Build.UltraSonicSensor(Pin.DigitalPin3);
            m_lcd = DeviceFactory.Build.RgbLcdDisplay();
            m_sound = DeviceFactory.Build.SoundSensor(Pin.AnalogPin0);
            m_rotary = DeviceFactory.Build.RotaryAngleSensor(Pin.AnalogPin1);

            m_lcd.SetBacklightRgb(128, 0, 0);

            m_t = new DispatcherTimer();
            m_t.Interval = TimeSpan.FromMilliseconds(5000);
            m_t.Tick += M_t_Tick;
            m_t.Start();
        }

        const int delay = 1000;
        static bool processing = false;
        private async void M_t_Tick(object sender, object e)
        {
            if (processing) return;
            processing = true;
            try {
                var ut = m_ut.MeasureInCentimeters();
                await Task.Delay(delay);
                var sound = m_sound.SensorValue();
                await Task.Delay(delay);
                var rotary = m_rotary.SensorValue();
                await Task.Delay(delay);
                m_lcd.SetText(ut.ToString());
                string msg = $"{ut}, {sound}, {rotary}";
                Debug.WriteLine(msg);
                await Task.Delay(delay);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            } finally
            {
                processing = false;
            }
        }
    }
}
