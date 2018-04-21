using Render.Core.Input;
using System;
using System.Collections.Generic;

namespace RenderCore.Game
{
    public static class Events
    {
        public static string MousePress(MouseButton button) => $"MouseEvents.MousePressed.{button}";
        public static string MouseRelease(MouseButton button) => $"MouseEvents.MouseRelease.{button}";

        public const string MouseMoved = "MouseEvents.MouseMoved";
        public const string MouseDragged = "MouseEvents.MouseDragged";

        public static string KeyPress(Key key)
        {
            return $"KeyEvents.KeyPress.{key}";
        }

        public static string KeyRelease(Key key)
        {
            return $"KeyEvents.KeyRelease.{key}";
        }
    }

    internal class EventHome
    {
        internal readonly Dictionary<Guid, EventBus> busses = new Dictionary<Guid, EventBus>();
        internal readonly Dictionary<string, HashSet<EventBus>> channelMap = new Dictionary<string, HashSet<EventBus>>();

        internal EventBus NewBus()
        {
            var bus = new EventBus(this, Guid.NewGuid());
            busses.Add(bus.Id, bus);
            return bus;
        }
    }

    public class EventBus
    {
        internal readonly Guid Id = Guid.NewGuid();

        private EventHome home;

        internal readonly Dictionary<string, List<Action<object>>> Subscriptions = new Dictionary<string, List<Action<object>>>();

        internal EventBus(EventHome eventHome, Guid id)
        {
            Id = id;
            home = eventHome;
        }

        public void Subscribe(string channel, Action<object> function)
        {
            if (!Subscriptions.ContainsKey(channel))
                Subscriptions[channel] = new List<Action<object>>();
            if (!home.channelMap.ContainsKey(channel))
                home.channelMap[channel] = new HashSet<EventBus>();
            if (!home.channelMap[channel].Contains(this))
                home.channelMap[channel].Add(this);
            Subscriptions[channel].Add(function);
        }

        public void Publish(string channel, object args)
        {
            if (home.channelMap.ContainsKey(channel))
                foreach (var bus in home.channelMap[channel])
                    foreach (var func in bus.Subscriptions[channel])
                        func?.Invoke(args);
        }

        public void AdoptBus(EventBus bus)
        {
            bus.Remove();
            home.busses.Add(bus.Id, bus);
            foreach (var channel in bus.Subscriptions.Keys)
            {
                if (!home.channelMap.ContainsKey(channel))
                    home.channelMap[channel] = new HashSet<EventBus>();
                home.channelMap[channel].Add(bus);
            }
            bus.home = home;
        }

        internal EventBus NewBus() => home.NewBus();

        public void Remove()
        {
            foreach (var channel in Subscriptions.Keys)
                home.channelMap[channel].Remove(this);
            home.busses.Remove(Id);
        }
    }
}
