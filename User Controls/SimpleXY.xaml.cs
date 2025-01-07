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
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Draw_BackgroundGrid(drawingContext);
        }

        protected void Draw_BackgroundGrid(DrawingContext drawingContext)
        {
            for (int i = 0; i <ActualWidth; i += 10)
            {
                Point p1 = new Point(i, 0);
                Point p2 = new Point(i,this. ActualHeight);
                drawingContext.DrawLine(pen_Background_Grid, p1, p2);

            }

            for (int i = (int)ActualHeight; i > 0; i -= 10)
            {
                Point p1 = new Point(0, i);
                Point p2 = new Point(this.ActualWidth, i);
                drawingContext.DrawLine(pen_Background_Grid, p1, p2);

            }
        }

        private void Grid_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            InvalidateVisual();
        }
    }
}
