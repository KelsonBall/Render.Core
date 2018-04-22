using KelsonBall.LudumDare41.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KelsonBall.LudumDare41.MetaTool
{
    public class Program
    {
        public static Type[] Types = typeof(Ball).Assembly.GetTypes().Where(t => t.Namespace.EndsWith("Models")).ToArray();

        /// <summary>
        /// Write some xaml
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args)
        {
            List<Task> tasks = new List<Task>();
            DirectoryInfo dir = new DirectoryInfo(@"..\..\..\..\KelsonBall.LudumDare41.LevelEditor");
            if (!dir.Exists)
                Directory.CreateDirectory(dir.FullName);
            tasks.Add(File.WriteAllTextAsync(Path.Combine(dir.FullName, "PropertiesPanelView.xaml"), await new PropertiesPanelTemplate().GetTemplateAsync()));
            dir = new DirectoryInfo(@"..\..\..\..\KelsonBall.LudumDare41.LevelEditor\Models");
            foreach (var type in Types)
            {
                var template = await new ViewModelTemplate(type).GetTemplateAsync();
                tasks.Add(File.WriteAllTextAsync(Path.Combine(dir.FullName, $"{type.Name}ViewModel.cs"), template));
            }
            foreach (var task in tasks)
                await task;
        }
    }
}
