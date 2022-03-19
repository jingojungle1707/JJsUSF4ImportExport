using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JJsUSF4ImportExport
{
    public class TimedFeedbackLabel : Label
    {
        public TimedFeedbackLabel(string text, int time, TreeView tv, TreeNode n)
        {
            Timer disposeTimer = new Timer();
            disposeTimer.Interval = time;
            disposeTimer.Tick += new EventHandler(DisposeLabel);
            disposeTimer.Start();

            Text = text;
            TextAlign = ContentAlignment.MiddleLeft;
            AutoSize = true;
            BackColor = Color.LightSeaGreen;


            using (Graphics g = CreateGraphics())
            {
                int offset = 24;
                
                int width = (int)g.MeasureString(Text, Font).Width + offset;
                int height = (int)g.MeasureString(Text, Font).Height;
                Height = height;
                Location = new Point(tv.Bounds.Right - width, (n.Bounds.Top + n.Bounds.Bottom) / 2 - height / 2);
            }
            tv.Controls.Add(this);
            Show();
        }

        private void DisposeLabel(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
