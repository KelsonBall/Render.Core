﻿using Render.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RenderCore.Game
{
    public class GameObject : IEnumerable<GameObject>
    {
        #region Properties
        public string Name;

        #endregion

        public GameObject(Action<GameObject> configure)
        {
            configure(this);
        }

        public Guid Id { get; internal set; } = Guid.NewGuid();

        public HashSet<string> Tags { get; } = new HashSet<string>();

        public EventBus Events { get; internal set; } = new EventHome().NewBus();

        protected GameObject _root;
        public Scene Scene => _root as Scene;

        private GameObject _parent;
        public GameObject Parent
        {
            get => _parent;
            private set
            {
                _parent = value;
                _root = value.Scene;
            }
        }

        private readonly IList<GameObject> _children = new List<GameObject>();
        public IEnumerable<GameObject> Children => _children.AsEnumerable();

        public IEnumerable<GameObject> Descendents
        {
            get
            {
                foreach (var child in Children)
                {
                    yield return child;
                    foreach (var desc in child.Descendents)
                        yield return desc;
                }
            }
        }

        public virtual void Add(GameObject child)
        {
            Events.AdoptBus(child.Events);
            foreach (var descendent in child.Descendents)
                Events.AdoptBus(descendent.Events);
            if (child.Parent != null)
                child.Parent._children.Remove(child);
            child.Parent = this;
            _children.Add(child);
            if (this is Scene scene)
            {
                _root = scene;
                foreach (var desc in Descendents)
                    desc._root = scene;
            }
            foreach (var behavior in child.Behaviors)
                behavior.Load?.Invoke();
        }

        public void Remove()
        {
            Parent._children.Remove(this);
            Events.Remove();
        }

        private readonly Dictionary<string, Behavior> _behaviors = new Dictionary<string, Behavior>();
        public IEnumerable<Behavior> Behaviors => _behaviors.Values.AsEnumerable();

        public void AddBehavior(Behavior behavior)
        {
            _behaviors.Add(behavior.Name, behavior);
            if (Parent != null)
                behavior.Load?.Invoke();
        }

        private Dictionary<string, object> _properties;
        public Property<TValue> Property<TValue>(string name)
        {
            if (_properties == null)
                _properties =
                    GetType()
                        .GetRuntimeFields()
                        .Where(f => f.FieldType == typeof(Property<>)
                        .MakeGenericType(new Type[] { f.FieldType.GenericTypeArguments.Single() }))
                        .ToDictionary(f => f.Name, f => f.GetValue(this));

            if (_properties.ContainsKey(name))
                return (Property<TValue>)_properties[name];

            throw new MissingMemberException($"{this.GetType().Name} does not contain a property {name} of type {typeof(TValue).Name}");
        }

        public void RemoveBehavior(string name) => _behaviors.Remove(name);

        public virtual void Render(ICanvas canvas)
        {
            foreach (var child in Children)
                child.Render(canvas);
        }

        public virtual void Update(TimeSpan time)
        {
            foreach (var child in Children)
                child.Update(time);
            foreach (var behavior in Behaviors)
                behavior.Update?.Invoke(time);
        }

        public IEnumerator<GameObject> GetEnumerator() => Children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
