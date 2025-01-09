using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MooreM.UserControls.Charts.Classes
{
    public class Channel
    {
        #region Private PMEs

        private bool _enabled;
        private System.Windows.Media.Color _Colour;
        private bool _DrawFaintLine;
        protected byte _PenIndicatorTransparancy = 50;
        private double _Offset;

        #endregion

        #region Events

        public delegate void ChannelDirty();

        public event ChannelDirty Dirty;

        #endregion

        #region Public PMEs

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; RaiseEvent_Dirty(); }
        }

        public System.Windows.Media.Color Colour
        {
            get { return _Colour; }
            set { _Colour = value; RaiseEvent_Dirty(); }
        }

        private Pen _Pen;

        public Pen Pen
        {
            get { return _Pen; }
        }

        public Pen PenIndicator
        {
            get { return new Pen(new SolidColorBrush(System.Windows.Media.Color.FromArgb(_PenIndicatorTransparancy, _Colour.R, _Colour.G, _Colour.B)), 1); }
        }

        public byte PenIndiactorTransparancy
        {
            get { return _PenIndicatorTransparancy; }
            set { _PenIndicatorTransparancy = value; RaiseEvent_Dirty(); }
        }

        public bool DrawFaintLine
        {
            get { return _DrawFaintLine; }
            set { _DrawFaintLine = value; }
        }

        public double Offset
        {
            get { return _Offset; }
            set { _Offset = value; }
        }

        #endregion

        #region Constructors

        public Channel(bool enabled, System.Windows.Media.Color colour)
        {
            Enabled = enabled;
            Colour = colour;
            _Pen = new Pen(new SolidColorBrush(_Colour), 1);
        }

        #endregion

        #region Events 

        protected void RaiseEvent_Dirty()
        {
            if (Dirty != null) { Dirty(); }
        }

        #endregion
    }
}
