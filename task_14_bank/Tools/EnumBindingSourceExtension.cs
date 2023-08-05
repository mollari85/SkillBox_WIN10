using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace task_14_bank.Tools
{
    public class EnumBindingSourceExtension:MarkupExtension
    {
        public Type EnumType { get; private set; }

        public EnumBindingSourceExtension(Type enumType)        {
            if (enumType is null || !enumType.IsEnum)
                throw (new Exception("EnumType must nit be null and must be Enum"));
            this.EnumType = enumType;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            return Enum.GetValues(EnumType);
        }

    }
}
