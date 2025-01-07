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
        protected Pen _Pen_Zero_0 = new Pen(new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)), 1);
        protected Color color_background = Color.FromRgb(230, 230, 120);
        protected System.Windows.Rect rectangle = new Rect() { Width = 100, Height = 100 };
        protected int _X;
        protected int _Y;

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

        //   public bool SnapToGrid { get { return _SnapToGrid; } set { _SnapToGrid = value; } }
        #endregion


        public SimpleXY()
        {
            InitializeComponent();
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Draw_BackgroundGrid(drawingContext);
            Draw_Zero_0(drawingContext);
        }

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

        protected void Draw_Zero_0(DrawingContext drawingContext)
        {
            Point _Start = new Point(0, GetHalf_Height());
            Point _End = new Point(GetWidth(), GetHalf_Height());

            drawingContext.DrawLine(_Pen_Zero_0, _Start, _End);
            RaiseEvent_Diagnostics(_Start.ToString());
            RaiseEvent_Diagnostics(_End.ToString());

        }

        protected void Draw_BackgroundGrid(DrawingContext drawingContext)
        {

            //   int _Half_H = (int)(_Blocks20_H / 2);

            int _Height = GetHeight();
            int _Width = GetWidth();

            //     int _Half_W = (int)(_Width / 2);


            // for (int i = 0; i <ActualWidth; i += 10)
            for (int i = 0; i < (_Width + 1); i += 10)
            {
                Point p1 = new Point(i, 0);
                Point p2 = new Point(i, _Height);
                drawingContext.DrawLine(pen_Background_Grid, p1, p2);

            }

            for (int i = (int)(_Height + 1); i > 0; i -= 10)
            {
                Point p1 = new Point(0, i);
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
    }
}
