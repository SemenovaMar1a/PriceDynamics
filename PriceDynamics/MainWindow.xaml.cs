using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace PriceDynamics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private List<Tuple<int, int>> data = new List<Tuple<int, int>>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateData();
            DrawGraph();
            UpdateListBox();
        }

        private void UpdateData()
        {
            Random rnd = new Random();

            if (!string.IsNullOrWhiteSpace(Points.Text) && int.TryParse(Points.Text, out int targetPoints))
            {
                data.Add(new Tuple<int, int>(data.Count, rnd.Next(500)));

                if (data.Count >= targetPoints)
                {
                    timer.Stop();
                }
            }
        }

        private void UpdateListBox()
        {
            dataListBox.Items.Clear();

            foreach (var tuple in data)
            {
                dataListBox.Items.Add($"Год: {tuple.Item1 + 2000}, Руб.: {tuple.Item2} тыс.");
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGraph();
            UpdateListBox();
        }

        private void DrawGraph()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                Pen pen = new Pen(Brushes.Blue, 2);
                double width = canvas.ActualWidth;
                double height = canvas.ActualHeight;
                double xStep = width / (data.Count - 1);

                drawingContext.DrawLine(new Pen(Brushes.Black, 2), new Point(0, height), new Point(width, height));

                for (int i = 0; i < data.Count - 1; i++)
                {
                    Point startPoint = new Point(i * xStep, height - data[i].Item2);
                    Point endPoint = new Point((i + 1) * xStep, height - data[i + 1].Item2);

                    drawingContext.DrawLine(pen, startPoint, endPoint);

                    int year = 2000 + i;

                    drawingContext.DrawText(new FormattedText(year.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        new Typeface("Arial"), 12, Brushes.Black), new Point(i * xStep, height + 5));
                }

                for (int yValue = 0; yValue <= 500; yValue += 50)
                {
                    double yPos = height - (yValue / 500.0) * height;
                    drawingContext.DrawLine(new Pen(Brushes.Black, 2), new Point(-10, yPos), new Point(0, yPos));
                    drawingContext.DrawText(new FormattedText(yValue.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        new Typeface("Arial"), 10, Brushes.Black), new Point(-30, yPos - 5));
                }
            }

            canvas.Children.Clear();
            canvas.Children.Add(new DrawingVisualHost(drawingVisual));
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Points.Text) && int.TryParse(Points.Text, out int numberOfPoints) && numberOfPoints > 0)
            {
                data.Clear();
                timer.Start();
            }
            else
            {
                MessageBox.Show("Введите корректное количество точек.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Password pas = new Password();
            pas.Owner = this;
            pas.ShowDialog();

            if (Account.Enter == "operator")
            {
                Points.Visibility = Visibility.Collapsed;
                Build.Visibility = Visibility.Collapsed;
                lable.Visibility = Visibility.Collapsed;
                dataListBox.Visibility = Visibility.Collapsed;
                Points.Text = "50";
                Loaded += MainWindow_Loaded;
            }
        }
    }
}




