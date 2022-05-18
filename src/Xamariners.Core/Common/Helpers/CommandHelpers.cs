using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Xamariners.Core.Common.Helpers
{
    public static class CommandHelpers
    {
        public static void SafeExecuteCommand<T>(this ICommand command, object bindingContext)
        {
            if (command.CanExecute((T)bindingContext))
            {
                command.Execute((T)bindingContext);
            }
        }
    }
}
