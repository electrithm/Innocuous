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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Speech.Synthesis;

namespace Innocuous
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Canvas canvas = new Canvas();
        int windowCount = 0;
    
        public MainWindow()
        {
            InitializeComponent();
        }
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            OpenWindows();
        }
        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(-1);
        }
        public void Speak()
        {
            SpeechSynthesizer speaker = new SpeechSynthesizer();
            speaker.Rate = 1;
            speaker.Volume = 100;
            speaker.Speak("soi");
        }
        public void OpenWindows()
        {
            while (true)
            {
                if (windowCount == 100)
                {
                    Environment.Exit(-1);
                }
                new Thread(() =>
                {
                    Speak();
                }).Start();

                windowCount = windowCount+1;
                Title = windowCount.ToString();
                WPFWindow w = new WPFWindow(windowCount);
                w.Show();
                System.Threading.Thread.Sleep(250);
            }
        }
    }
    public class WPFWindow : Window
    {

        private Canvas canvas = new Canvas();
        public static Random rnd = new Random();

        public WPFWindow(int count)
        {
            this.AllowsTransparency = false;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255))));
            this.Topmost = false;

            this.Title = count.ToString();
            this.Width = rnd.Next(0, 500);
            this.Height = rnd.Next(0, 500);
            canvas.Width = this.Width;
            canvas.Height = this.Height;
            canvas.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255)), Convert.ToByte(rnd.Next(0, 255))));
            this.Content = canvas;
        }
    }
}
