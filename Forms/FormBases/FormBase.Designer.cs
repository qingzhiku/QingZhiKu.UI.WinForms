using Microsoft.Win32;
using System.Drawing;

namespace System.Windows.Forms
{
    partial class FormBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (disposing && (SystemMenu != null))
            {
                SystemMenu.Dispose();
            }

            if (disposing && (_marginRectangle != MarginRectangle.Empty))
            {
                _marginRectangle.Dispose();
            }
            
            if (disposing)
            {
                SystemEvents.UserPreferenceChanged -= UserPreferenceChanged;
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Text = "NoneTitleForm";
        }

        #endregion
    }
}