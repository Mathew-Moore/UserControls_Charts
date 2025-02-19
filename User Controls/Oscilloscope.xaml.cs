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
    /// Interaction logic for Oscilloscope.xaml
    /// </summary>
    public partial class Oscilloscope : UserControl
    {
        #region Fields

        private Color color_BackgroundGrid = System.Windows.Media.Color.FromRgb(192, 192, 192);
        private Pen? pen_Background_Grid = new Pen(new SolidColorBrush(System.Windows.Media.Color.FromRgb(192, 192, 192)), 1);
        private Pen pen2 = new Pen(new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)), 1);
        protected Color color_background = Color.FromRgb(230, 230, 120);
        protected System.Windows.Rect rectangle = new Rect() { Width = 100, Height = 100 };
        protected int _X;
        protected int _Y;
        private Brush brush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 255));
        protected int _ChartOffset_Width = 20;

        #endregion



        public Oscilloscope()
        {
            InitializeComponent();
        }
    }
}
