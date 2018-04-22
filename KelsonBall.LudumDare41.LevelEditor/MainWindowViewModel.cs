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
using Render.Core;
using KelsonBall.LudumDare41.Assets;

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
        public ToolbarViewModel Toolbar
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

        private ObservableCollection<string> _assetNames = new ObservableCollection<string>();
        public ObservableCollection<string> AssetNames
        {
            get => _assetNames;
            set => Set(() => _assetNames = value);
        }

        private string _selectedItemNameToAdd;
        public string SelectedItemNameToAdd
        {
            get => _selectedItemNameToAdd;
            set => Set(() =>
            {
                _selectedItemNameToAdd = value;
                this.Publish(new TypeSelectedEvent(value));
            });
        }

        public static MapItemViewModel GetBlankItemByTypeName(string value)
        {
            var selectedType = MapItemTypes.Single(t => t.Name == value);
            var item = (MapItemViewModel)Activator.CreateInstance(selectedType);
            var modelType = selectedType.BaseType.GenericTypeArguments.Single();
            var data = Activator.CreateInstance(modelType);
            selectedType.GetMethod("Load").Invoke(item, new[] { data });
            item.Name = modelType.Name;
            return item;
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
            Toolbar = new ToolbarViewModel();
            ItemNames.AddRange(MapItemTypes.Select(t => t.Name));
            ResourceManager.RegisterResourceAssembly("Assets", "KelsonBall.LudumDare41.Assets", Art.Assembly);
            AssetNames.AddRange()
            OpenCommand = new ActionCommand(OpenAction);

            this.Subscribe<MapItemSelectedEvent>(args => SelectedItem = args.Vm);
            this.Subscribe<MapItemDeletedEvent>(args =>
            {
                LevelItems.Remove(args.Vm);
                if (SelectedItem == args.Vm)
                    SelectedItem = null;
            });
            this.Subscribe<GameObjectAddedEvent>(args =>
            {
                LevelItems.Add(args.Payload);
                args.Payload.XDisplayOffset += xToLeftConverter.HalfWidth;
                args.Payload.YDisplayOffset += yToTopConverter.HalfHeight;
                args.Payload.XAdjusted = args.Payload.TryGetProperty("X", 0d) + args.Payload.XDisplayOffset;
                args.Payload.YAdjusted = args.Payload.TryGetProperty("Y", 0d) + args.Payload.YDisplayOffset;

                this.Publish(new MapItemSelectedEvent(args.Payload));
            });
        }

        public void OpenAction()
        {

        }

        public void SaveAction()
        {
            DirectoryInfo dir = new DirectoryInfo(@"..\..\..\..\KelsonBall.LudumDare41.Assets\Levels");
        }
    }
}
