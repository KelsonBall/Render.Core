using System;
using System.Threading.Tasks;

namespace KelsonBall.LudumDare41.MetaTool
{
    public class ViewModelTemplate : Template
    {
        const string q = "\"";
        public ViewModelTemplate(Type type) : base(type)
        {

        }

        public override async Task<string> GetTemplateAsync()
        {
            return await Task.Run(async () =>
$@"
using System;
using System.Text;
using System.Linq;
using KelsonBall.LudumDare41.Items.Models;

namespace KelsonBall.LudumDare41.LevelEditor
{{
    public class {Class.Name}ViewModel : MapItemViewModel<{Class.Name}>
    {{
        {await new ViewModelPropertiesTemplate(Class).GetTemplateAsync()}
        
        public string Tags
        {{
            get => string.Join({q}, {q}, data.Tags);
            set => Set(() => data.Tags = value.Split(',').Select(t => t.Trim()).ToArray());
        }}

        public string CollisionGroups
        {{
            get => string.Join({q}, {q}, data.CollisionGroups);
            set => Set(() => data.CollisionGroups = value.Split(',').Select(t => t.Trim()).ToArray());
        }}

    }}
}}
");
        }
    }
}
