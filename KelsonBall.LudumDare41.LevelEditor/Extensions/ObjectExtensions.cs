namespace KelsonBall.LudumDare41.LevelEditor.Extensions
{
    public static class ObjectExtensions
    {
        public static void TrySetProperty<T>(this object item, string name, T set)
        {
            var p = item.GetType().GetProperty(name);
            if (p == null)
                return;
            p.GetSetMethod().Invoke(item, new object[] { set });
        }

        public static T TryGetProperty<T>(this object item, string name, T @default)
        {
            var p = item.GetType().GetProperty(name);
            if (p == null)
                return @default;
            return (T)p.GetGetMethod().Invoke(item, new object[0]);
        }
    }

}
