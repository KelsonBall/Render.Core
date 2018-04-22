using KelsonBall.LudumDare41.LevelEditor.Events;
using KelsonBall.LudumDare41.LevelEditor.Extensions;
using System.Windows.Forms;
using PubSub;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using KelsonBall.LudumDare41.LevelEditor.Utils;
using Render.Core.Textures;
using System.IO;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public class MainWindowViewModel : ViewModel
    {
        private static readonly Type[] MapItemTypes = typeof(MainWindowViewModel).Assembly.GetTypes().Where(t => t.BaseType.Name == typeof(MapItemViewModel<>).Name).ToArray();

        private ViewModel _selectedItem;
        public ViewModel SelectedItem
        {
            get => _selectedItem;
            set => Set(() => _selectedItem = value);
        }

        private ToolbarViewModel _menu;
        public ToolbarViewModel Menu
        {
            get => _menu;
            set => Set(() => _menu = value);
        }

        private PropertiesPanelViewModel _properties;
        public PropertiesPanelViewModel Properties
        {
            get => _properties;
            set => Set(() => _properties = value);
        }

        private Bitmap map;

        private int _mapWidth;
        public int MapWidth
        {
            get => _mapWidth;
            set => Set(() => _mapWidth = value);
        }

        private int _mapHeight;
        public int MapHeight
        {
            get => _mapHeight;
            set => Set(() => _mapHeight = value);
        }


        private ObservableCollection<MapItemViewModel> _levelItems = new ObservableCollection<MapItemViewModel>();
        public ObservableCollection<MapItemViewModel> LevelItems
        {
            get => _levelItems;
            set => Set(() => _levelItems = value);
        }

        private ObservableCollection<string> _itemNames = new ObservableCollection<string>();
        public ObservableCollection<string> ItemNames
        {
            get => _itemNames;
            set => Set(() => _itemNames = value);
        }

        private string _selectedItemNameToAdd;
        public string SelectedItemNameToAdd
        {
            get => _selectedItemNameToAdd;
            set => Set(() =>
            {
                _selectedItemNameToAdd = null;
                var selectedType = MapItemTypes.Single(t => t.Name == value);
                var item = (MapItemViewModel) Activator.CreateInstance(selectedType);
                var modelType = selectedType.BaseType.GenericTypeArguments.Single();
                var data = Activator.CreateInstance(modelType);
                selectedType.GetMethod("Load").Invoke(item, new[] { data });
                item.Name = modelType.Name;
                LevelItems.Add(item);
            });
        }

        private string _openLevel;
        public string OpenLevel
        {
            get => _openLevel;
            set => Set(() => _openLevel = value);
        }

        public ICommand SaveCommand { get; set; }
        public ICommand OpenCommand { get; set; }

        public MainWindowViewModel()
        {
            Properties = new PropertiesPanelViewModel();
            ItemNames.AddRange(MapItemTypes.Select(t => t.Name));

            OpenCommand = new ActionCommand(OpenAction);

            this.Subscribe<MapItemSelectedEvent>(args => SelectedItem = args.Vm);
            this.Subscribe<MapItemDeletedEvent>(args =>
            {
                LevelItems.Remove(args.Vm);
                if (SelectedItem == args.Vm)
                    SelectedItem = null;
            });
        }

        public void OpenAction()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Map Asset Files|*.bmp;*.map",
                CheckFileExists = true,
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName.EndsWith(".bmp"))
                {
                    this.Publish(new MapImagePickedEvent(dialog.FileName));
                    map = new Bitmap(File.ReadAllBytes(dialog.FileName));
                    MapWidth = (int)map.ImageData.Width;
                    xToLeftConverter.HalfWidth = MapWidth / 2;
                    MapHeight = (int)map.ImageData.Height;
                    yToTopConverter.HalfHeight = MapHeight / 2;
                }
            }
        }
    }
}
