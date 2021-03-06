﻿namespace HedgeEdit
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectNoneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.advancedModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSelectedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewport = new OpenTK.GLControl();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.topSplitContainer = new System.Windows.Forms.SplitContainer();
            this.addObjectBtn = new System.Windows.Forms.Button();
            this.removeObjectBtn = new System.Windows.Forms.Button();
            this.objectCountLbl = new System.Windows.Forms.Label();
            this.bottomSplitContainer = new System.Windows.Forms.SplitContainer();
            this.posYBox = new System.Windows.Forms.TextBox();
            this.posXBox = new System.Windows.Forms.TextBox();
            this.viewSelectedBtn = new System.Windows.Forms.Button();
            this.objectSelectedLbl = new System.Windows.Forms.Label();
            this.objectProperties = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.objectTypeLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topSplitContainer)).BeginInit();
            this.topSplitContainer.Panel1.SuspendLayout();
            this.topSplitContainer.Panel2.SuspendLayout();
            this.topSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomSplitContainer)).BeginInit();
            this.bottomSplitContainer.Panel1.SuspendLayout();
            this.bottomSplitContainer.Panel2.SuspendLayout();
            this.bottomSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.White;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip.Size = new System.Drawing.Size(523, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem,
            this.openMenuItem,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.fileSeparator1,
            this.exitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 22);
            this.fileMenu.Text = "&File";
            // 
            // newMenuItem
            // 
            this.newMenuItem.Name = "newMenuItem";
            this.newMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newMenuItem.Size = new System.Drawing.Size(195, 22);
            this.newMenuItem.Text = "&New";
            this.newMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openMenuItem.Text = "&Open...";
            this.openMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveMenuItem.Text = "&Save";
            this.saveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Name = "saveAsMenuItem";
            this.saveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveAsMenuItem.Text = "Save &As...";
            this.saveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // fileSeparator1
            // 
            this.fileSeparator1.Name = "fileSeparator1";
            this.fileSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem,
            this.redoMenuItem,
            this.selectSeparator1,
            this.cutMenuItem,
            this.copyMenuItem,
            this.pasteMenuItem,
            this.deleteMenuItem,
            this.selectSeparator2,
            this.selectAllMenuItem,
            this.selectNoneMenuItem,
            this.selectSeparator3,
            this.advancedModeMenuItem});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(39, 22);
            this.editMenu.Text = "&Edit";
            // 
            // undoMenuItem
            // 
            this.undoMenuItem.Enabled = false;
            this.undoMenuItem.Name = "undoMenuItem";
            this.undoMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
            this.undoMenuItem.Size = new System.Drawing.Size(179, 22);
            this.undoMenuItem.Text = "&Undo";
            this.undoMenuItem.Click += new System.EventHandler(this.UndoMenuItem_Click);
            // 
            // redoMenuItem
            // 
            this.redoMenuItem.Enabled = false;
            this.redoMenuItem.Name = "redoMenuItem";
            this.redoMenuItem.ShortcutKeyDisplayString = "";
            this.redoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoMenuItem.Size = new System.Drawing.Size(179, 22);
            this.redoMenuItem.Text = "&Redo";
            this.redoMenuItem.Click += new System.EventHandler(this.RedoMenuItem_Click);
            // 
            // selectSeparator1
            // 
            this.selectSeparator1.Name = "selectSeparator1";
            this.selectSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // cutMenuItem
            // 
            this.cutMenuItem.Enabled = false;
            this.cutMenuItem.Name = "cutMenuItem";
            this.cutMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
            this.cutMenuItem.Size = new System.Drawing.Size(179, 22);
            this.cutMenuItem.Text = "Cu&t";
            this.cutMenuItem.Click += new System.EventHandler(this.CutMenuItem_Click);
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Enabled = false;
            this.copyMenuItem.Name = "copyMenuItem";
            this.copyMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.copyMenuItem.Size = new System.Drawing.Size(179, 22);
            this.copyMenuItem.Text = "&Copy";
            this.copyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Enabled = false;
            this.pasteMenuItem.Name = "pasteMenuItem";
            this.pasteMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
            this.pasteMenuItem.Size = new System.Drawing.Size(179, 22);
            this.pasteMenuItem.Text = "&Paste";
            this.pasteMenuItem.Click += new System.EventHandler(this.PasteMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Enabled = false;
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.ShortcutKeyDisplayString = "Del";
            this.deleteMenuItem.Size = new System.Drawing.Size(179, 22);
            this.deleteMenuItem.Text = "&Delete";
            this.deleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // selectSeparator2
            // 
            this.selectSeparator2.Name = "selectSeparator2";
            this.selectSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // selectAllMenuItem
            // 
            this.selectAllMenuItem.Name = "selectAllMenuItem";
            this.selectAllMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
            this.selectAllMenuItem.Size = new System.Drawing.Size(179, 22);
            this.selectAllMenuItem.Text = "Select &All";
            this.selectAllMenuItem.Click += new System.EventHandler(this.SelectAllMenuItem_Click);
            // 
            // selectNoneMenuItem
            // 
            this.selectNoneMenuItem.Name = "selectNoneMenuItem";
            this.selectNoneMenuItem.ShortcutKeyDisplayString = "";
            this.selectNoneMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.selectNoneMenuItem.Size = new System.Drawing.Size(179, 22);
            this.selectNoneMenuItem.Text = "Select &None";
            this.selectNoneMenuItem.Click += new System.EventHandler(this.SelectNoneMenuItem_Click);
            // 
            // selectSeparator3
            // 
            this.selectSeparator3.Name = "selectSeparator3";
            this.selectSeparator3.Size = new System.Drawing.Size(176, 6);
            // 
            // advancedModeMenuItem
            // 
            this.advancedModeMenuItem.Name = "advancedModeMenuItem";
            this.advancedModeMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.advancedModeMenuItem.Size = new System.Drawing.Size(179, 22);
            this.advancedModeMenuItem.Text = "Scene &View";
            this.advancedModeMenuItem.Click += new System.EventHandler(this.AdvancedModeMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewSelectedMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(44, 22);
            this.viewMenu.Text = "&View";
            // 
            // viewSelectedMenuItem
            // 
            this.viewSelectedMenuItem.Enabled = false;
            this.viewSelectedMenuItem.Name = "viewSelectedMenuItem";
            this.viewSelectedMenuItem.Size = new System.Drawing.Size(146, 22);
            this.viewSelectedMenuItem.Text = "View &Selected";
            this.viewSelectedMenuItem.Click += new System.EventHandler(this.ViewSelected);
            // 
            // viewport
            // 
            this.viewport.BackColor = System.Drawing.Color.Black;
            this.viewport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewport.Location = new System.Drawing.Point(0, 0);
            this.viewport.Name = "viewport";
            this.viewport.Size = new System.Drawing.Size(270, 341);
            this.viewport.TabIndex = 1;
            this.viewport.VSync = true;
            this.viewport.Paint += new System.Windows.Forms.PaintEventHandler(this.Viewport_Paint);
            this.viewport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Viewport_MouseDown);
            this.viewport.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Viewport_MouseUp);
            this.viewport.Resize += new System.EventHandler(this.Viewport_Resize);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.mainSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.topSplitContainer);
            this.mainSplitContainer.Panel1MinSize = 250;
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.viewport);
            this.mainSplitContainer.Size = new System.Drawing.Size(523, 341);
            this.mainSplitContainer.SplitterDistance = 250;
            this.mainSplitContainer.SplitterWidth = 3;
            this.mainSplitContainer.TabIndex = 2;
            // 
            // topSplitContainer
            // 
            this.topSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.topSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.topSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.topSplitContainer.Name = "topSplitContainer";
            this.topSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // topSplitContainer.Panel1
            // 
            this.topSplitContainer.Panel1.Controls.Add(this.addObjectBtn);
            this.topSplitContainer.Panel1.Controls.Add(this.removeObjectBtn);
            this.topSplitContainer.Panel1.Controls.Add(this.objectCountLbl);
            this.topSplitContainer.Panel1MinSize = 110;
            // 
            // topSplitContainer.Panel2
            // 
            this.topSplitContainer.Panel2.Controls.Add(this.bottomSplitContainer);
            this.topSplitContainer.Size = new System.Drawing.Size(250, 341);
            this.topSplitContainer.SplitterDistance = 110;
            this.topSplitContainer.SplitterWidth = 3;
            this.topSplitContainer.TabIndex = 1;
            // 
            // addObjectBtn
            // 
            this.addObjectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addObjectBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addObjectBtn.Location = new System.Drawing.Point(5, 62);
            this.addObjectBtn.Margin = new System.Windows.Forms.Padding(2);
            this.addObjectBtn.Name = "addObjectBtn";
            this.addObjectBtn.Size = new System.Drawing.Size(240, 19);
            this.addObjectBtn.TabIndex = 1;
            this.addObjectBtn.Text = "&Add Object";
            this.addObjectBtn.UseVisualStyleBackColor = true;
            // 
            // removeObjectBtn
            // 
            this.removeObjectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeObjectBtn.Enabled = false;
            this.removeObjectBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.removeObjectBtn.Location = new System.Drawing.Point(5, 86);
            this.removeObjectBtn.Margin = new System.Windows.Forms.Padding(2);
            this.removeObjectBtn.Name = "removeObjectBtn";
            this.removeObjectBtn.Size = new System.Drawing.Size(240, 19);
            this.removeObjectBtn.TabIndex = 2;
            this.removeObjectBtn.Text = "&Remove Selected Object(s)";
            this.removeObjectBtn.UseVisualStyleBackColor = true;
            // 
            // objectCountLbl
            // 
            this.objectCountLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectCountLbl.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectCountLbl.Location = new System.Drawing.Point(0, 0);
            this.objectCountLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.objectCountLbl.Name = "objectCountLbl";
            this.objectCountLbl.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.objectCountLbl.Size = new System.Drawing.Size(250, 110);
            this.objectCountLbl.TabIndex = 0;
            this.objectCountLbl.Text = "0 Objects";
            this.objectCountLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // bottomSplitContainer
            // 
            this.bottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.bottomSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.bottomSplitContainer.Name = "bottomSplitContainer";
            this.bottomSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // bottomSplitContainer.Panel1
            // 
            this.bottomSplitContainer.Panel1.Controls.Add(this.label2);
            this.bottomSplitContainer.Panel1.Controls.Add(this.label1);
            this.bottomSplitContainer.Panel1.Controls.Add(this.posYBox);
            this.bottomSplitContainer.Panel1.Controls.Add(this.posXBox);
            this.bottomSplitContainer.Panel1.Controls.Add(this.viewSelectedBtn);
            this.bottomSplitContainer.Panel1.Controls.Add(this.objectSelectedLbl);
            this.bottomSplitContainer.Panel1MinSize = 140;
            // 
            // bottomSplitContainer.Panel2
            // 
            this.bottomSplitContainer.Panel2.Controls.Add(this.objectProperties);
            this.bottomSplitContainer.Panel2.Controls.Add(this.objectTypeLbl);
            this.bottomSplitContainer.Size = new System.Drawing.Size(250, 228);
            this.bottomSplitContainer.SplitterDistance = 140;
            this.bottomSplitContainer.SplitterWidth = 3;
            this.bottomSplitContainer.TabIndex = 0;
            // 
            // posYBox
            // 
            this.posYBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.posYBox.Enabled = false;
            this.posYBox.Location = new System.Drawing.Point(37, 46);
            this.posYBox.Margin = new System.Windows.Forms.Padding(2);
            this.posYBox.Name = "posYBox";
            this.posYBox.Size = new System.Drawing.Size(125, 20);
            this.posYBox.TabIndex = 4;
            this.posYBox.Text = "0";
            this.posYBox.Enter += new System.EventHandler(this.NumTxtBx_Enter);
            this.posYBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumTxtBx_KeyPress);
            this.posYBox.Leave += new System.EventHandler(this.NumTxtBx_Leave);
            // 
            // posXBox
            // 
            this.posXBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.posXBox.Enabled = false;
            this.posXBox.Location = new System.Drawing.Point(37, 22);
            this.posXBox.Margin = new System.Windows.Forms.Padding(2);
            this.posXBox.Name = "posXBox";
            this.posXBox.Size = new System.Drawing.Size(125, 20);
            this.posXBox.TabIndex = 3;
            this.posXBox.Text = "0";
            this.posXBox.Enter += new System.EventHandler(this.NumTxtBx_Enter);
            this.posXBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumTxtBx_KeyPress);
            this.posXBox.Leave += new System.EventHandler(this.NumTxtBx_Leave);
            // 
            // viewSelectedBtn
            // 
            this.viewSelectedBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewSelectedBtn.Enabled = false;
            this.viewSelectedBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.viewSelectedBtn.Location = new System.Drawing.Point(5, 70);
            this.viewSelectedBtn.Margin = new System.Windows.Forms.Padding(2);
            this.viewSelectedBtn.Name = "viewSelectedBtn";
            this.viewSelectedBtn.Size = new System.Drawing.Size(157, 19);
            this.viewSelectedBtn.TabIndex = 2;
            this.viewSelectedBtn.Text = "&View Selected";
            this.viewSelectedBtn.UseVisualStyleBackColor = true;
            this.viewSelectedBtn.Click += new System.EventHandler(this.ViewSelected);
            // 
            // objectSelectedLbl
            // 
            this.objectSelectedLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectSelectedLbl.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectSelectedLbl.Location = new System.Drawing.Point(0, 0);
            this.objectSelectedLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.objectSelectedLbl.Name = "objectSelectedLbl";
            this.objectSelectedLbl.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.objectSelectedLbl.Size = new System.Drawing.Size(250, 140);
            this.objectSelectedLbl.TabIndex = 1;
            this.objectSelectedLbl.Text = "0 Object(s) Selected";
            this.objectSelectedLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // objectProperties
            // 
            this.objectProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.objectProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectProperties.FullRowSelect = true;
            this.objectProperties.Location = new System.Drawing.Point(0, 23);
            this.objectProperties.Margin = new System.Windows.Forms.Padding(2);
            this.objectProperties.MultiSelect = false;
            this.objectProperties.Name = "objectProperties";
            this.objectProperties.ShowItemToolTips = true;
            this.objectProperties.Size = new System.Drawing.Size(250, 62);
            this.objectProperties.TabIndex = 0;
            this.objectProperties.UseCompatibleStateImageBehavior = false;
            this.objectProperties.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 90;
            // 
            // objectTypeLbl
            // 
            this.objectTypeLbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.objectTypeLbl.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectTypeLbl.Location = new System.Drawing.Point(0, 0);
            this.objectTypeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.objectTypeLbl.Name = "objectTypeLbl";
            this.objectTypeLbl.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.objectTypeLbl.Size = new System.Drawing.Size(250, 23);
            this.objectTypeLbl.TabIndex = 2;
            this.objectTypeLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "X: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Y: ";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(523, 365);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(539, 404);
            this.Name = "MainFrm";
            this.Text = "HedgeEdit";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.topSplitContainer.Panel1.ResumeLayout(false);
            this.topSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topSplitContainer)).EndInit();
            this.topSplitContainer.ResumeLayout(false);
            this.bottomSplitContainer.Panel1.ResumeLayout(false);
            this.bottomSplitContainer.Panel1.PerformLayout();
            this.bottomSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bottomSplitContainer)).EndInit();
            this.bottomSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem undoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoMenuItem;
        private System.Windows.Forms.ToolStripSeparator selectSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
        private System.Windows.Forms.ToolStripSeparator selectSeparator2;
        private System.Windows.Forms.ToolStripMenuItem selectAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectNoneMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewSelectedMenuItem;
        private OpenTK.GLControl viewport;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.Label objectCountLbl;
        private System.Windows.Forms.Button addObjectBtn;
        private System.Windows.Forms.Button removeObjectBtn;
        private System.Windows.Forms.SplitContainer topSplitContainer;
        private System.Windows.Forms.SplitContainer bottomSplitContainer;
        private System.Windows.Forms.Label objectSelectedLbl;
        private System.Windows.Forms.Button viewSelectedBtn;
        private System.Windows.Forms.TextBox posYBox;
        private System.Windows.Forms.TextBox posXBox;
        private System.Windows.Forms.ListView objectProperties;
        private System.Windows.Forms.Label objectTypeLbl;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripSeparator selectSeparator3;
        private System.Windows.Forms.ToolStripMenuItem advancedModeMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

