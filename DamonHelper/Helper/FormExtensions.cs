// ***********************************************************************
// Solution         : MyselfTools
// Project          : DamonHelper
// File             : FormExtensions.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Windows.Forms;
using DamonHelper.sys;

namespace DamonHelper.Helper
{
    public static class FormExtensions
    {
        public static void ShowOutputForm(this OutputForm form, ref EventHandler eventHandler)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            eventHandler += form.TextEventHandler;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        /// <summary>
        ///     将内容输出到 Output 窗体
        /// </summary>
        public static void AppendMessage(this EventHandler eventHandler, Form form, string text)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler));
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            CustomEventArgs eventArgs = new CustomEventArgs
            {
                Text = text,
                Time = Time.Now
            };

            eventHandler.Invoke(form, eventArgs);
        }
    }
}