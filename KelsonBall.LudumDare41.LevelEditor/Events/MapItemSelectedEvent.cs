namespace KelsonBall.LudumDare41.LevelEditor.Events
{
    public abstract class MapItemEvent
    {
        public readonly MapItemViewModel Vm;

        public MapItemEvent(MapItemViewModel vm) => Vm = vm;
    }

    public class MapItemSelectedEvent : MapItemEvent
    {
        public MapItemSelectedEvent(MapItemViewModel vm) : base(vm) { }
    }

    public class MapItemDeletedEvent : MapItemEvent
    {
        public MapItemDeletedEvent(MapItemViewModel vm) : base(vm) { }
    }
}
