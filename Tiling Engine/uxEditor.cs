using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tiling_Engine
{
    public partial class uxEditorForm : Form
    {
        private World _map = null;
        private FlowLayoutPanel _mapPanel;
        private FlowLayoutPanel _outerPanel;
        private DataGridView mapGrid;

        public uxEditorForm()
        {
            InitializeComponent();
        }

        public World ReturnMap()
        {
            return _map;
        }

        public void SetMap(World m)
        {
            this.Controls.Remove(mapGrid);
            _map = m;
            int size = _map.ReturnSize();

            mapGrid = new DataGridView();
            mapGrid.RowTemplate.Height = 20;
            mapGrid.RowHeadersVisible = false;
            mapGrid.ColumnHeadersVisible = false;
            mapGrid.AllowUserToAddRows = false;
            mapGrid.AllowUserToDeleteRows = false;
            mapGrid.AllowUserToResizeColumns = false;
            mapGrid.AllowUserToResizeRows = false;
            mapGrid.EditMode = DataGridViewEditMode.EditProgrammatically;

            mapGrid.DefaultCellStyle.SelectionBackColor = _map.ReturnKnownColor(0,0);
            mapGrid.ClearSelection();

            mapGrid.ColumnCount = size;
            mapGrid.RowCount = size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    mapGrid.Rows[i].Cells[j].Style.BackColor = _map.ReturnKnownColor(i, j);
                }
            }

            mapGrid.CellMouseUp += (s, e) => {
                foreach (DataGridViewCell cell in mapGrid.SelectedCells)
                {
                    Color c = _map.CellClick(cell.ColumnIndex, cell.RowIndex);  //stuff might get flipped
                    mapGrid.DefaultCellStyle.SelectionBackColor = c;
                    mapGrid.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = c;
                    mapGrid.ClearSelection();
                }
            };
            objectPlaces();
        }

        public void MakeNew()
        {
            _map = new World();
            int size = _map.ReturnSize();

            mapGrid = new DataGridView();
            mapGrid.RowTemplate.Height = 20;
            mapGrid.RowHeadersVisible = false;
            mapGrid.ColumnHeadersVisible = false;
            mapGrid.AllowUserToAddRows = false;
            mapGrid.AllowUserToDeleteRows = false;
            mapGrid.AllowUserToResizeColumns = false;
            mapGrid.AllowUserToResizeRows = false;
            mapGrid.EditMode = DataGridViewEditMode.EditProgrammatically;

            mapGrid.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            mapGrid.ClearSelection();

            mapGrid.ColumnCount = size;
            mapGrid.RowCount = size;
            for (int i=0; i<size; i++)
            {
                for(int j=0; j<size; j++)
                {
                    mapGrid.Columns[j].Width = 20;
                    mapGrid.Rows[i].Cells[j].Style.BackColor = Color.DarkGray;
                }
            }

            mapGrid.CellMouseUp += (s, e) =>{                        
                foreach (DataGridViewCell cell in mapGrid.SelectedCells)
                {
                    Color c = _map.CellClick(cell.ColumnIndex, cell.RowIndex);  //stuff might get flipped
                    mapGrid.DefaultCellStyle.SelectionBackColor = c;
                    mapGrid.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = c;
                    mapGrid.ClearSelection();
                }
            };

            this.Controls.Add(mapGrid);

            objectPlaces();
        }



        public void ClearMap(int size)
        {
            this.Controls.Remove(mapGrid);
            _map = new World();
            size = _map.ReturnSize();

            mapGrid = new DataGridView();
            mapGrid.RowTemplate.Height = 20;
            mapGrid.RowHeadersVisible = false;
            mapGrid.ColumnHeadersVisible = false;
            mapGrid.AllowUserToAddRows = false;
            mapGrid.AllowUserToDeleteRows = false;
            mapGrid.AllowUserToResizeColumns = false;
            mapGrid.AllowUserToResizeRows = false;
            mapGrid.EditMode = DataGridViewEditMode.EditProgrammatically;

            mapGrid.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            mapGrid.ClearSelection();

            mapGrid.ColumnCount = size;
            mapGrid.RowCount = size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    mapGrid.Columns[j].Width = 20;
                    mapGrid.Rows[i].Cells[j].Style.BackColor = Color.DarkGray;
                }
            }

            mapGrid.CellMouseUp += (s, e) => {                     
                foreach (DataGridViewCell cell in mapGrid.SelectedCells)
                {
                    Color c = _map.CellClick(cell.ColumnIndex, cell.RowIndex);  //stuff might get flipped
                    mapGrid.DefaultCellStyle.SelectionBackColor = c;
                    mapGrid.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = c;
                    mapGrid.ClearSelection();
                }
            };

            this.Controls.Add(mapGrid);

            objectPlaces();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            uxRBlank.Checked = true;

            this.ResizeEnd += (s, en) =>
            {
                objectPlaces();
            };
        }

        private void uxGenerate_Click(object sender, EventArgs e)
        {
            _map.Generate();
        }

        private void uxClear_Click(object sender, EventArgs e)
        {
            int size = _map.ReturnSize();
            ClearMap(size);  
        }

        private void uxBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uxBiomes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (button.Checked)
            {
                _map.SetMouseColor(Convert.ToInt16(button.Tag));
            }
        }
        
        private void objectPlaces()
        {
            int buttonX = (this.Width - uxBack.Size.Width) - 55;
            int RBX = (this.Width - uxRBlank.Size.Width) - 55;

            //buttons
            uxBack.Location = new Point(buttonX, 519);
            uxClear.Location = new Point(buttonX, 453);
            uxGenerate.Location = new Point(buttonX, 394);

            //radiobuttons
            uxRBlank.Location = new Point(RBX, 24);
            uxRGrass.Location = new Point(RBX, 51);
            uxRDesert.Location = new Point(RBX, 78);
            uxRMountains.Location = new Point(RBX, 105);
            uxROcean.Location = new Point(RBX, 132);
            uxRTundra.Location = new Point(RBX, 160);
            uxRCity.Location = new Point(RBX, 187);

            //grid size and location
            mapGrid.Width = (20 * mapGrid.ColumnCount) + 10;
            mapGrid.Height = (20 * mapGrid.RowCount) + 10;

            if (mapGrid.Height >= (this.Height - 55))
            {
                mapGrid.Height = (this.Height - 55);
            }

            if (mapGrid.Width >= (RBX - 75))
            {
                mapGrid.Width = (RBX - 75);
            }   
        }
    }
}
