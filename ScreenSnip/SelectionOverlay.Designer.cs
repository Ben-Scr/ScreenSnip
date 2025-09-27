namespace ScreenSnip
{
    partial class SelectionOverlay
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // SelectionOverlay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 450);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "SelectionOverlay";
            Opacity = 0.1D;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            TransparencyKey = Color.Transparent;
            Load += SelectionOverlay_Load;
            KeyDown += SelectionOverlay_KeyDown;
            MouseDown += SelectionOverlay_MouseDown;
            MouseMove += SelectionOverlay_MouseMove;
            MouseUp += SelectionOverlay_MouseUp;
            ResumeLayout(false);
        }

        #endregion
    }
}
