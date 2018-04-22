using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KelsonBall.LudumDare41.MetaTool
{
    public class XamlDataTemplatesTemplate : Template
    {
        private readonly Func<Type, Template> inner;
        public XamlDataTemplatesTemplate(Func<Type, Template> innerTemplate) : base()
        {
            inner = innerTemplate;
        }

        public override async Task<string> GetTemplateAsync()
        {
            return await Task.Run(async () => new StringBuilder()
                .AppendLine($"")
                .AppendLine($"  <UserControl.Resources>")
                .AppendLine($"{AllTemplates()}")
                .AppendLine($"  </UserControl.Resources>")
                .ToString());
        }

        public string AllTemplates()
            => string.Join("", Program.Types.Select(t => new StringBuilder()
                .AppendLine($"      <DataTemplate DataType=\"{{x:Type local:{t.Name}ViewModel}}\">")
                .AppendLine($"          <StackPanel>")
                .AppendLine($"{inner(t).GetTemplateAsync().Result}")
                .AppendLine($"          </StackPanel>")
                .AppendLine($"      </DataTemplate>")
                .ToString()));
    }
}
