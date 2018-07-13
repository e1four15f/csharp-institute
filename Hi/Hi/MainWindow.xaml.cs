using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Hi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Point> xy = new List<Point>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            var windowPosition = Mouse.GetPosition(this);
            var screenPosition = this.PointToScreen(windowPosition);
            Label foo = (Label) this.FindName("label");
            //foo.Content = string.Format("({0})", screenPosition);
            xy.Add(screenPosition);

            //Console.WriteLine(string.Format("Add ({0}) to list. List count : {1}", screenPosition, xy.Count));
            DrawDot(windowPosition);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello!");
        }

        public void DrawDot(Point p)
        {
            int dotSize = 2;

            Ellipse dot = new Ellipse();
            dot.Stroke = new SolidColorBrush(Colors.Black);
            dot.StrokeThickness = 1;
            Canvas.SetZIndex(dot, 3);
            dot.Height = dotSize;
            dot.Width = dotSize;
            dot.Fill = new SolidColorBrush(Colors.White);
            dot.Margin = new Thickness(p.X * 2 - this.RenderSize.Width + 12 + dotSize / 2, p.Y * 2 - this.RenderSize.Height + 35 + dotSize / 2, 0, 0); 
            grid.Children.Add(dot);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"C:\Users\User\Desktop\asdasd\xy.txt"))
            {
                foreach (Point p in xy)
                {
                    file.WriteLine(p);
                }
            }
        }
    }
}
