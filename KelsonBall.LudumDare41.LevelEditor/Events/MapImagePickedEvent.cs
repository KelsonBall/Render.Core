namespace KelsonBall.LudumDare41.LevelEditor.Events
{
    public class MapImagePickedEvent
    {
        public readonly string Uri;

        public MapImagePickedEvent(string uri) => Uri = uri;
    }

    public abstract class PayloadEvent<T>
    {
        public readonly T Payload;

        public PayloadEvent(T payload) => Payload = payload;
    }

    public class GameObjectAddedEvent : PayloadEvent<MapItemViewModel>
    {
        public GameObjectAddedEvent(MapItemViewModel payload) : base(payload)
        {
        }
    }


}
