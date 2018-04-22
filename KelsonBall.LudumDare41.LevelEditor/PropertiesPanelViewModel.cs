using KelsonBall.LudumDare41.LevelEditor.Events;
using PubSub;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public class PropertiesPanelViewModel : ViewModel
    {
        private ViewModel _selectedItem;
        public ViewModel SelectedItem
        {
            get => _selectedItem;
            set => Set(() => _selectedItem = value);
        }

        public PropertiesPanelViewModel()
        {
            this.Subscribe<MapItemSelectedEvent>(args =>
            {
                App.Current.Dispatcher.Invoke(() => SelectedItem = args.Vm);
            });
            this.Subscribe<MapItemDeletedEvent>(args =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (SelectedItem == args.Vm)
                        SelectedItem = null;
                });
            });
        }
    }
}
