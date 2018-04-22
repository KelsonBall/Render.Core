using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KelsonBall.LudumDare41.MetaTool
{
    public class PropertyPanelItemTemplate : Template
    {
        public PropertyPanelItemTemplate(Type type) : base(type)
        {

        }

        public override async Task<string> GetTemplateAsync()
        {
            return await Task.Run(() => string.Join("", Properties.Select(p =>
                new StringBuilder()
                    .AppendLine($"              <Label>{p.Name}</Label>")
                    .AppendLine($"              {PropertyBox(1, p)}")
                    .ToString())));
        }

        public string PropertyBox(int tabs, PropertyInfo p)
        {
            string tab = string.Join("\t", Enumerable.Range(0, tabs).Select(i => ""));
            var text = new StringBuilder();
            if (p.PropertyType == typeof(int) || p.PropertyType == typeof(double))
                return text.AppendLine($"{tab}<TextBox Text=\"{{Binding {p.Name}, StringFormat=N}}\" />").ToString();
            if (p.PropertyType == typeof(string))
                return text.AppendLine($"{tab}<TextBox Text=\"{{Binding {p.Name}}}\" />").ToString();
            if (p.Name == "Tags")
                return text.AppendLine($"{tab}<TextBox Text=\"{{Binding Tags}}\" />").ToString();
            if (p.Name == "CollisionGroups")
                return text.AppendLine($"{tab}<TextBox Text=\"{{Binding CollisionGroups}}\" />").ToString();
            if (Program.Types.Any(t => t == p.PropertyType))
                return text.AppendLine($"{tab}<StackPanel Margin=\"8,2,2,2\"><Label>X</Label><TextBox Text=\"{{Binding X}}\" /><Label>Y</Label><TextBox Text=\"{{Binding Y}}\" /> </StackPanel>").ToString();
            //if (p.PropertyType.GetMethods().Any(m => m.Name == "GetEnumerator"))
            //    return new ItemsControlTemplate(p).GetTemplateAsync().Result;
            return "";
        }
    }

    public class ItemsControlTemplate : Template
    {
        private readonly PropertyInfo property;

        public ItemsControlTemplate(PropertyInfo p)
        {
            property = p;
        }

        public override async Task<string> GetTemplateAsync()
            => await Task.Run(() =>
                new StringBuilder()
                    .AppendLine($"              <ItemsControl ItemsSource=\"{{Binding {property.Name}}} />")
                    .ToString());

    }
}
