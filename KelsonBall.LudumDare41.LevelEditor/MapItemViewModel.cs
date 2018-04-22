using KelsonBall.LudumDare41.Items.Models;
using System.Reflection;
using System.Windows.Input;
using PubSub;
using KelsonBall.LudumDare41.LevelEditor.Utils;
using KelsonBall.LudumDare41.LevelEditor.Events;
using KelsonBall.LudumDare41.Items;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public abstract class MapItemViewModel : ViewModel
    {
        private string _name;
        public virtual string Name
        {
            get => _name;
            set => Set(() => _name = value);
        }

        public ICommand SelectItemCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }

        public MapItemViewModel()
        {
            SelectItemCommand = new ActionCommand(() => this.Publish(new MapItemSelectedEvent(this)));
            RemoveItemCommand = new ActionCommand(() => this.Publish(new MapItemDeletedEvent(this)));
        }
    }


    public abstract class MapItemViewModel<T> : MapItemViewModel where T : GameItemModel
    {
        protected T data;

        public virtual void Load(T model)
        {
            data = model;
            foreach (var prop in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                OnPropertyChanged(prop.Name);
        }

        public virtual T Save() => data;

    }
}
