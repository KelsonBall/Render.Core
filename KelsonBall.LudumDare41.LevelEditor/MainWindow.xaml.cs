using KelsonBall.LudumDare41.LevelEditor.Events;
using KelsonBall.LudumDare41.LevelEditor.Utils;
using PubSub;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KelsonBall.LudumDare41.LevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = (ViewModel = new MainWindowViewModel());
            this.Subscribe<MapImagePickedEvent>(args =>
            {
                ImageBrush image = new ImageBrush();
                image.ImageSource = new BitmapImage(new Uri(args.Uri));
                CanvasItemsControl.Background = image;
            });
        }

        private void MapCanvas_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var p = e.GetPosition(CanvasItemsControl);
            Console.WriteLine(p);
            this.Publish(new MapClickedAtEvent((p.X - xToLeftConverter.HalfWidth, p.Y - yToTopConverter.HalfHeight)));
        }
    }
}
