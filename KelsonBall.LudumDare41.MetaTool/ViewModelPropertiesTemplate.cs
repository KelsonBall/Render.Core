using System;
using System.Linq;
using System.Threading.Tasks;

namespace KelsonBall.LudumDare41.MetaTool
{
    public class ViewModelPropertiesTemplate : Template
    {
        public ViewModelPropertiesTemplate(Type type) : base(type)
        {

        }

        public override async Task<string> GetTemplateAsync()
        {
            return await Task.Run(() => string.Join("", Properties.Select(p =>
            {
                if (p.PropertyType.GetMethods().Any(m => m.Name == "GetEnumerator"))
                    return "";
                else if (p.Name == "X" || p.Name == "Y")
                    return
                $@"
        public {p.PropertyType.FullName} {p.Name} 
        {{
            get => data.{p.Name};
            set => Set(() => 
            {{
                {p.Name}Adjusted = value + {p.Name}DisplayOffset;
                data.{p.Name} = value;
            }});
        }}
";
                else
                    return
                $@"
        public {p.PropertyType.FullName} {p.Name} 
        {{
            get => data.{p.Name};
            set => Set(() => data.{p.Name} = value);
        }}
";
            })));
        }
    }
}
