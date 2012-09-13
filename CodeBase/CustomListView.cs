using System.Drawing;
using System.Windows.Forms;

namespace CodeBase
{
    internal class MyListView : ListView
    {
        public MyListView()
        {
            //Activate double buffering
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc
            SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }

        protected override void WndProc(ref Message msg)
        {
            // Ignore mouse messages not in the client area 
            if (msg.Msg >= 0x201 && msg.Msg <= 0x209)
            {
                var pointMousePos = new Point(msg.LParam.ToInt32() & 0xffff, msg.LParam.ToInt32() >> 16);
                ListViewHitTestInfo lvhti = HitTest(pointMousePos);
                switch (lvhti.Location)
                {
                    case ListViewHitTestLocations.AboveClientArea:
                    case ListViewHitTestLocations.BelowClientArea:
                    case ListViewHitTestLocations.LeftOfClientArea:
                    case ListViewHitTestLocations.RightOfClientArea:
                    case ListViewHitTestLocations.None:
                        return;
                }
            }
            base.WndProc(ref msg);
        }
    }
}