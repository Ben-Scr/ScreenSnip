using System.Windows.Forms;

namespace ScreenSnip
{
    public partial class SelectionOverlay : Form
    {
        private Screen.Selection selection;
        private Rectangle previousRect = Rectangle.Empty;
        private bool isDragging;

        public SelectionOverlay()
        {
            InitializeComponent();
        }

        private void SelectionOverlay_Load(object sender, EventArgs e)
        {
            Location = new Point(Screen.left, Screen.top);
            Size = new Size(Screen.width, Screen.height);
        }

        private void SelectionOverlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Application.Exit();
        }

        private void SelectionOverlay_MouseDown(object sender, MouseEventArgs e)
        {
            // Resets the current selection
            if (e.Button == MouseButtons.Right)
            {
                isDragging = false;
                ControlPaint.DrawReversibleFrame(previousRect, Color.Black, FrameStyle.Dashed);
                previousRect = Rectangle.Empty;
            }

            if (e.Button != MouseButtons.Left) return;

            isDragging = true;
            selection.p1 = MousePosition;
        }

        private void SelectionOverlay_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging) return;

            // Deletes the previous rectangle
            if (previousRect != Rectangle.Empty)
                ControlPaint.DrawReversibleFrame(previousRect, Color.Black, FrameStyle.Dashed);

            selection.p2 = Control.MousePosition;
            var x = Math.Min(selection.p1.X, selection.p2.X);
            var y = Math.Min(selection.p1.Y, selection.p2.Y);
            var w = Math.Abs(selection.p1.X - selection.p2.X);
            var h = Math.Abs(selection.p1.Y - selection.p2.Y);

            previousRect = new Rectangle(x, y, w, h);
            ControlPaint.DrawReversibleFrame(previousRect, Color.Black, FrameStyle.Dashed);
        }

        private void SelectionOverlay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !isDragging) return;

            // Deletes the previous rectangle
            if (previousRect != Rectangle.Empty)
                ControlPaint.DrawReversibleFrame(previousRect, Color.Black, FrameStyle.Dashed); // letzten Rahmen „löschen“

            isDragging = false;
            var snipRect = previousRect;
            previousRect = Rectangle.Empty;

            if (snipRect.Width > 0 && snipRect.Height > 0)
                ScreenshotUtils.TakeCutShot(snipRect);

            Application.Exit();
        }
    }
}
