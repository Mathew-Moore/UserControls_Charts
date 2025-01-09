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
using System.Windows.Threading;

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
            _Channel = new Classes.Channel[6];
         //   _Channel[0] = new Classes.Channel(false, Color.FromArgb(255, 255, 0, 0));
         //   _Channel[1] = new Classes.Channel(false, Color.FromArgb(255, 0, 255, 0));


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

        #region GUI and Overrides

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Draw_BackgroundGrid(drawingContext);
            Draw_Zeros(drawingContext);
        }

        public void Refresh()
        {
            this.Dispatcher.Invoke(() => { InvalidateVisual(); });
        }


        /// <summary>
        /// Method to draw Lines in colours at a channels zero point
        /// </summary>
        /// <remarks>This can include offsets</remarks>
        /// <param name="drawingContext"></param>
        protected void Draw_Zeros(DrawingContext drawingContext)
        {
            for (int i = 0; i < 6; i++)
            {
                if (_Channel[i] != null && _Channel[i].Enabled)
                {
                    int _OffsetValue = (int)(GetHalf_Height() - _Channel[i].Offset);    //substracted because of 0 being at the top
                    if (_Channel[i].DrawFaintLine)
                    {  // draw zero line on graph if required
                        Point _Start_WithOffset = new Point(_ChartOffset_Width, _OffsetValue);
                        Point _End_WithOffset = new Point(GetWidth(), _OffsetValue);
                        drawingContext.DrawLine(_Channel[i].PenIndicator, _Start_WithOffset, _End_WithOffset);
                        RaiseEvent_Diagnostics("Offset: " + i.ToString() + " " + _OffsetValue.ToString());
                    }
                    //draw zero line at the side of the graph
                    Point _Start = new Point(5, _OffsetValue);
                    Point _End = new Point(_ChartOffset_Width - 5, _OffsetValue);
                    drawingContext.DrawLine(_Channel[i].Pen, _Start, _End);
                }
            }
        }

        /// <summary>
        /// Method to draw a grid of 10x10 pixels
        /// </summary>
        /// <param name="drawingContext"></param>
        protected void Draw_BackgroundGrid(DrawingContext drawingContext)
        {
            int _Height = GetHeight();
            int _Width = GetWidth();

            //Draw a set of lines across the chart area
            for (int i = _ChartOffset_Width; i < (_Width); i += 10)
            {
                Point p1 = new Point(i, 0);
                Point p2 = new Point(i, _Height);
                drawingContext.DrawLine(pen_Background_Grid, p1, p2);
            }

            //Draw a set of lines down the chart area
            for (int i = (int)(_Height ); i > 0; i -= 10)
            {
                Point p1 = new Point(_ChartOffset_Width, i);
                Point p2 = new Point(_Width, i);
                drawingContext.DrawLine(pen_Background_Grid, p1, p2);
            }

            //Draw a Center line
                Point p3 = new Point(0, GetHalf_Height());
                Point p4 = new Point(_Width, GetHalf_Height());
                drawingContext.DrawLine(pen_Background_Grid, p3, p4);
        }

        /// <summary>
        /// Event sink for when data is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            Refresh();
        }

        #endregion

        /// <summary>
        /// Helper method to call diagnostics event 
        /// </summary>
        /// <param name="Message">message to be displayed</param>
        protected void RaiseEvent_Diagnostics(string Message)
        {
            if (Diagnostics != null) { Diagnostics(this, new Common.Libraries.CommonLibrary.EventHandlers.ClsEventHandler_Diagnostics(DateTime.Now, Message)); }
        }

        /// <summary>
        /// Property which defines the left hand side gap to display zero line
        /// </summary>
        public int ChartOffset_Leftside
        {
            get { return _ChartOffset_Width; }
            set { _ChartOffset_Width = value; }
        }

        #region Public Channel 

        private MooreM.UserControls.Charts.Classes.Channel[] _Channel;

        public MooreM.UserControls.Charts.Classes.Channel this[int index]
        {
            get { return _Channel[index]; }
            set { _Channel[index] = value; }
        }

        public void Create(int Index, bool enabled, System.Windows.Media.Color colour)
        {
             _Channel[Index] = new Classes.Channel(enabled,colour);
            Refresh();
        }

        #endregion


    }
}
