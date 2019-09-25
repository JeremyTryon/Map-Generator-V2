using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Tiling_Engine
{
    [Serializable]
    public class Cell
    {
        [NonSerialized]
        private Label _label;

        private bool _visible;
        private Tuple<int, int> _coor;
        private int _color;
        private int[] _surColors = { 0, 0, 0, 0, 0, 0 };

        public Cell(int x, int y)
        {
            _color = 0;
            _visible = true;
        }

        public Color ChangeColor(int color)
        { 
            if(color == 0)
            {
                _color = color;
                setVisible(true);
                return Color.DarkGray;
            }
            else if (color == 1)
            {
                _color = color;
                setVisible(true);
                return Color.Green;
            }
            else if (color == 2)
            {
                _color = color;
                setVisible(true);
                return Color.Yellow;
            }
            else if (color == 3)
            {
                _color = color;
                setVisible(true);
                return Color.Brown;
            }
            else if (color == 4)
            {
                _color = color;
                setVisible(true);
                return Color.Blue;
            }
            else if (color == 5)
            {
                _color = color;
                setVisible(true);
                return Color.White;
            }
            else if (color == 6)
            {
                setVisible(true);
                if (_color == 1)
                {
                    return Color.Green;
                }
                else if (_color == 2)
                {
                    return Color.Yellow;
                }
                else if (_color == 3)
                {
                    return Color.Brown;
                }
                else if (_color == 4)
                {
                    return Color.Blue;
                }
                else if (_color == 5)
                {
                    return Color.White;
                }
            }
            else if (color == 7)
            {
                setVisible(false);
                if (_color == 1)
                {
                    return Color.FromArgb(100, Color.Green);
                }
                else if (_color == 2)
                {
                    return Color.Orange;
                    //_label.BackColor = System.Drawing.Color.FromArgb(100, Color.Yellow);
                }
                else if (_color == 3)
                {
                    return Color.FromArgb(100, Color.Brown);
                }
                else if (_color == 4)
                {
                    return Color.FromArgb(100, Color.Blue);
                }
                else if (_color == 5)
                {
                    return Color.LightGray;
                }
            }
            return Color.DarkGray; // this exists to remove errors but there will never be a path that reaches it
        }

        public bool IsVisible()
        {
            return _visible;
        }

        public Color ReturnKnownColor()
        {

            Color ret = ChangeColor(_color);
            if (IsVisible())
            {
                ret = ChangeColor(6);
            }
            else
            {
                ret = ChangeColor(7);
            }
            return ret;
        }

        public void setVisible(bool b)
        {
            _visible = b;
        }
        public int ReturnColor()
        {
            return _color;
        }

        public void RemoveColor(int color)
        {

            if (color < 5)
            {
                _surColors[color] -= 1;
            } 
        }

        public void AddColor(int color)
        {
            if (color < 5)
            {
                _surColors[color] += 1;
            }
        }

        public int[] RetCA()
        {
            return _surColors;
        }
    }
}
