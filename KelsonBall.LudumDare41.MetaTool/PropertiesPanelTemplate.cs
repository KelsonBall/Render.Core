using System.Text;
using System.Threading.Tasks;

namespace KelsonBall.LudumDare41.MetaTool
{
    public class PropertiesPanelTemplate : Template
    {
        public PropertiesPanelTemplate() : base()
        {

        }

        public override async Task<string> GetTemplateAsync()
        {
            return await Task.Run(async () => new StringBuilder()
                    .AppendLine($"<UserControl x:Class = \"KelsonBall.LudumDare41.LevelEditor.PropertiesPanelView\"")
                    .AppendLine($"  xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"")
                    .AppendLine($"  xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"")
                    .AppendLine($"  xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"")
                    .AppendLine($"  xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"")
                    .AppendLine($"  xmlns:local=\"clr-namespace:KelsonBall.LudumDare41.LevelEditor\"")
                    .AppendLine($"  xmlns:models=\"clr-namespace:KelsonBall.LudumDare41.Items.Models;assembly=KelsonBall.LudumDare41.Items\"")
                    .AppendLine($"  mc:Ignorable=\"d\" Background=\"White\"")
                    .AppendLine($"  d:DesignHeight=\"450\" d:DesignWidth=\"800\">")
                    .AppendLine(await new XamlDataTemplatesTemplate(t => new PropertyPanelItemTemplate(t)).GetTemplateAsync())
                    .AppendLine("   <Grid>")
                    .AppendLine("       <ContentPresenter Content=\"{Binding SelectedItem}\" />")
                    .AppendLine("   </Grid>")
                    .AppendLine("</UserControl>")
                    .ToString());
        }
    }
}
