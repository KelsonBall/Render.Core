using System;
using System.Collections.Generic;

namespace RenderCore.Game
{
    public class Property<TValue>
    {
        private TValue _value;
        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke(this);
            }
        }

        public event Action<Property<TValue>> Changed;

        public Guid Id { get; internal set; }

        private readonly HashSet<string> _tags = new HashSet<string>();
        public HashSet<string> Tags { get => _tags; }

        public GameObject Parent { get; internal set; }

        public TValue Get() => Value;
        public void Set(TValue value) => Value = value;
    }
}
