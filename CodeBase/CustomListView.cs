using System.Drawing;
using System.Windows.Forms;

namespace CodeBase
{
    internal class MyListView : ListView
    {
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