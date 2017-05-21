using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AimlBotUI.Infrastructure.Dialogs
{
    public interface IDialog
    {
        DialogViewModel CreateDialog();

        DialogViewModel CreateDialog(DialogType dialogType);

        DialogViewModel CreateDialog(DialogType dialogType, string message);

        DialogViewModel CreateDialog(DialogType dialogType, string errorMessage, ImageSource imageSource);
    }
}
