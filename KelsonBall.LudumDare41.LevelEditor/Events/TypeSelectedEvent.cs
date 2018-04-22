namespace KelsonBall.LudumDare41.LevelEditor.Events
{
    public class TypeSelectedEvent
    {
        public string TypeName;
        public TypeSelectedEvent(string type) => TypeName = type;
    }
}
