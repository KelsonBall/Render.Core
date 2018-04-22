using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KelsonBall.LudumDare41.LevelEditor.Events
{
    public class MapImagePickedEvent
    {
        public readonly string Uri;

        public MapImagePickedEvent(string uri) => Uri = uri;
    }
}
