using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;
using System.Speech.Synthesis;
using System.Xml;
//using System.Xml;

namespace Innocuous
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Canvas canvas = new Canvas();
        int windowCount = 0;
        string sayThis = "soi";
        int maxWindows = 200;
        int windowWait = 250;

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
            if (sayThis != "")
            {
                SpeechSynthesizer speaker = new SpeechSynthesizer();
                speaker.Rate = 1;
                speaker.Volume = 100;
                speaker.Speak(sayThis);
            }
        }
        public void OpenWindows()
        {
            try
            {
                XmlReader xmlReader = XmlReader.Create("speak.xml");
                while (xmlReader.Read())
                {
                    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Speech"))
                    {
                        if (xmlReader.HasAttributes)
                        {
                            sayThis = xmlReader.GetAttribute("say");
                            maxWindows = Convert.ToInt32(xmlReader.GetAttribute("windowCount"));
                            windowWait = Convert.ToInt32(xmlReader.GetAttribute("windowWait"));
                        }
                    }
                }
            }
            catch{}
            while (true)
            {
                if (windowCount == maxWindows)
                {
                    Environment.Exit(-1);
                }
                new Thread(() =>
                {
                    Speak();
                }).Start();

                windowCount = windowCount + 1;
                Title = windowCount.ToString();
                WPFWindow w = new WPFWindow(windowCount);
                w.Show();
                System.Threading.Thread.Sleep(windowWait);
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
