using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Binaries
{
    //Decoration area, nothing to describe
    public partial class Decorate
    {
        UserClass wU = new UserClass();
        GradientStopCollection gsc;
        /// <summary>
        /// Adds purple-cyan gradient
        /// </summary>
        /// <param name="grid"></param>
        public void GradientGrid(Grid grid)
        {
            gsc = new GradientStopCollection();
            gsc.Add(new GradientStop()
            {
                Color = Colors.Purple,
                Offset = 0.1
            });
            gsc.Add(new GradientStop()
            {
                Color = Colors.Cyan,
                Offset = 1.0
            });
            grid.Background = new LinearGradientBrush(gsc, 0)
            {
                StartPoint = new Point(0.5, 0),
                EndPoint = new Point(0.5, 1)
            };
        }
        
        /// <summary>
        /// Adds Gray-Darkgray gradient
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="textbox"></param>
        public void GradientField(ListBox listbox, TextBox textbox)
        {
            gsc = new GradientStopCollection();
            gsc.Add(new GradientStop()
            {
                Color = Colors.Gray,
                Offset = 0.0
            });
            gsc.Add(new GradientStop()
            {
                Color = Colors.DarkGray,
                Offset = 0.5
            });
            listbox.Background = new LinearGradientBrush(gsc, 0)
            {
                StartPoint = new Point(0.5, 0),
                Opacity = 0.6,
                EndPoint = new Point(0.5, 1)
            };
            textbox.Background = new LinearGradientBrush(gsc, 0)
            {
                StartPoint = new Point(0.5, 0),
                Opacity = 0.6,
                EndPoint = new Point(0.5, 1)
            };
        }

        
    }

    public partial class Decorate
    {
        /// <summary>
        /// Sets color of settings element
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="index"></param>
        public void InitiateColor(ContentControl uiElement, int index)
        {
            if(!wU.Reader()[index])
            {
                uiElement.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
            else
            {
                uiElement.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
        
    }
}
