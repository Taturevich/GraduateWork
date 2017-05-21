using AimlBotUI.Shared;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AimlBotUI.Infrastructure.Dialogs
{
    public class DialogViewModel : ScreenBase, IViewModel, IDialog
    {
        private const DialogType DefaultDialogType = DialogType.Information;

        private const string DefaultMessage = "Information";

        public DialogViewModel CreateDialog()
        {
            DialogType = DefaultDialogType;
            Message = DefaultMessage;
            ImageSource = LoadIcon(DefaultDialogType);
            return this;
        }

        public DialogViewModel CreateDialog(DialogType dialogType)
        {
            DialogType = dialogType;
            Message = DefaultMessage;
            ImageSource = LoadIcon(DialogType);
            return this;
        }

        public DialogViewModel CreateDialog(DialogType dialogType, string message)
        {
            DialogType = dialogType;
            Message = message;
            ImageSource = LoadIcon(DialogType);
            return this;
        }

        public DialogViewModel CreateDialog(DialogType dialogType, string message, ImageSource imageSource)
        {
            DialogType = dialogType;
            Message = message;
            ImageSource = imageSource;
            return this;
        }

        /// <summary>
        /// flag for detecting confirmation dialog
        /// </summary>
        public bool IsConfirmationDialog => DialogType == DialogType.Confirmation;

        /// <summary>
        /// output result of confirmation dialog
        /// </summary>

        public DialogType DialogType { get; set; }

        public string Message { get; set; }

        public ImageSource ImageSource { get;set; }

        public void OkResult()
        {
            IsConfirm = true;
            TryClose(IsConfirm);
        }

        public void CancelResult()
        {
            IsConfirm = false;
            TryClose(IsConfirm);
        }

        private ImageSource LoadIcon(DialogType dialogType)
        {
            IntPtr icon = new IntPtr();

            var result = Imaging.CreateBitmapSourceFromHIcon(icon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            return result; 
        }
    }
}
