// ***********************************************************************
// Solution         : MyselfTools
// Project          : DamonHelper
// File             : MessageBoxExtensions.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Windows.Forms;

namespace DamonHelper.Helper
{
    public static class MessageBoxExtensions
    {
        public static DialogResult Show(this Form form, string text)
        {
            return MessageBoxHelper.Show(form, text);
        }

        public static DialogResult Show(this Form form, string text, string caption)
        {
            return MessageBoxHelper.Show(form, text, caption);
        }

        public static DialogResult Show(this Form form, string text, string caption, MessageBoxButtons buttons)
        {
            return MessageBoxHelper.Show(form, text, caption, buttons);
        }

        public static DialogResult Show(this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBoxHelper.Show(form, text, caption, buttons, icon);
        }

        public static DialogResult Show(this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton)
        {
            return MessageBoxHelper.Show(form, text, caption, buttons, icon, defButton);
        }

        public static DialogResult Show(this Form form, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, MessageBoxOptions options)
        {
            return MessageBoxHelper.Show(form, text, caption, buttons, icon, defButton, options);
        }
    }
}