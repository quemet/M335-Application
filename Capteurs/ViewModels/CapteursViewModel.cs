using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Capteurs.ViewModels
{
    public partial class CapteursViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? sensorValue;

        [ObservableProperty]
        private bool enabled = false;

        [ObservableProperty]
        private bool disabled = false;

        [ObservableProperty]
        private bool toggled;

        [ObservableProperty]
        private string shakeColor;

        public CapteursViewModel()
        {

        }

        [RelayCommand]
        private void Enable()
        {
            if(Accelerometer.Default.IsSupported)
            {
                if(!Accelerometer.Default.IsMonitoring) 
                {
                    Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;

                    Accelerometer.Default.Start(SensorSpeed.Default);

                    Enabled = true;
                    Disabled = false;
                } else
                {
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ReadingChanged -= Accelerometer_ReadingChanged;
                    Enabled = false;
                    Disabled = true;   
                }
            }
        }

        private void Accelerometer_ReadingChanged(object? sender, AccelerometerChangedEventArgs e)
        {
            SensorValue = e.Reading.ToString();
        }

        private void OnToggleChanged(bool value)
        {
            ToggleShake();
        }

        private void ToggleShake()
        {
            if(Accelerometer.Default.IsSupported)
            {
                if(!Accelerometer.Default.IsMonitoring)
                {
                    Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                    Accelerometer.Default.Start(SensorSpeed.Default);
                } else
                {
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
                    ShakeColor = "black";
                }
            }
        }

        private void Accelerometer_ShakeDetected(object? sender, EventArgs e)
        {
            ShakeColor = "red";
        }
    }
}