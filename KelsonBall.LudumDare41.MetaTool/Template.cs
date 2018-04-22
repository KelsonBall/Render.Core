using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace KelsonBall.LudumDare41.MetaTool
{
    public abstract class Template
    {
        public readonly Type Class;
        public readonly PropertyInfo[] Properties;

        public Template(Type type)
        {
            Class = type;
            Properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance ).ToArray();
        }

        public Template()
        {
            Class = null;
            Properties = null;
        }

        public abstract Task<string> GetTemplateAsync();
    }
}
