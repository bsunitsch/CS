// Copyright (c) Microsoft. All rights reserved.

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
using SenseHat;
using Windows.UI.Core;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
// ToDo:
// 1. Add the sense hat library from Boryki's IoT book to access RPi sense hat sensor functions
// 2. Read temp value from the sensoe
// 3. Display the sensor value on the RPi display
// 4. Set this up to loop and read the values once per 40ms
// 5. Upload each result somewhere (Azure DB?)

namespace HelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SensorReadings sensorReadings = new SensorReadings();
        private readonly TemperatureAndPressureSensor temperatureAndPressureSensor = TemperatureAndPressureSensor.Instance;
        private readonly HumidityAndTemperatureSensor humidityAndTemperatureSensor = HumidityAndTemperatureSensor.Instance;

        public MainPage()
        {
            InitializeComponent();
            InitSensors();
        }

        private async void InitSensors()
        {
            // init temp and pressure
            if(await temperatureAndPressureSensor.Initialize())
            {
                BeginTemperatureAndPressureAcquisition();
            }

            //init humitity 
            if(await humidityAndTemperatureSensor.Initialize())
            {
                BeginHumidityAqquisition();
            }
        }

        private void BeginTemperatureAndPressureAcquisition()
        {
            const int msDelayTime = 40;

            BeginSensorReading(async () =>
            {
                // get the value of temperature and display it
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    sensorReadings.Temperature = temperatureAndPressureSensor.GetTemperature();
                    sensorReadings.Humidity = humidityAndTemperatureSensor.GetHumidity();
                });
            }, msDelayTime );
        }

        private void BeginHumidityAqquisition()
        {
            const int msDelayTime = 80;

            BeginSensorReading(async () =>
            {

                //Get and display humitity sensor readings
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    sensorReadings.Humidity = humidityAndTemperatureSensor.GetHumidity();

                });
            }, msDelayTime);
        }

        private void BeginSensorReading(Action periodicAction, int msDelayTime)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    periodicAction();
                    Task.Delay(msDelayTime).Wait();
                }
            });
        }

    }

}
