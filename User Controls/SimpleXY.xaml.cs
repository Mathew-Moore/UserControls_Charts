using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MooreM.UserControls.Charts
{
    /// <summary>
    /// Interaction logic for SimpleXY.xaml
    /// </summary>
    public partial class SimpleXY : UserControl
    {

        #region Fields
        private Color color_BackgroundGrid = System.Windows.Media.Color.FromRgb(192, 192, 192);
        private Pen? pen_Background_Grid = new Pen(new SolidColorBrush(System.Windows.Media.Color.FromRgb(192, 192, 192)), 1);
        private Pen pen2 = new Pen(new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)), 1);
        protected Color color_background = Color.FromRgb(230, 230, 120);
        protected System.Windows.Rect rectangle = new Rect() { Width = 100, Height = 100 };
        protected int _X;
        protected int _Y;

        protected int _ChartOffset_Width = 20;

        public event MooreM.Common.Libraries.CommonLibrary.Delegates.delDiagnostics Diagnostics;
        //
        //    protected bool _SnapToGrid = true;
        #endregion

        #region Public Properties

        public Color Pen_BackgroundGrid
        {
            get { return color_BackgroundGrid; }
            set
            {
                color_BackgroundGrid = value;
                pen_Background_Grid = new Pen(new SolidColorBrush(color_BackgroundGrid), 1);
                this.InvalidateVisual();
            }
        }

        #endregion


        public SimpleXY()
        {
            InitializeComponent();
            InvalidateVisual();
            Channel_0.Dirty += Channel_0_Dirty;

        }

        private void Channel_0_Dirty()
        {
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Draw_BackgroundGrid(drawingContext);
            if (_Channel_0.Enabled) { Draw_Zero_0(drawingContext); }
            if (_Channel_1.Enabled) { Draw_Zero_1(drawingContext); }
        }

        #region Methods to  find Widths / Heights

        protected int GetHeight()
        {
            double _Double_H = ActualHeight % 20;
            double _Blocks20_H = ActualHeight - _Double_H;
            return (int)_Blocks20_H;
        }

        protected int GetWidth()
        {
            double _Double_W = ActualWidth % 20;
            double _Blocks20_W = ActualWidth - _Double_W;
            return (int)_Blocks20_W;
        }

        protected int GetHalf_Height()
        {
            double d = (double)GetHeight() / 2;
            return (int)d;
        }

        protected int GetHalf_Width()
        {
            double d = (double)GetWidth() / 2;
            return (int)d;
        }

        #endregion

        protected void Draw_Zero_0(DrawingContext drawingContext)
        {
            if (_Channel_0.DrawFaintLine)
            {
                Point _Start_WithOffset = new Point(_ChartOffset_Width, GetHalf_Height());
                Point _End_WithOffset = new Point(GetWidth(), GetHalf_Height());
                drawingContext.DrawLine(_Channel_0.PenIndicator, _Start_WithOffset, _End_WithOffset);
            }
            Point _Start = new Point(5, GetHalf_Height());
            Point _End = new Point(_ChartOffset_Width - 5, GetHalf_Height());
            drawingContext.DrawLine(_Channel_0.Pen, _Start, _End);

        }

        protected void Draw_Zero_1(DrawingContext drawingContext)
        {
            if (_Channel_1.DrawFaintLine)
            {
                Point _Start_WithOffset = new Point(_ChartOffset_Width, (GetHalf_Height() + _Channel_1.Offset));
                Point _End_WithOffset = new Point(GetWidth(), (GetHalf_Height() + _Channel_1.Offset));
                drawingContext.DrawLine(_Channel_0.PenIndicator, _Start_WithOffset, _End_WithOffset);
            }
            Point _Start = new Point(5, (GetHalf_Height() + _Channel_1.Offset));
            Point _End = new Point(_ChartOffset_Width - 5, (GetHalf_Height() + _Channel_1.Offset));
            drawingContext.DrawLine(_Channel_1.Pen, _Start, _End);

        }

        protected void Draw_BackgroundGrid(DrawingContext drawingContext)
        {

            int _Height = GetHeight();
            int _Width = GetWidth();

            //Draw a set of lines across the chart area
            for (int i = _ChartOffset_Width; i < (_Width + 1); i += 10)
            {
                Point p1 = new Point(i, 0);
                Point p2 = new Point(i, _Height);
                drawingContext.DrawLine(pen_Background_Grid, p1, p2);

            }

            //Draw a set of lines down the chart area
            for (int i = (int)(_Height + 1); i > 0; i -= 10)
            {
                Point p1 = new Point(_ChartOffset_Width, i);
                Point p2 = new Point(_Width, i);
                drawingContext.DrawLine(pen_Background_Grid, p1, p2);

            }

        }

        private void Grid_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            InvalidateVisual();
        }

        protected void RaiseEvent_Diagnostics(string Message)
        {
            if (Diagnostics != null) { Diagnostics(this, new Common.Libraries.CommonLibrary.EventHandlers.ClsEventHandler_Diagnostics(DateTime.Now, Message)); }
        }

        public int ChartOffset { get { return _ChartOffset_Width; } set { _ChartOffset_Width = value; } }

        #region Public Channel Enables
        private MooreM.UserControls.Charts.Classes.Channel[] _Channel;

       public MooreM.UserControls.Charts.Classes.Channel this[int index]
        {
            get { return _Channel[index]; } set { _Channel[index] = value; }
        }

        private MooreM.UserControls.Charts.Classes.Channel _Channel_0 = new Classes.Channel(false, Color.FromArgb(255, 255, 0, 0));

        public MooreM.UserControls.Charts.Classes.Channel Channel_0
        {
            get { return _Channel_0; }
            set { _Channel_0 = value; }
        }

        private MooreM.UserControls.Charts.Classes.Channel _Channel_1 = new Classes.Channel(false, Color.FromArgb(255, 0, 255, 0));

        public MooreM.UserControls.Charts.Classes.Channel Channel_1
        {
            get { return _Channel_1; }
            set { _Channel_1 = value; }
        }

        #endregion
    }
}
