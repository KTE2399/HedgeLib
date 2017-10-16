using HedgeLib;
using HedgeLib.Sets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HedgeEdit
{
    public partial class MainFrm : Form
    {

        public static string MainDirectoryPath = Path.GetFullPath("SONICORCA\\");
        public static MainFrm Instance;
        public static S2HDSetData SetData;
        // Variables/Constants
        public static SceneView SceneView
        {
            get => sceneView;
        }

        public SetObject SelectedSetObject
        {
            get
            {
                // Returns the currently-selected set object if only one is selected
                return (SelectedObjects.Count == 1) ?
                    (SelectedObjects[0] as SetObject) : null;
            }
        }

        public SetObjectTransform SelectedTransform
        {
            get
            {
                // Returns the transform of the currently-selected set object
                // if only one object is selected.
                var obj = SelectedSetObject;

                return (obj == null) ? ((SelectedObjects.Count == 1) ?
                    (SelectedObjects[0] as SetObjectTransform) :
                    null) : obj.Transform;
            }
        }

        public List<object> SelectedObjects = new List<object>();
        private static SceneView sceneView = null;
        private Control activeTxtBx = null;

        // Constructors
        public MainFrm()
        {
            InitializeComponent();
            UpdateTitle();
            Application.Idle += Application_Idle;
        }

        // Methods
        public static string GetFullPathFromSonicOrcaPath(string key)
        {
            string parent = new DirectoryInfo(MainDirectoryPath).Parent.FullName;
            return Path.Combine(parent, key.Replace('/', '\\'));
        }
        public void UpdatePos(int x,int y) {
            posXBox.Text = x.ToString();
            posYBox.Text = y.ToString();
            RefreshGUI();
        }
        public void RefreshGUI()
        {
            // Get the selected object(s), if any
            // TODO
            int selectedObjs = Editor.Instance.SelectedObject != null ? 1 : 0;
            bool objsSelected = (selectedObjs > 0),
                 singleObjSelected = (selectedObjs == 1);

            // Update Labels
            objectSelectedLbl.Text = $"{selectedObjs} Object(s) Selected";

            // Enable/Disable EVERYTHING
            posXBox.Enabled = posYBox.Enabled = viewSelectedBtn.Enabled =
            viewSelectedMenuItem.Enabled = singleObjSelected;

            removeObjectBtn.Enabled = objsSelected;

        }

        public void UpdateTitle(string stgID = null)
        {
            Text = string.Format("{0} - {1}",
                (string.IsNullOrEmpty(stgID)) ? "Untitled" : stgID,
                Program.Name);
        }

        // GUI Events
        #region MainFrm/Viewport Events
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Handle these shortcut keys only if no textBoxes are in focus
            if (activeTxtBx != null)
                return base.ProcessCmdKey(ref msg, keyData);

            switch (keyData)
            {
                // Undo Changes
                case Keys.Control | Keys.Z:
                    if (Editor.Instance.LastSelectedObjects.Count < 1)
                        return false;

                    Editor.Instance.LastSelectedObjects[Editor.Instance.LastSelectedObjects.Count -1].X = Editor.Instance.LastX[Editor.Instance.LastSelectedObjects.Count - 1];
                    Editor.Instance.LastSelectedObjects[Editor.Instance.LastSelectedObjects.Count - 1].Y = Editor.Instance.LastY[Editor.Instance.LastSelectedObjects.Count - 1];
                    Editor.Instance.LastSelectedObjects.RemoveAt(Editor.Instance.LastSelectedObjects.Count - 1);
                    Editor.Instance.LastX.RemoveAt(Editor.Instance.LastX.Count - 1);
                    Editor.Instance.LastY.RemoveAt(Editor.Instance.LastY.Count - 1);
                    return true;

                // Cut Selected Object(s)
                case Keys.Control | Keys.X:
                    cutMenuItem.PerformClick();
                    return true;

                // Copy Selected Object(s)
                case Keys.Control | Keys.C:
                    copyMenuItem.PerformClick();
                    return true;

                // Paste Selected Object(s)
                case Keys.Control | Keys.V:
                    pasteMenuItem.PerformClick();
                    return true;

                // Delete Selected Object(s)
                case Keys.Delete:
                    deleteMenuItem.PerformClick();
                    return true;

                // Select All
                case Keys.Control | Keys.A:
                    selectAllMenuItem.PerformClick();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            Instance = this;
            //GameList.Load(Program.StartupPath);
            Viewport.Init(viewport);
            var editor = new Editor();
            Viewport.AddObject(editor);


            var test = new ViewportSprite("test.png");
            test.Position = new OpenTK.Vector2(100, 100);
            test.Size = new OpenTK.Vector2(200, 200);
            var test2 = new ViewportSprite("test.png");
            test2.Position = new OpenTK.Vector2(150, 100);
            test2.Size = new OpenTK.Vector2(200, 200);
            editor.AddLevelObject(test2);
            editor.AddLevelObject(test);


            string tileset = GetFullPathFromSonicOrcaPath("SONICORCA/LEVELS/EHZ/TILESET.tileset.xml");
            string binding = GetFullPathFromSonicOrcaPath("SONICORCA/LEVELS/EHZ/BINDING.binding.xml");
            string mapPath = GetFullPathFromSonicOrcaPath("SONICORCA/LEVELS/EHZ/MAP.map.xml");
            var font = new Font();
            font.LoadFont("HUD");
            font.LoadFontTexture(1);
            Editor.Instance.Font = font;

            if (File.Exists(tileset))
            {
                var tileSet = new TileSet();
                tileSet.Load(tileset);
                //var frame = tileSet.Tiles["1371"].Frames[0];
                //var tilesetTexture = new ViewportSprite("TILESET" + tileSet.Textures[0] + ".png");
                //tilesetTexture.Position = new OpenTK.Vector2(300, 100);
                //tilesetTexture.Size = new OpenTK.Vector2(200, 200);
                //tilesetTexture.Crop = new OpenTK.Vector4(frame.X, frame.Y, 64, 64);
                //editor.AddLevelObject(tilesetTexture);
                if (File.Exists(binding))
                {
                    var set = new S2HDSetData();
                    set.Load(binding);
                    SetData = set;
                    if (File.Exists(mapPath))
                    {
                        var map = new Map();
                        map.Load(mapPath);
                        var vpMap = new ViewPortMap(map, tileSet);
                        editor.Init(set);
                        editor.AddLevelObject(vpMap);
                    }
                }
            }
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            while (viewport.IsIdle)
            {
                Viewport.Render();
            }
        }

        private void Viewport_Paint(object sender, PaintEventArgs e)
        {
            Viewport.Render();
        }

        private void Viewport_Resize(object sender, EventArgs e)
        {
            Viewport.Resize(viewport.Width, viewport.Height);
        }

        private void Viewport_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Editor.IsMovingCamera = true;
                Cursor.Hide();
            }
        }

        private void Viewport_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Editor.IsMovingCamera = false;
                Cursor.Show();
            }
        }
        #endregion

        #region NumTxtBx Events
        private void NumTxtBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txtBx = sender as TextBox;
            if (txtBx == null) return;

            // If the pressed key is enter, stop typing
            if (e.KeyChar == (char)Keys.Return)
            {
                ActiveControl = null;
                e.Handled = true;
                return;
            }

            // If the pressed key isn't a control key, digit, or
            // the first decimal point, don't accept it.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                ((e.KeyChar != '.') || (txtBx.Text.IndexOf('.') > -1)))
            {
                e.Handled = true;
            }
        }

        private void NumTxtBx_Enter(object sender, EventArgs e)
        {
            activeTxtBx = sender as Control;
        }

        private void NumTxtBx_Leave(object sender, EventArgs e)
        {
            var txtBx = sender as TextBox;
            if (txtBx == null) return;

            if (float.TryParse(txtBx.Text, out float f))
            {
                txtBx.Text = f.ToString();
            }
            else
            {
                txtBx.Text = "0";
            }

            activeTxtBx = null;
        }
        #endregion

        #region File Menu Events
        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            var openDialog = new StgOpenDialog();
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                // Update title
                UpdateTitle(openDialog.StageID);

                // Load stage
                Stage.Load(openDialog.DataDir,
                    openDialog.StageID, GameList.Games[openDialog.GameID]);

                // Update Scene View
                if (sceneView != null)
                    sceneView.RefreshView();
            }
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
            SetData.Save("BINDING.binding.xml", true);
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Edit Menu Events
        private void UndoMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void RedoMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void CutMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void PasteMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void SelectAllMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void SelectNoneMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void AdvancedModeMenuItem_Click(object sender, EventArgs e)
        {
            if (sceneView == null || sceneView.IsDisposed)
            {
                sceneView = new SceneView(this);
                sceneView.Show();
            }
            else
            {
                sceneView.Focus();
            }
        }
        #endregion

        private void ViewSelected(object sender, EventArgs e)
        {
            if (SelectedObjects.Count == 1)
            {
                var selectedTransform = SelectedTransform;
                if (selectedTransform == null) return;

                Viewport.CameraPos =
                    Types.ToOpenTK(selectedTransform.Position);
            }
            else if (SelectedObjects.Count > 0)
            {
                // TODO: Show all of the objects currently selected.
            }
        }

        public void FillSetObjectDetails(S2HDSetData.SetObject sobj)
        {
            objectProperties.Items.Clear();
            foreach (var pair in sobj.ExtraData)
            {
                var lvi = new ListViewItem(pair.Key);
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, pair.Value.ToString()));
                lvi.Tag = pair;
                objectProperties.Items.Add(lvi);
            }
            UpdatePos(sobj.X, sobj.Y);
        }
    }
}