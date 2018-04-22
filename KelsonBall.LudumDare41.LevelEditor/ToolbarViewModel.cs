using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KelsonBall.LudumDare41.LevelEditor.Events;
using KelsonBall.LudumDare41.LevelEditor.Extensions;
using KelsonBall.LudumDare41.LevelEditor.Utils;
using PubSub;
using Render.Core.Vectors;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public class ToolbarViewModel : ViewModel
    {
        public abstract class Tool
        {
            public abstract void Click(Rektor at);

            public abstract void Finish();
        }

        public class AddTool : Tool
        {
            public string LastTypeAdded = null;

            public AddTool()
            {
                this.Subscribe<TypeSelectedEvent>(args => LastTypeAdded = args.TypeName);
            }

            public override void Click(Rektor at)
            {
                if (string.IsNullOrEmpty(LastTypeAdded))
                    return;
                var item = MainWindowViewModel.GetBlankItemByTypeName(LastTypeAdded);

                item.TrySetProperty("X", at.X);
                item.TrySetProperty("Y", at.Y);
                item.TrySetProperty("Width", 10);
                item.TrySetProperty("Height", 10);
                item.TrySetProperty("Radius", 10);

                this.Publish(new GameObjectAddedEvent(item));
            }

            public override void Finish()
            {
            }
        }

        public class MoveTool : Tool
        {
            MapItemViewModel item = null;

            public MoveTool()
            {
                this.Subscribe<MapItemSelectedEvent>(args => item = args.Vm);
            }

            public override void Click(Rektor at)
            {
                if (item != null)
                {
                    item.TrySetProperty("X", at.X);
                    item.TrySetProperty("Y", at.Y);
                }
            }

            public override void Finish()
            {
                this.Publish(new MapItemSelectedEvent(null));
            }
        }

        public class DrawBorderTool : Tool
        {
            int count = 0;
            public Rektor? PreviousClick = null;

            public override void Click(Rektor at)
            {
                var item = new PlayerSpawnViewModel();
                item.Load(new Items.Models.PlayerSpawn());
                item.Name = "Click Point";
                item.X = at.X;
                item.Y = at.Y;
                this.Publish(new GameObjectAddedEvent(item));

                if (PreviousClick.HasValue)
                {
                    var length = (at - PreviousClick.Value).Magnitude();
                    var direction = (at - PreviousClick.Value).Unit();
                    var midpoint = direction * (length / 2) + PreviousClick.Value;
                    var rectangle = new RectangleTerrainViewModel();
                    rectangle.Load(new Items.Models.RectangleTerrain());
                    rectangle.Name = $"TerrainBorderItem{count++}";
                    rectangle.Width = length;
                    rectangle.Height = 10;
                    rectangle.XDisplayOffset -= length / 2;
                    rectangle.X = midpoint.X;
                    rectangle.Y = midpoint.Y;
                    rectangle.Rotation = 180 * direction.Angle() / Math.PI;
                    this.Publish(new GameObjectAddedEvent(rectangle));
                }
                PreviousClick = at;
            }

            public override void Finish()
            {
                PreviousClick = null;
            }
        }

        public Tool SelectedTool { get; private set; }

        public ICommand AddToolSelectedCommand { get; set; }
        public ICommand MoveToolSelectedCommand { get; set; }
        public ICommand DrawToolSelectedCommand { get; set; }


        public ToolbarViewModel()
        {
            var add = new AddTool();
            var move = new MoveTool();
            var draw = new DrawBorderTool();
            AddToolSelectedCommand = new ActionCommand(SetTool(() => add));
            MoveToolSelectedCommand = new ActionCommand(SetTool(() => move));
            DrawToolSelectedCommand = new ActionCommand(SetTool(() =>
            {
                draw.PreviousClick = null;
                return draw;
            }));

            this.Subscribe<MapClickedAtEvent>(at =>
            {
                SelectedTool?.Click(at.Point);
            });
        }

        private Action SetTool(Func<Tool> tool)
        {
            return () =>
            {
                if (SelectedTool != null)
                    SelectedTool.Finish();
                SelectedTool = tool();
            };
        }
    }
}
