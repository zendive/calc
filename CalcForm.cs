/**
 * Author: Alexander Block
 * Is a part of Calc application
 * developed for C# study goals and is an intelectual
 * property of his author.
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Calc;

namespace MasCalc
{
  

  /// <summary>
  /// Calculator form implementation
  /// </summary>
  public class CalcForm : System.Windows.Forms.Form
  {
    // calculator object
    private CCalculator m_Calc;
    // calculation answer holder for presentation in history pane
    private CNumber m_Answer;
    // reference to calculator registers
    private CRegister m_reg;
    // user typed expression in `txtExp` before calculation
    private string m_sExpression;
    // history of NumPad expression builder
    private ArrayList m_alNpExp;

    #region DATA DEFINITION
    private System.Windows.Forms.TextBox txtExp;// user expression text-box
    private System.Windows.Forms.ListBox listHist;// user results history
    private System.ComponentModel.IContainer components;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.MenuItem CleanPage;
    private System.Windows.Forms.MenuItem SaveAs;
    private System.Windows.Forms.ContextMenu cmenuHist;
    private System.Windows.Forms.StatusBar statusBar;
    private System.Windows.Forms.StatusBarPanel statusBarSyntax;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ImageList StatusIcons;
    private System.Windows.Forms.ComboBox InputCountSystem;
    private System.Windows.Forms.ComboBox OutputCountSystem;
    private System.Windows.Forms.ComboBox InputTrigSystem;
    private System.Windows.Forms.Button btnSolve;
    private System.Windows.Forms.ComboBox OutputPrecision;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.TreeView treeAlbom;
    private System.Windows.Forms.TabPage tabFunc;
    private System.Windows.Forms.TabPage tabNum;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.Button npDel;
    private System.Windows.Forms.Button npAC;
    private System.Windows.Forms.Button npSolve;
    private System.Windows.Forms.Button npBrEnd;
    private System.Windows.Forms.Button npBrBeg;
    private System.Windows.Forms.Button npAns;
    private System.Windows.Forms.Button npDot;
    private System.Windows.Forms.Button np00;
    private System.Windows.Forms.Button np0;
    private System.Windows.Forms.Button np3;
    private System.Windows.Forms.Button np2;
    private System.Windows.Forms.Button np1;
    private System.Windows.Forms.Button np6;
    private System.Windows.Forms.Button np5;
    private System.Windows.Forms.Button np4;
    private System.Windows.Forms.Button np9;
    private System.Windows.Forms.Button np8;
    private System.Windows.Forms.Button np7;
    private System.Windows.Forms.Button npGcd;
    private System.Windows.Forms.Button npSftR;
    private System.Windows.Forms.Button npSftL;
    private System.Windows.Forms.Button npXor;
    private System.Windows.Forms.Button npOr;
    private System.Windows.Forms.Button npAnd;
    private System.Windows.Forms.Button npNot;
    private System.Windows.Forms.Button npMod;
    private System.Windows.Forms.Button npAbs;
    private System.Windows.Forms.Button npTan;
    private System.Windows.Forms.Button npSin;
    private System.Windows.Forms.Button npCos;
    private System.Windows.Forms.Button npExp;
    private System.Windows.Forms.Button npLg;
    private System.Windows.Forms.Button npLn;
    private System.Windows.Forms.Button npLog;
    private System.Windows.Forms.Button npFct;
    private System.Windows.Forms.Button npRtY;
    private System.Windows.Forms.Button npRt2;
    private System.Windows.Forms.Button npDgrY;
    private System.Windows.Forms.Button npDgr2;
    private System.Windows.Forms.Button npDiv;
    private System.Windows.Forms.Button npMul;
    private System.Windows.Forms.Button npPls;
    private System.Windows.Forms.Button npMns;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.MenuItem CopyResult;
    private System.Windows.Forms.MenuItem CopyExpression;
    private System.Windows.Forms.PictureBox pictureBox4;
    #endregion

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalcForm));
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("!(x)");
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("GCD(x)");
      System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Exp(x)");
      System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Default", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
      System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("F(x) = x*Exp(-x)");
      System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Function", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
      System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Ans");
      System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Pi");
      System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("_e");
      System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("@");
      System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Default", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
      System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("x");
      System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("y");
      System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("z");
      System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Register", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
      this.txtExp = new System.Windows.Forms.TextBox();
      this.cmenuHist = new System.Windows.Forms.ContextMenu();
      this.CopyResult = new System.Windows.Forms.MenuItem();
      this.CopyExpression = new System.Windows.Forms.MenuItem();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.CleanPage = new System.Windows.Forms.MenuItem();
      this.SaveAs = new System.Windows.Forms.MenuItem();
      this.listHist = new System.Windows.Forms.ListBox();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.btnClear = new System.Windows.Forms.Button();
      this.InputTrigSystem = new System.Windows.Forms.ComboBox();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnSolve = new System.Windows.Forms.Button();
      this.OutputPrecision = new System.Windows.Forms.ComboBox();
      this.npDel = new System.Windows.Forms.Button();
      this.npAC = new System.Windows.Forms.Button();
      this.npSolve = new System.Windows.Forms.Button();
      this.npGcd = new System.Windows.Forms.Button();
      this.npAbs = new System.Windows.Forms.Button();
      this.npFct = new System.Windows.Forms.Button();
      this.npAns = new System.Windows.Forms.Button();
      this.npSftR = new System.Windows.Forms.Button();
      this.npSftL = new System.Windows.Forms.Button();
      this.npMod = new System.Windows.Forms.Button();
      this.npLg = new System.Windows.Forms.Button();
      this.npLn = new System.Windows.Forms.Button();
      this.npLog = new System.Windows.Forms.Button();
      this.InputCountSystem = new System.Windows.Forms.ComboBox();
      this.OutputCountSystem = new System.Windows.Forms.ComboBox();
      this.statusBar = new System.Windows.Forms.StatusBar();
      this.statusBarSyntax = new System.Windows.Forms.StatusBarPanel();
      this.label2 = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pictureBox4 = new System.Windows.Forms.PictureBox();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.pictureBox3 = new System.Windows.Forms.PictureBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabNum = new System.Windows.Forms.TabPage();
      this.npXor = new System.Windows.Forms.Button();
      this.npOr = new System.Windows.Forms.Button();
      this.npAnd = new System.Windows.Forms.Button();
      this.npNot = new System.Windows.Forms.Button();
      this.npTan = new System.Windows.Forms.Button();
      this.npSin = new System.Windows.Forms.Button();
      this.npCos = new System.Windows.Forms.Button();
      this.npExp = new System.Windows.Forms.Button();
      this.npBrEnd = new System.Windows.Forms.Button();
      this.npBrBeg = new System.Windows.Forms.Button();
      this.npRtY = new System.Windows.Forms.Button();
      this.npRt2 = new System.Windows.Forms.Button();
      this.npDgrY = new System.Windows.Forms.Button();
      this.npDgr2 = new System.Windows.Forms.Button();
      this.npDiv = new System.Windows.Forms.Button();
      this.npMul = new System.Windows.Forms.Button();
      this.npMns = new System.Windows.Forms.Button();
      this.npPls = new System.Windows.Forms.Button();
      this.npDot = new System.Windows.Forms.Button();
      this.np00 = new System.Windows.Forms.Button();
      this.np0 = new System.Windows.Forms.Button();
      this.np3 = new System.Windows.Forms.Button();
      this.np2 = new System.Windows.Forms.Button();
      this.np1 = new System.Windows.Forms.Button();
      this.np6 = new System.Windows.Forms.Button();
      this.np5 = new System.Windows.Forms.Button();
      this.np4 = new System.Windows.Forms.Button();
      this.np9 = new System.Windows.Forms.Button();
      this.np8 = new System.Windows.Forms.Button();
      this.np7 = new System.Windows.Forms.Button();
      this.tabFunc = new System.Windows.Forms.TabPage();
      this.treeAlbom = new System.Windows.Forms.TreeView();
      this.StatusIcons = new System.Windows.Forms.ImageList(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.statusBarSyntax)).BeginInit();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.panel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabNum.SuspendLayout();
      this.tabFunc.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtExp
      // 
      this.txtExp.AcceptsReturn = true;
      this.txtExp.AllowDrop = true;
      this.txtExp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtExp.BackColor = System.Drawing.Color.WhiteSmoke;
      this.txtExp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtExp.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtExp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtExp.ForeColor = System.Drawing.SystemColors.WindowText;
      this.txtExp.HideSelection = false;
      this.txtExp.Location = new System.Drawing.Point(25, 20);
      this.txtExp.Multiline = true;
      this.txtExp.Name = "txtExp";
      this.txtExp.Size = new System.Drawing.Size(354, 31);
      this.txtExp.TabIndex = 0;
      this.toolTip.SetToolTip(this.txtExp, "Type ? to list of supported operations.");
      this.txtExp.WordWrap = false;
      this.txtExp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExp_KeyDown);
      this.txtExp.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtExp_KeyUp);
      this.txtExp.MouseHover += new System.EventHandler(this.txtExp_MouseHover);
      // 
      // cmenuHist
      // 
      this.cmenuHist.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.CopyResult,
            this.CopyExpression,
            this.menuItem1,
            this.CleanPage,
            this.SaveAs});
      // 
      // CopyResult
      // 
      this.CopyResult.Index = 0;
      this.CopyResult.Text = "Copy &Result";
      this.CopyResult.Click += new System.EventHandler(this.CopyResult_Click);
      // 
      // CopyExpression
      // 
      this.CopyExpression.Index = 1;
      this.CopyExpression.Text = "Copy &Expression";
      this.CopyExpression.Click += new System.EventHandler(this.CopyExpression_Click);
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 2;
      this.menuItem1.Text = "-";
      // 
      // CleanPage
      // 
      this.CleanPage.Index = 3;
      this.CleanPage.Text = "&Clean Results";
      this.CleanPage.Click += new System.EventHandler(this.CleanPage_Click);
      // 
      // SaveAs
      // 
      this.SaveAs.Index = 4;
      this.SaveAs.Text = "&Save As...";
      this.SaveAs.Click += new System.EventHandler(this.SaveAs_Click);
      // 
      // listHist
      // 
      this.listHist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.listHist.BackColor = System.Drawing.Color.White;
      this.listHist.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.listHist.ContextMenu = this.cmenuHist;
      this.listHist.Cursor = System.Windows.Forms.Cursors.Default;
      this.listHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.listHist.ForeColor = System.Drawing.SystemColors.WindowText;
      this.listHist.HorizontalScrollbar = true;
      this.listHist.IntegralHeight = false;
      this.listHist.ItemHeight = 24;
      this.listHist.Location = new System.Drawing.Point(0, 24);
      this.listHist.Name = "listHist";
      this.listHist.Size = new System.Drawing.Size(377, 326);
      this.listHist.TabIndex = 1;
      this.toolTip.SetToolTip(this.listHist, "Calculation history pane.");
      this.listHist.DoubleClick += new System.EventHandler(this.listHist_DoubleClick);
      this.listHist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listHist_MouseDown);
      this.listHist.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listHist_KeyUp);
      this.listHist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listHist_KeyDown);
      // 
      // btnClear
      // 
      this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnClear.ForeColor = System.Drawing.Color.DimGray;
      this.btnClear.Location = new System.Drawing.Point(63, 350);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(64, 22);
      this.btnClear.TabIndex = 8;
      this.btnClear.TabStop = false;
      this.btnClear.Text = "Clear";
      this.toolTip.SetToolTip(this.btnClear, "Clear result pane.");
      this.btnClear.Click += new System.EventHandler(this.CleanPage_Click);
      // 
      // InputTrigSystem
      // 
      this.InputTrigSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.InputTrigSystem.Cursor = System.Windows.Forms.Cursors.Default;
      this.InputTrigSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.InputTrigSystem.DropDownWidth = 1;
      this.InputTrigSystem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.InputTrigSystem.IntegralHeight = false;
      this.InputTrigSystem.ItemHeight = 13;
      this.InputTrigSystem.Items.AddRange(new object[] {
            "Degrees",
            "Radians",
            "Grads"});
      this.InputTrigSystem.Location = new System.Drawing.Point(310, -1);
      this.InputTrigSystem.MaxDropDownItems = 3;
      this.InputTrigSystem.Name = "InputTrigSystem";
      this.InputTrigSystem.Size = new System.Drawing.Size(70, 21);
      this.InputTrigSystem.TabIndex = 12;
      this.InputTrigSystem.TabStop = false;
      this.toolTip.SetToolTip(this.InputTrigSystem, "Input trigonomic argument count system.");
      this.InputTrigSystem.MouseHover += new System.EventHandler(this.FocusOnMe);
      this.InputTrigSystem.MouseEnter += new System.EventHandler(this.FocusOnMe);
      this.InputTrigSystem.MouseLeave += new System.EventHandler(this.FocusOnInput);
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSave.ForeColor = System.Drawing.Color.DimGray;
      this.btnSave.Location = new System.Drawing.Point(0, 350);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(64, 22);
      this.btnSave.TabIndex = 14;
      this.btnSave.TabStop = false;
      this.btnSave.Text = "Save";
      this.toolTip.SetToolTip(this.btnSave, "Save results as...");
      this.btnSave.Click += new System.EventHandler(this.SaveAs_Click);
      // 
      // btnSolve
      // 
      this.btnSolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSolve.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnSolve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSolve.ForeColor = System.Drawing.Color.DimGray;
      this.btnSolve.Location = new System.Drawing.Point(63, -1);
      this.btnSolve.Name = "btnSolve";
      this.btnSolve.Size = new System.Drawing.Size(64, 22);
      this.btnSolve.TabIndex = 15;
      this.btnSolve.TabStop = false;
      this.btnSolve.Text = "Solve";
      this.toolTip.SetToolTip(this.btnSolve, "Solve the input expression and diplaye result in Result panel (Ctrl+Enter).");
      this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
      // 
      // OutputPrecision
      // 
      this.OutputPrecision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OutputPrecision.Cursor = System.Windows.Forms.Cursors.Default;
      this.OutputPrecision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.OutputPrecision.DropDownWidth = 1;
      this.OutputPrecision.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.OutputPrecision.IntegralHeight = false;
      this.OutputPrecision.ItemHeight = 13;
      this.OutputPrecision.Items.AddRange(new object[] {
            "Default",
            "Roundtrip",
            "0",
            "1",
            "2",
            "3",
            "5",
            "8",
            "13"});
      this.OutputPrecision.Location = new System.Drawing.Point(310, 351);
      this.OutputPrecision.MaxDropDownItems = 9;
      this.OutputPrecision.Name = "OutputPrecision";
      this.OutputPrecision.Size = new System.Drawing.Size(70, 21);
      this.OutputPrecision.TabIndex = 15;
      this.OutputPrecision.TabStop = false;
      this.toolTip.SetToolTip(this.OutputPrecision, "Output precision.");
      this.OutputPrecision.MouseHover += new System.EventHandler(this.FocusOnMe);
      this.OutputPrecision.MouseEnter += new System.EventHandler(this.FocusOnMe);
      this.OutputPrecision.MouseLeave += new System.EventHandler(this.FocusOnInput);
      // 
      // npDel
      // 
      this.npDel.BackColor = System.Drawing.Color.Firebrick;
      this.npDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.npDel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.npDel.ForeColor = System.Drawing.Color.White;
      this.npDel.Location = new System.Drawing.Point(55, 5);
      this.npDel.Name = "npDel";
      this.npDel.Size = new System.Drawing.Size(37, 27);
      this.npDel.TabIndex = 12;
      this.npDel.TabStop = false;
      this.npDel.Text = "DEL";
      this.toolTip.SetToolTip(this.npDel, "Undo last hit");
      this.npDel.UseVisualStyleBackColor = false;
      this.npDel.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npAC
      // 
      this.npAC.BackColor = System.Drawing.Color.IndianRed;
      this.npAC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.npAC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.npAC.ForeColor = System.Drawing.Color.White;
      this.npAC.Location = new System.Drawing.Point(100, 5);
      this.npAC.Name = "npAC";
      this.npAC.Size = new System.Drawing.Size(37, 27);
      this.npAC.TabIndex = 13;
      this.npAC.TabStop = false;
      this.npAC.Text = "AC";
      this.toolTip.SetToolTip(this.npAC, "Reset");
      this.npAC.UseVisualStyleBackColor = false;
      this.npAC.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npSolve
      // 
      this.npSolve.BackColor = System.Drawing.Color.LightSteelBlue;
      this.npSolve.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.npSolve.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.npSolve.Location = new System.Drawing.Point(1, 180);
      this.npSolve.Name = "npSolve";
      this.npSolve.Size = new System.Drawing.Size(71, 27);
      this.npSolve.TabIndex = 14;
      this.npSolve.TabStop = false;
      this.npSolve.Text = "=";
      this.toolTip.SetToolTip(this.npSolve, "Solve");
      this.npSolve.UseVisualStyleBackColor = false;
      this.npSolve.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npGcd
      // 
      this.npGcd.BackColor = System.Drawing.Color.MidnightBlue;
      this.npGcd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npGcd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npGcd.ForeColor = System.Drawing.Color.Aquamarine;
      this.npGcd.Location = new System.Drawing.Point(1, 371);
      this.npGcd.Name = "npGcd";
      this.npGcd.Size = new System.Drawing.Size(36, 27);
      this.npGcd.TabIndex = 42;
      this.npGcd.TabStop = false;
      this.npGcd.Text = "Gcd";
      this.toolTip.SetToolTip(this.npGcd, "Great Common Devisor");
      this.npGcd.UseVisualStyleBackColor = false;
      this.npGcd.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npAbs
      // 
      this.npAbs.BackColor = System.Drawing.Color.MidnightBlue;
      this.npAbs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npAbs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npAbs.ForeColor = System.Drawing.Color.Aquamarine;
      this.npAbs.Location = new System.Drawing.Point(71, 371);
      this.npAbs.Name = "npAbs";
      this.npAbs.Size = new System.Drawing.Size(36, 27);
      this.npAbs.TabIndex = 34;
      this.npAbs.TabStop = false;
      this.npAbs.Text = "Abs";
      this.toolTip.SetToolTip(this.npAbs, "Absolute value");
      this.npAbs.UseVisualStyleBackColor = false;
      this.npAbs.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npFct
      // 
      this.npFct.BackColor = System.Drawing.Color.MidnightBlue;
      this.npFct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npFct.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npFct.ForeColor = System.Drawing.Color.Aquamarine;
      this.npFct.Location = new System.Drawing.Point(36, 371);
      this.npFct.Name = "npFct";
      this.npFct.Size = new System.Drawing.Size(36, 27);
      this.npFct.TabIndex = 26;
      this.npFct.TabStop = false;
      this.npFct.Text = "!x";
      this.toolTip.SetToolTip(this.npFct, "Factorial");
      this.npFct.UseVisualStyleBackColor = false;
      this.npFct.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npAns
      // 
      this.npAns.BackColor = System.Drawing.Color.DarkSeaGreen;
      this.npAns.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.npAns.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.npAns.Location = new System.Drawing.Point(10, 5);
      this.npAns.Name = "npAns";
      this.npAns.Size = new System.Drawing.Size(37, 27);
      this.npAns.TabIndex = 23;
      this.npAns.TabStop = false;
      this.npAns.Text = "ANS";
      this.toolTip.SetToolTip(this.npAns, "Answer register");
      this.npAns.UseVisualStyleBackColor = false;
      this.npAns.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npSftR
      // 
      this.npSftR.BackColor = System.Drawing.Color.SlateBlue;
      this.npSftR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npSftR.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npSftR.ForeColor = System.Drawing.Color.Aquamarine;
      this.npSftR.Location = new System.Drawing.Point(36, 345);
      this.npSftR.Name = "npSftR";
      this.npSftR.Size = new System.Drawing.Size(36, 27);
      this.npSftR.TabIndex = 41;
      this.npSftR.TabStop = false;
      this.npSftR.Text = ">>";
      this.toolTip.SetToolTip(this.npSftR, "Shift Right");
      this.npSftR.UseVisualStyleBackColor = false;
      this.npSftR.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npSftL
      // 
      this.npSftL.BackColor = System.Drawing.Color.SlateBlue;
      this.npSftL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npSftL.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npSftL.ForeColor = System.Drawing.Color.Aquamarine;
      this.npSftL.Location = new System.Drawing.Point(1, 345);
      this.npSftL.Name = "npSftL";
      this.npSftL.Size = new System.Drawing.Size(36, 27);
      this.npSftL.TabIndex = 40;
      this.npSftL.TabStop = false;
      this.npSftL.Text = "<<";
      this.toolTip.SetToolTip(this.npSftL, "Shift Left");
      this.npSftL.UseVisualStyleBackColor = false;
      this.npSftL.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npMod
      // 
      this.npMod.BackColor = System.Drawing.Color.MidnightBlue;
      this.npMod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npMod.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npMod.ForeColor = System.Drawing.Color.Aquamarine;
      this.npMod.Location = new System.Drawing.Point(106, 371);
      this.npMod.Name = "npMod";
      this.npMod.Size = new System.Drawing.Size(36, 27);
      this.npMod.TabIndex = 35;
      this.npMod.TabStop = false;
      this.npMod.Text = "Mod";
      this.toolTip.SetToolTip(this.npMod, "Reminder");
      this.npMod.UseVisualStyleBackColor = false;
      this.npMod.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npLg
      // 
      this.npLg.BackColor = System.Drawing.Color.SlateBlue;
      this.npLg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npLg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npLg.ForeColor = System.Drawing.Color.Aquamarine;
      this.npLg.Location = new System.Drawing.Point(106, 267);
      this.npLg.Name = "npLg";
      this.npLg.Size = new System.Drawing.Size(36, 27);
      this.npLg.TabIndex = 29;
      this.npLg.TabStop = false;
      this.npLg.Text = "Lg";
      this.toolTip.SetToolTip(this.npLg, "Logarithm 2");
      this.npLg.UseVisualStyleBackColor = false;
      this.npLg.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npLn
      // 
      this.npLn.BackColor = System.Drawing.Color.SlateBlue;
      this.npLn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npLn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npLn.ForeColor = System.Drawing.Color.Aquamarine;
      this.npLn.Location = new System.Drawing.Point(71, 267);
      this.npLn.Name = "npLn";
      this.npLn.Size = new System.Drawing.Size(36, 27);
      this.npLn.TabIndex = 28;
      this.npLn.TabStop = false;
      this.npLn.Text = "Ln";
      this.toolTip.SetToolTip(this.npLn, "Logarithm ℮");
      this.npLn.UseVisualStyleBackColor = false;
      this.npLn.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npLog
      // 
      this.npLog.BackColor = System.Drawing.Color.SlateBlue;
      this.npLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npLog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npLog.ForeColor = System.Drawing.Color.Aquamarine;
      this.npLog.Location = new System.Drawing.Point(36, 267);
      this.npLog.Name = "npLog";
      this.npLog.Size = new System.Drawing.Size(36, 27);
      this.npLog.TabIndex = 27;
      this.npLog.TabStop = false;
      this.npLog.Text = "Log";
      this.toolTip.SetToolTip(this.npLog, "Logarithm 10");
      this.npLog.UseVisualStyleBackColor = false;
      this.npLog.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // InputCountSystem
      // 
      this.InputCountSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.InputCountSystem.Cursor = System.Windows.Forms.Cursors.Default;
      this.InputCountSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.InputCountSystem.DropDownWidth = 1;
      this.InputCountSystem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.InputCountSystem.IntegralHeight = false;
      this.InputCountSystem.ItemHeight = 13;
      this.InputCountSystem.Items.AddRange(new object[] {
            "Hexadecimal [0-F]",
            "Decimal [0-9]",
            "Octal [0-7]",
            "Binary [0-1]"});
      this.InputCountSystem.Location = new System.Drawing.Point(176, -1);
      this.InputCountSystem.MaxDropDownItems = 4;
      this.InputCountSystem.Name = "InputCountSystem";
      this.InputCountSystem.Size = new System.Drawing.Size(111, 21);
      this.InputCountSystem.TabIndex = 11;
      this.InputCountSystem.TabStop = false;
      this.InputCountSystem.MouseHover += new System.EventHandler(this.FocusOnMe);
      this.InputCountSystem.SelectedIndexChanged += new System.EventHandler(this.InputCountSystem_SelectedIndexChanged);
      this.InputCountSystem.MouseEnter += new System.EventHandler(this.FocusOnMe);
      this.InputCountSystem.MouseLeave += new System.EventHandler(this.FocusOnInput);
      // 
      // OutputCountSystem
      // 
      this.OutputCountSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OutputCountSystem.Cursor = System.Windows.Forms.Cursors.Default;
      this.OutputCountSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.OutputCountSystem.DropDownWidth = 1;
      this.OutputCountSystem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.OutputCountSystem.IntegralHeight = false;
      this.OutputCountSystem.ItemHeight = 13;
      this.OutputCountSystem.Items.AddRange(new object[] {
            "Hexadecimal [0-F]",
            "Decimal [0-9]",
            "Octal [0-7]",
            "Binary [0-1]"});
      this.OutputCountSystem.Location = new System.Drawing.Point(176, 351);
      this.OutputCountSystem.MaxDropDownItems = 4;
      this.OutputCountSystem.Name = "OutputCountSystem";
      this.OutputCountSystem.Size = new System.Drawing.Size(111, 21);
      this.OutputCountSystem.TabIndex = 13;
      this.OutputCountSystem.TabStop = false;
      this.OutputCountSystem.MouseHover += new System.EventHandler(this.FocusOnMe);
      this.OutputCountSystem.SelectedIndexChanged += new System.EventHandler(this.OutputCountSystem_SelectedIndexChanged);
      this.OutputCountSystem.MouseEnter += new System.EventHandler(this.FocusOnMe);
      this.OutputCountSystem.MouseLeave += new System.EventHandler(this.FocusOnInput);
      // 
      // statusBar
      // 
      this.statusBar.Location = new System.Drawing.Point(0, 434);
      this.statusBar.Name = "statusBar";
      this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarSyntax});
      this.statusBar.ShowPanels = true;
      this.statusBar.Size = new System.Drawing.Size(542, 20);
      this.statusBar.TabIndex = 6;
      this.statusBar.Text = "Ready";
      // 
      // statusBarSyntax
      // 
      this.statusBarSyntax.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
      this.statusBarSyntax.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
      this.statusBarSyntax.Icon = ((System.Drawing.Icon)(resources.GetObject("statusBarSyntax.Icon")));
      this.statusBarSyntax.MinWidth = 100;
      this.statusBarSyntax.Name = "statusBarSyntax";
      this.statusBarSyntax.Text = "Syntax";
      this.statusBarSyntax.ToolTipText = "Operational status informer.";
      // 
      // label2
      // 
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.Black;
      this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.label2.Location = new System.Drawing.Point(1, 2);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(95, 18);
      this.label2.TabIndex = 7;
      this.label2.Text = "Result ƒ";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.label2.UseMnemonic = false;
      // 
      // panel1
      // 
      this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panel1.BackColor = System.Drawing.Color.Transparent;
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel1.Controls.Add(this.pictureBox4);
      this.panel1.Controls.Add(this.pictureBox2);
      this.panel1.Controls.Add(this.InputTrigSystem);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.txtExp);
      this.panel1.Controls.Add(this.InputCountSystem);
      this.panel1.Controls.Add(this.btnSolve);
      this.panel1.Controls.Add(this.label1);
      this.panel1.ForeColor = System.Drawing.Color.Transparent;
      this.panel1.Location = new System.Drawing.Point(1, 379);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(380, 52);
      this.panel1.TabIndex = 8;
      // 
      // pictureBox4
      // 
      this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
      this.pictureBox4.Location = new System.Drawing.Point(292, 2);
      this.pictureBox4.Name = "pictureBox4";
      this.pictureBox4.Size = new System.Drawing.Size(16, 16);
      this.pictureBox4.TabIndex = 18;
      this.pictureBox4.TabStop = false;
      // 
      // pictureBox2
      // 
      this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
      this.pictureBox2.Location = new System.Drawing.Point(156, 2);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(16, 16);
      this.pictureBox2.TabIndex = 17;
      this.pictureBox2.TabStop = false;
      // 
      // label3
      // 
      this.label3.BackColor = System.Drawing.Color.Transparent;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.ForeColor = System.Drawing.Color.Black;
      this.label3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.label3.Location = new System.Drawing.Point(1, 1);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(59, 18);
      this.label3.TabIndex = 9;
      this.label3.Text = "Input";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.label3.UseMnemonic = false;
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.Black;
      this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.label1.Location = new System.Drawing.Point(0, 26);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(25, 18);
      this.label1.TabIndex = 10;
      this.label1.Text = "ƒ›";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.label1.UseMnemonic = false;
      // 
      // panel3
      // 
      this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panel3.BackColor = System.Drawing.Color.Transparent;
      this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel3.Controls.Add(this.pictureBox3);
      this.panel3.Controls.Add(this.pictureBox1);
      this.panel3.Controls.Add(this.OutputPrecision);
      this.panel3.Controls.Add(this.btnSave);
      this.panel3.Controls.Add(this.btnClear);
      this.panel3.Controls.Add(this.label2);
      this.panel3.Controls.Add(this.listHist);
      this.panel3.Controls.Add(this.OutputCountSystem);
      this.panel3.ForeColor = System.Drawing.Color.IndianRed;
      this.panel3.Location = new System.Drawing.Point(1, 2);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(380, 374);
      this.panel3.TabIndex = 10;
      // 
      // pictureBox3
      // 
      this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
      this.pictureBox3.Location = new System.Drawing.Point(292, 354);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new System.Drawing.Size(17, 16);
      this.pictureBox3.TabIndex = 17;
      this.pictureBox3.TabStop = false;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(156, 354);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(16, 16);
      this.pictureBox1.TabIndex = 16;
      this.pictureBox1.TabStop = false;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabNum);
      this.tabControl1.Controls.Add(this.tabFunc);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Right;
      this.tabControl1.Location = new System.Drawing.Point(383, 0);
      this.tabControl1.Multiline = true;
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(159, 434);
      this.tabControl1.TabIndex = 11;
      this.tabControl1.TabStop = false;
      // 
      // tabNum
      // 
      this.tabNum.BackColor = System.Drawing.Color.Transparent;
      this.tabNum.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabNum.BackgroundImage")));
      this.tabNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.tabNum.Controls.Add(this.npGcd);
      this.tabNum.Controls.Add(this.npSftR);
      this.tabNum.Controls.Add(this.npSftL);
      this.tabNum.Controls.Add(this.npXor);
      this.tabNum.Controls.Add(this.npOr);
      this.tabNum.Controls.Add(this.npAnd);
      this.tabNum.Controls.Add(this.npNot);
      this.tabNum.Controls.Add(this.npMod);
      this.tabNum.Controls.Add(this.npAbs);
      this.tabNum.Controls.Add(this.npTan);
      this.tabNum.Controls.Add(this.npSin);
      this.tabNum.Controls.Add(this.npCos);
      this.tabNum.Controls.Add(this.npExp);
      this.tabNum.Controls.Add(this.npLg);
      this.tabNum.Controls.Add(this.npLn);
      this.tabNum.Controls.Add(this.npLog);
      this.tabNum.Controls.Add(this.npFct);
      this.tabNum.Controls.Add(this.npBrEnd);
      this.tabNum.Controls.Add(this.npBrBeg);
      this.tabNum.Controls.Add(this.npAns);
      this.tabNum.Controls.Add(this.npRtY);
      this.tabNum.Controls.Add(this.npRt2);
      this.tabNum.Controls.Add(this.npDgrY);
      this.tabNum.Controls.Add(this.npDgr2);
      this.tabNum.Controls.Add(this.npDiv);
      this.tabNum.Controls.Add(this.npMul);
      this.tabNum.Controls.Add(this.npMns);
      this.tabNum.Controls.Add(this.npPls);
      this.tabNum.Controls.Add(this.npSolve);
      this.tabNum.Controls.Add(this.npAC);
      this.tabNum.Controls.Add(this.npDel);
      this.tabNum.Controls.Add(this.npDot);
      this.tabNum.Controls.Add(this.np00);
      this.tabNum.Controls.Add(this.np0);
      this.tabNum.Controls.Add(this.np3);
      this.tabNum.Controls.Add(this.np2);
      this.tabNum.Controls.Add(this.np1);
      this.tabNum.Controls.Add(this.np6);
      this.tabNum.Controls.Add(this.np5);
      this.tabNum.Controls.Add(this.np4);
      this.tabNum.Controls.Add(this.np9);
      this.tabNum.Controls.Add(this.np8);
      this.tabNum.Controls.Add(this.np7);
      this.tabNum.Location = new System.Drawing.Point(4, 22);
      this.tabNum.Name = "tabNum";
      this.tabNum.Size = new System.Drawing.Size(151, 408);
      this.tabNum.TabIndex = 1;
      this.tabNum.Text = "NumPad";
      this.tabNum.UseVisualStyleBackColor = true;
      // 
      // npXor
      // 
      this.npXor.BackColor = System.Drawing.Color.DarkSlateBlue;
      this.npXor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npXor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npXor.ForeColor = System.Drawing.Color.Aquamarine;
      this.npXor.Location = new System.Drawing.Point(71, 319);
      this.npXor.Name = "npXor";
      this.npXor.Size = new System.Drawing.Size(36, 27);
      this.npXor.TabIndex = 39;
      this.npXor.TabStop = false;
      this.npXor.Text = "Xor";
      this.npXor.UseVisualStyleBackColor = false;
      this.npXor.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npOr
      // 
      this.npOr.BackColor = System.Drawing.Color.DarkSlateBlue;
      this.npOr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npOr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npOr.ForeColor = System.Drawing.Color.Aquamarine;
      this.npOr.Location = new System.Drawing.Point(36, 319);
      this.npOr.Name = "npOr";
      this.npOr.Size = new System.Drawing.Size(36, 27);
      this.npOr.TabIndex = 38;
      this.npOr.TabStop = false;
      this.npOr.Text = "Or";
      this.npOr.UseVisualStyleBackColor = false;
      this.npOr.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npAnd
      // 
      this.npAnd.BackColor = System.Drawing.Color.DarkSlateBlue;
      this.npAnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npAnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npAnd.ForeColor = System.Drawing.Color.Aquamarine;
      this.npAnd.Location = new System.Drawing.Point(1, 319);
      this.npAnd.Name = "npAnd";
      this.npAnd.Size = new System.Drawing.Size(36, 27);
      this.npAnd.TabIndex = 37;
      this.npAnd.TabStop = false;
      this.npAnd.Text = "And";
      this.npAnd.UseVisualStyleBackColor = false;
      this.npAnd.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npNot
      // 
      this.npNot.BackColor = System.Drawing.Color.DarkSlateBlue;
      this.npNot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npNot.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npNot.ForeColor = System.Drawing.Color.Aquamarine;
      this.npNot.Location = new System.Drawing.Point(106, 319);
      this.npNot.Name = "npNot";
      this.npNot.Size = new System.Drawing.Size(36, 27);
      this.npNot.TabIndex = 36;
      this.npNot.TabStop = false;
      this.npNot.Text = "Not";
      this.npNot.UseVisualStyleBackColor = false;
      this.npNot.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npTan
      // 
      this.npTan.BackColor = System.Drawing.Color.MidnightBlue;
      this.npTan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npTan.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npTan.ForeColor = System.Drawing.Color.Aquamarine;
      this.npTan.Location = new System.Drawing.Point(71, 293);
      this.npTan.Name = "npTan";
      this.npTan.Size = new System.Drawing.Size(36, 27);
      this.npTan.TabIndex = 33;
      this.npTan.TabStop = false;
      this.npTan.Text = "Tan";
      this.npTan.UseVisualStyleBackColor = false;
      this.npTan.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npSin
      // 
      this.npSin.BackColor = System.Drawing.Color.MidnightBlue;
      this.npSin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npSin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npSin.ForeColor = System.Drawing.Color.Aquamarine;
      this.npSin.Location = new System.Drawing.Point(1, 293);
      this.npSin.Name = "npSin";
      this.npSin.Size = new System.Drawing.Size(36, 27);
      this.npSin.TabIndex = 32;
      this.npSin.TabStop = false;
      this.npSin.Text = "Sin";
      this.npSin.UseVisualStyleBackColor = false;
      this.npSin.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npCos
      // 
      this.npCos.BackColor = System.Drawing.Color.MidnightBlue;
      this.npCos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npCos.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npCos.ForeColor = System.Drawing.Color.Aquamarine;
      this.npCos.Location = new System.Drawing.Point(36, 293);
      this.npCos.Name = "npCos";
      this.npCos.Size = new System.Drawing.Size(36, 27);
      this.npCos.TabIndex = 31;
      this.npCos.TabStop = false;
      this.npCos.Text = "Cos";
      this.npCos.UseVisualStyleBackColor = false;
      this.npCos.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npExp
      // 
      this.npExp.BackColor = System.Drawing.Color.SlateBlue;
      this.npExp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npExp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npExp.ForeColor = System.Drawing.Color.Aquamarine;
      this.npExp.Location = new System.Drawing.Point(1, 267);
      this.npExp.Name = "npExp";
      this.npExp.Size = new System.Drawing.Size(36, 27);
      this.npExp.TabIndex = 30;
      this.npExp.TabStop = false;
      this.npExp.Text = "℮^x";
      this.npExp.UseVisualStyleBackColor = false;
      this.npExp.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npBrEnd
      // 
      this.npBrEnd.BackColor = System.Drawing.Color.LightSlateGray;
      this.npBrEnd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.npBrEnd.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npBrEnd.ForeColor = System.Drawing.Color.PowderBlue;
      this.npBrEnd.Location = new System.Drawing.Point(106, 180);
      this.npBrEnd.Name = "npBrEnd";
      this.npBrEnd.Size = new System.Drawing.Size(36, 27);
      this.npBrEnd.TabIndex = 25;
      this.npBrEnd.TabStop = false;
      this.npBrEnd.Text = ")";
      this.npBrEnd.UseVisualStyleBackColor = false;
      this.npBrEnd.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npBrBeg
      // 
      this.npBrBeg.BackColor = System.Drawing.Color.LightSlateGray;
      this.npBrBeg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.npBrBeg.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npBrBeg.ForeColor = System.Drawing.Color.PowderBlue;
      this.npBrBeg.Location = new System.Drawing.Point(71, 180);
      this.npBrBeg.Name = "npBrBeg";
      this.npBrBeg.Size = new System.Drawing.Size(36, 27);
      this.npBrBeg.TabIndex = 24;
      this.npBrBeg.TabStop = false;
      this.npBrBeg.Text = "(";
      this.npBrBeg.UseVisualStyleBackColor = false;
      this.npBrBeg.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npRtY
      // 
      this.npRtY.BackColor = System.Drawing.Color.DarkSlateBlue;
      this.npRtY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npRtY.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npRtY.ForeColor = System.Drawing.Color.Aquamarine;
      this.npRtY.Location = new System.Drawing.Point(106, 241);
      this.npRtY.Name = "npRtY";
      this.npRtY.Size = new System.Drawing.Size(36, 27);
      this.npRtY.TabIndex = 22;
      this.npRtY.TabStop = false;
      this.npRtY.Text = "n√x";
      this.npRtY.UseVisualStyleBackColor = false;
      this.npRtY.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npRt2
      // 
      this.npRt2.BackColor = System.Drawing.Color.DarkSlateBlue;
      this.npRt2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npRt2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npRt2.ForeColor = System.Drawing.Color.Aquamarine;
      this.npRt2.Location = new System.Drawing.Point(71, 241);
      this.npRt2.Name = "npRt2";
      this.npRt2.Size = new System.Drawing.Size(36, 27);
      this.npRt2.TabIndex = 21;
      this.npRt2.TabStop = false;
      this.npRt2.Text = "2√x";
      this.npRt2.UseVisualStyleBackColor = false;
      this.npRt2.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npDgrY
      // 
      this.npDgrY.BackColor = System.Drawing.Color.DarkSlateBlue;
      this.npDgrY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npDgrY.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npDgrY.ForeColor = System.Drawing.Color.Aquamarine;
      this.npDgrY.Location = new System.Drawing.Point(36, 241);
      this.npDgrY.Name = "npDgrY";
      this.npDgrY.Size = new System.Drawing.Size(36, 27);
      this.npDgrY.TabIndex = 20;
      this.npDgrY.TabStop = false;
      this.npDgrY.Text = "x^n";
      this.npDgrY.UseVisualStyleBackColor = false;
      this.npDgrY.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npDgr2
      // 
      this.npDgr2.BackColor = System.Drawing.Color.DarkSlateBlue;
      this.npDgr2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npDgr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npDgr2.ForeColor = System.Drawing.Color.Aquamarine;
      this.npDgr2.Location = new System.Drawing.Point(1, 241);
      this.npDgr2.Name = "npDgr2";
      this.npDgr2.Size = new System.Drawing.Size(36, 27);
      this.npDgr2.TabIndex = 19;
      this.npDgr2.TabStop = false;
      this.npDgr2.Text = "x^2";
      this.npDgr2.UseVisualStyleBackColor = false;
      this.npDgr2.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npDiv
      // 
      this.npDiv.BackColor = System.Drawing.Color.MidnightBlue;
      this.npDiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npDiv.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npDiv.ForeColor = System.Drawing.Color.Aquamarine;
      this.npDiv.Location = new System.Drawing.Point(106, 215);
      this.npDiv.Name = "npDiv";
      this.npDiv.Size = new System.Drawing.Size(36, 27);
      this.npDiv.TabIndex = 18;
      this.npDiv.TabStop = false;
      this.npDiv.Text = "÷";
      this.npDiv.UseVisualStyleBackColor = false;
      this.npDiv.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npMul
      // 
      this.npMul.BackColor = System.Drawing.Color.MidnightBlue;
      this.npMul.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npMul.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npMul.ForeColor = System.Drawing.Color.Aquamarine;
      this.npMul.Location = new System.Drawing.Point(71, 215);
      this.npMul.Name = "npMul";
      this.npMul.Size = new System.Drawing.Size(36, 27);
      this.npMul.TabIndex = 17;
      this.npMul.TabStop = false;
      this.npMul.Text = "×";
      this.npMul.UseVisualStyleBackColor = false;
      this.npMul.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npMns
      // 
      this.npMns.BackColor = System.Drawing.Color.MidnightBlue;
      this.npMns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npMns.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npMns.ForeColor = System.Drawing.Color.Aquamarine;
      this.npMns.Location = new System.Drawing.Point(36, 215);
      this.npMns.Name = "npMns";
      this.npMns.Size = new System.Drawing.Size(36, 27);
      this.npMns.TabIndex = 16;
      this.npMns.TabStop = false;
      this.npMns.Text = "−";
      this.npMns.UseVisualStyleBackColor = false;
      this.npMns.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npPls
      // 
      this.npPls.BackColor = System.Drawing.Color.MidnightBlue;
      this.npPls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npPls.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npPls.ForeColor = System.Drawing.Color.Aquamarine;
      this.npPls.Location = new System.Drawing.Point(1, 215);
      this.npPls.Name = "npPls";
      this.npPls.Size = new System.Drawing.Size(36, 27);
      this.npPls.TabIndex = 15;
      this.npPls.TabStop = false;
      this.npPls.Text = "+";
      this.npPls.UseVisualStyleBackColor = false;
      this.npPls.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // npDot
      // 
      this.npDot.BackColor = System.Drawing.Color.Silver;
      this.npDot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.npDot.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.npDot.Location = new System.Drawing.Point(100, 145);
      this.npDot.Name = "npDot";
      this.npDot.Size = new System.Drawing.Size(36, 27);
      this.npDot.TabIndex = 11;
      this.npDot.TabStop = false;
      this.npDot.Text = ".";
      this.npDot.UseVisualStyleBackColor = false;
      this.npDot.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np00
      // 
      this.np00.BackColor = System.Drawing.Color.Silver;
      this.np00.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np00.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.np00.Location = new System.Drawing.Point(55, 145);
      this.np00.Name = "np00";
      this.np00.Size = new System.Drawing.Size(36, 27);
      this.np00.TabIndex = 10;
      this.np00.TabStop = false;
      this.np00.Text = "00";
      this.np00.UseVisualStyleBackColor = false;
      this.np00.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np0
      // 
      this.np0.BackColor = System.Drawing.Color.Silver;
      this.np0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np0.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.np0.Location = new System.Drawing.Point(10, 145);
      this.np0.Name = "np0";
      this.np0.Size = new System.Drawing.Size(36, 27);
      this.np0.TabIndex = 9;
      this.np0.TabStop = false;
      this.np0.Text = "0";
      this.np0.UseVisualStyleBackColor = false;
      this.np0.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np3
      // 
      this.np3.BackColor = System.Drawing.Color.Silver;
      this.np3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.np3.Location = new System.Drawing.Point(100, 110);
      this.np3.Name = "np3";
      this.np3.Size = new System.Drawing.Size(36, 27);
      this.np3.TabIndex = 8;
      this.np3.TabStop = false;
      this.np3.Text = "3";
      this.np3.UseVisualStyleBackColor = false;
      this.np3.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np2
      // 
      this.np2.BackColor = System.Drawing.Color.Silver;
      this.np2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.np2.Location = new System.Drawing.Point(55, 110);
      this.np2.Name = "np2";
      this.np2.Size = new System.Drawing.Size(36, 27);
      this.np2.TabIndex = 7;
      this.np2.TabStop = false;
      this.np2.Text = "2";
      this.np2.UseVisualStyleBackColor = false;
      this.np2.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np1
      // 
      this.np1.BackColor = System.Drawing.Color.Silver;
      this.np1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.np1.Location = new System.Drawing.Point(10, 110);
      this.np1.Name = "np1";
      this.np1.Size = new System.Drawing.Size(36, 27);
      this.np1.TabIndex = 6;
      this.np1.TabStop = false;
      this.np1.Text = "1";
      this.np1.UseVisualStyleBackColor = false;
      this.np1.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np6
      // 
      this.np6.BackColor = System.Drawing.Color.Silver;
      this.np6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.np6.Location = new System.Drawing.Point(100, 75);
      this.np6.Name = "np6";
      this.np6.Size = new System.Drawing.Size(36, 27);
      this.np6.TabIndex = 5;
      this.np6.TabStop = false;
      this.np6.Text = "6";
      this.np6.UseVisualStyleBackColor = false;
      this.np6.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np5
      // 
      this.np5.BackColor = System.Drawing.Color.Silver;
      this.np5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.np5.Location = new System.Drawing.Point(55, 75);
      this.np5.Name = "np5";
      this.np5.Size = new System.Drawing.Size(36, 27);
      this.np5.TabIndex = 4;
      this.np5.TabStop = false;
      this.np5.Text = "5";
      this.np5.UseVisualStyleBackColor = false;
      this.np5.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np4
      // 
      this.np4.BackColor = System.Drawing.Color.Silver;
      this.np4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.np4.Location = new System.Drawing.Point(10, 75);
      this.np4.Name = "np4";
      this.np4.Size = new System.Drawing.Size(36, 27);
      this.np4.TabIndex = 3;
      this.np4.TabStop = false;
      this.np4.Text = "4";
      this.np4.UseVisualStyleBackColor = false;
      this.np4.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np9
      // 
      this.np9.BackColor = System.Drawing.Color.Silver;
      this.np9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np9.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.np9.Location = new System.Drawing.Point(100, 40);
      this.np9.Name = "np9";
      this.np9.Size = new System.Drawing.Size(36, 27);
      this.np9.TabIndex = 2;
      this.np9.TabStop = false;
      this.np9.Text = "9";
      this.np9.UseVisualStyleBackColor = false;
      this.np9.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np8
      // 
      this.np8.BackColor = System.Drawing.Color.Silver;
      this.np8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.np8.Location = new System.Drawing.Point(55, 40);
      this.np8.Name = "np8";
      this.np8.Size = new System.Drawing.Size(36, 27);
      this.np8.TabIndex = 1;
      this.np8.TabStop = false;
      this.np8.Text = "8";
      this.np8.UseVisualStyleBackColor = false;
      this.np8.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // np7
      // 
      this.np7.BackColor = System.Drawing.Color.Silver;
      this.np7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.np7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.np7.Location = new System.Drawing.Point(10, 40);
      this.np7.Name = "np7";
      this.np7.Size = new System.Drawing.Size(36, 27);
      this.np7.TabIndex = 0;
      this.np7.TabStop = false;
      this.np7.Text = "7";
      this.np7.UseVisualStyleBackColor = false;
      this.np7.Click += new System.EventHandler(this.NumPad_BTN_Click);
      // 
      // tabFunc
      // 
      this.tabFunc.BackColor = System.Drawing.Color.Transparent;
      this.tabFunc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabFunc.BackgroundImage")));
      this.tabFunc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.tabFunc.Controls.Add(this.treeAlbom);
      this.tabFunc.Location = new System.Drawing.Point(4, 22);
      this.tabFunc.Name = "tabFunc";
      this.tabFunc.Size = new System.Drawing.Size(151, 408);
      this.tabFunc.TabIndex = 0;
      this.tabFunc.Text = "Collection";
      this.tabFunc.UseVisualStyleBackColor = true;
      // 
      // treeAlbom
      // 
      this.treeAlbom.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.treeAlbom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeAlbom.FullRowSelect = true;
      this.treeAlbom.HideSelection = false;
      this.treeAlbom.ImageIndex = 0;
      this.treeAlbom.ImageList = this.StatusIcons;
      this.treeAlbom.Indent = 19;
      this.treeAlbom.Location = new System.Drawing.Point(0, 0);
      this.treeAlbom.Name = "treeAlbom";
      treeNode1.Name = "";
      treeNode1.Text = "!(x)";
      treeNode2.Name = "";
      treeNode2.Text = "GCD(x)";
      treeNode3.Name = "";
      treeNode3.Text = "Exp(x)";
      treeNode4.Name = "";
      treeNode4.Text = "Default";
      treeNode5.Name = "";
      treeNode5.Text = "F(x) = x*Exp(-x)";
      treeNode6.Name = "";
      treeNode6.Text = "Function";
      treeNode7.Name = "";
      treeNode7.Text = "Ans";
      treeNode8.Name = "";
      treeNode8.Text = "Pi";
      treeNode9.Name = "";
      treeNode9.Text = "_e";
      treeNode10.Name = "";
      treeNode10.Text = "@";
      treeNode11.Name = "";
      treeNode11.Text = "Default";
      treeNode12.Name = "";
      treeNode12.Text = "x";
      treeNode13.Name = "";
      treeNode13.Text = "y";
      treeNode14.Name = "";
      treeNode14.Text = "z";
      treeNode15.Name = "";
      treeNode15.Text = "Register";
      this.treeAlbom.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode15});
      this.treeAlbom.SelectedImageIndex = 0;
      this.treeAlbom.Size = new System.Drawing.Size(147, 404);
      this.treeAlbom.TabIndex = 0;
      // 
      // StatusIcons
      // 
      this.StatusIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("StatusIcons.ImageStream")));
      this.StatusIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.StatusIcons.Images.SetKeyName(0, "");
      this.StatusIcons.Images.SetKeyName(1, "");
      // 
      // CalcForm
      // 
      this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.BackColor = System.Drawing.SystemColors.Control;
      this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
      this.ClientSize = new System.Drawing.Size(542, 454);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.statusBar);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.panel3);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(550, 483);
      this.Name = "CalcForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Load += new System.EventHandler(this.Form_Load);
      ((System.ComponentModel.ISupportInitialize)(this.statusBarSyntax)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.panel3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabNum.ResumeLayout(false);
      this.tabFunc.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion

    #region MyForm STANDARD
    

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public CalcForm()
    {
      // free all garbage
      GC.Collect();
      GC.GetTotalMemory(true);
      InitializeComponent();

      m_Calc = new CCalculator();
      m_Answer = new CNumber();
      m_reg = m_Calc.GetRegReference();
      m_sExpression = "";
      m_alNpExp = new ArrayList();
    }

    /// <summary>
    /// Clean up any resources being used.</summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if (components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }
      
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    private void Form_Load(object sender, System.EventArgs e)
    {
      // display status
      ShowStatusOK(null);
      this.Text = Application.ProductName;

      // in honour of tradition initialy display zero
      m_Answer.FromString("0");
      listHist.Items.Add(m_Answer.ToString() + " = ans");
      listHist.SetSelected(0, true);   // and select it

      // past to expression box clipboard content if its text and contains digits
      if (Clipboard.ContainsText(TextDataFormat.Text))
      {
        string sClipboardText = Clipboard.GetText(TextDataFormat.Text);
        if (sClipboardText.IndexOfAny(new char[]
          { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', }) >= 0)
        {
          txtExp.Text = sClipboardText.Replace(Environment.NewLine, "");
        }
      }

      // init input/output systems
      InputCountSystem.SelectedIndex = (int)T_CS.i_DEC;
      OutputCountSystem.SelectedIndex = InputCountSystem.SelectedIndex;
      InputTrigSystem.SelectedIndex = (int)T_TCS.i_RADIANS;
      OutputPrecision.SelectedIndex = (int)T_PREC.i_DEFAULT;
    }
    #endregion MyForm STANDARD

    //·····························································

    #region CALCULATION LOGIC
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public void CalculateInput(string _sInput)
    {
      // declaring known expression delimeters
      string nl = Environment.NewLine;
      string sDelimeters = nl + ";";
      char[] achDelimiters = sDelimeters.ToCharArray();
      string[] expParts = _sInput.Split(achDelimiters);

      // solve expression by part-part
      foreach (string exp in expParts)
      {
        string subExpr = exp.Replace(" ", "").ToLower();   // simplify
        if (subExpr == "")
        {
          continue;
        }

        try
        {
          // Pass formated answer variable as a template
          // for conversion-managment while parsing expression
          m_Answer.CS = (T_CS)InputCountSystem.SelectedIndex;
          m_Answer.TCS = (T_TCS)InputTrigSystem.SelectedIndex;

          m_Calc.Solve(subExpr, ref m_Answer);// try it

          // set number format for output
          m_Answer.CS = (T_CS)OutputCountSystem.SelectedIndex;
          m_Answer.PREC = (T_PREC)OutputPrecision.SelectedIndex;

          // show result, whatever
          ShowResult(exp);
          // track last item in the result list
          listHist.SetSelected(listHist.Items.Count - 1, true);
        }
          // on syntax error
        catch (Calc.SyntaxException _xcp)
        {
          if (_xcp.Error != "")
          {
            ShowStatusError(_xcp.Error);
            return;
          }
        }
          // catch calculator supported commands
        catch (Calc.CommandException _xcp)
        {
          bool bBreak = RunCalcCommand(_xcp);
          if (bBreak)
          {
            return;
          }
        }
          // on engine failure
        catch (Calc.EngineException _xcp)
        {
          MessageBox.Show(_xcp.Message, "Self-control:"
            , System.Windows.Forms.MessageBoxButtons.OK
            , System.Windows.Forms.MessageBoxIcon.Warning);

          return;
        }
      }// end of foreach

      ShowStatusOK(null);  // reset status
    }

    /// <summary>
    /// Process Calc command;</summary>
    /// <param name="_cmd">Command exception to run;</param>
    /// <returns>true if calculation needs to break;</returns>
    private bool RunCalcCommand(Calc.CommandException _cmd)
    {
      switch (_cmd.Command)
      {
          // Clear screen and status
        case Calc.AppCommand.ClearResultScreen:
          CleanPage_Click(this, null);
          ShowStatusOK(null);// Ready
          break;

          // Exit Application
        case Calc.AppCommand.ExitClientApplication:
          this.Close();
          return true;

          // Display information about the Calc Engine
        case Calc.AppCommand.AboutCalcEngine:
          MessageBox.Show(m_Calc.AboutEngine, "Supported Functionality:"
            , MessageBoxButtons.OK, MessageBoxIcon.Information);
          return true;

          // change input count system
        case Calc.AppCommand.ChangeInputCS:
          InputCountSystem.SelectedIndex = _cmd.Argument;
          break;
          // change result count system
        case Calc.AppCommand.ChangeResultCS:
          OutputCountSystem.SelectedIndex = _cmd.Argument;
          break;

          // do nothing
        case Calc.AppCommand.Standby:
          break;

          // on any unknown command
        default:
          ShowStatusError(
            "Not implemented by app. command \""
            + _cmd.Command.ToString() + "\"");
          return true;
      }

      return false;
    }
    #endregion CALCULATION LOGIC

    #region EXPRESSION TEXT-BOX KEY HOOKERS

    // automaticaly set focus on mouse hover under input control
    private void txtExp_MouseHover(object sender, System.EventArgs e)
    {
      txtExp.Focus();
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // on key down in expression text-box
    private void txtExp_KeyDown(object sender
      , System.Windows.Forms.KeyEventArgs e)
    {
      if (Keys.Escape == e.KeyCode)
      {
        e.Handled = true;
        txtExp.Clear();
      }
      // scroll via history entries down
      if (Keys.PageDown == e.KeyCode
        || Keys.BrowserForward == e.KeyCode)
      {
        e.Handled = true;
        GetDownHistory();
      }
        // scroll via history entries up
      else if (Keys.PageUp == e.KeyCode
        || Keys.BrowserBack == e.KeyCode)
      {
        e.Handled = true;
        GetUpHistory();
      }
        // on [Return] key - show calculation result
        // in history list
      else if (Keys.Return == e.KeyCode)// && e.Control)
      {
        e.Handled = true;
        // clear expression from break-line sequence
        m_sExpression = txtExp.Text;
        if (m_sExpression != "")
        {
          CalculateInput(m_sExpression);
          /**
                * Assume: input clean from break-line (txtExp is multiline).
                * Reassign expression to allow recalculation
                * if [Return] key is STEEL holded down. See code in `txtExp_KeyUP`.
                *
                * NOTE: Recalculation is useful if performed action with
                * changeble registers like answer.
                **/
          txtExp.Clear();
          txtExp.Text = m_sExpression.Replace(Environment.NewLine, "");
        }
      }
      else
      {
#if DEBUG
        Keys key = e.KeyCode;
#endif
      }
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // on key release in expression text-box
    private void txtExp_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      // [Return] key released
      if (e.KeyCode == Keys.Enter)
      {
        e.Handled = true;
        // clear input and reassign previous expression
        // to allow recalculation on NEXT [Return] hitting
        txtExp.Clear();
        txtExp.Text = m_sExpression.Replace(Environment.NewLine, "");
        // select it to allow quick removing if user wish to type
        // something else
        txtExp.SelectAll();
      }
    }
    #endregion EXPRESSION TEXT-BOX KEY HOOKERS

    #region HISTORY
    private void ShowResult(string _res)
    {
      //?//string nl = Environment.NewLine;
      listHist.Items.Add(m_Answer + " = " + _res);
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // scroll via history entries
    private void GetDownHistory()
    {
      if (listHist.SelectedIndex != listHist.Items.Count-1)
      {
        listHist.SetSelected(listHist.SelectedIndex + 1, true);
        // replace text with selected in history expression
        txtExp.Text = listHist.Text.Remove(0, listHist.Text.IndexOf("=") + 2);
        txtExp.SelectAll();
      }
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // scroll via history entries
    private void GetUpHistory()
    {
      if (listHist.SelectedIndex > 0)
      {
        listHist.SetSelected(listHist.SelectedIndex - 1, true);
        // replace text with selected in history expression
        txtExp.Text = listHist.Text.Remove(0, listHist.Text.IndexOf("=") + 2);
        txtExp.SelectAll();
      }
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    private void listHist_DoubleClick(object sender, System.EventArgs e)
    {
      // replace input with selected expresion in history expression
      if (listHist.SelectedIndex >= 0)
      {
        txtExp.Text = listHist.Text.Remove(0, listHist.Text.IndexOf("=") + 2);
        txtExp.SelectAll();
        txtExp.Focus();
      }
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // append result of the hovered expression to expression textbox
    private void listHist_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Middle)
      {
        // retrieve record index under the cursor
        int iHist = listHist.IndexFromPoint(e.X, e.Y);
        if (iHist < 0)
        { // no result under the cursor
          return;
        }

        listHist.SelectedIndex = iHist;

        int iEqv = listHist.Text.IndexOf("=");
        txtExp.Text += listHist.Text.Substring(0, iEqv);
      }
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    private void listHist_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        e.Handled = true;
        listHist_DoubleClick(this, null);
      }
    }
    private void listHist_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        e.Handled = true;
        txtExp.Focus();
      }
    }
    #endregion HISTORY

    #region HISTORY - Context Menu
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    private void CleanPage_Click(object sender, System.EventArgs e)
    {
      listHist.BeginUpdate();
      listHist.Items.Clear();
      listHist.Items.Add(m_reg.ANSWER.ToString() + " = ans");
      listHist.EndUpdate();
      listHist.SelectedIndex = 0;
      txtExp.Focus();
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    private void SaveAs_Click(object sender, System.EventArgs e)
    {
      SaveFileDialog dlg = new SaveFileDialog();
      dlg.ShowDialog(this);

      MessageBox.Show("Not implemnted.", "Save calculation history to file");
      txtExp.Focus();
    }

    private void CopyResult_Click(object sender, System.EventArgs e)
    {
      if (listHist.SelectedIndex < 0)
      {
          return;
      }

      int iEqv = listHist.Text.IndexOf("=") - 1;
      string strExpression = listHist.Text.Substring(0, iEqv);

      Clipboard.SetDataObject(strExpression, true);
      txtExp.Focus();
    }

    private void CopyExpression_Click(object sender, System.EventArgs e)
    {
      if (listHist.SelectedIndex < 0)
      {
        return;
      }

      Clipboard.SetDataObject(listHist.Text, true);
      txtExp.Focus();
    }
    #endregion HISTORY - Context Menu

    #region STATUS BAR
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    enum STAT_ICON: int
    {
      OK = 0,
      ERROR
    };
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    void ShowStatusOK(string _sOkMsg)
    {
      statusBarSyntax.Text = ((_sOkMsg != null)? _sOkMsg : "Ready");

      // change icon
      try
      {
        Icon icc = new Icon(typeof(MasCalc.CalcForm), "syntaxOK.ico");
        statusBarSyntax.Icon = icc;
      }
      catch (System.Exception xcp)
      {
        MessageBox.Show(xcp.Message);
      }

      //StatusIcons.ImageStream
    }
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    void ShowStatusError(string _sErrMsg)
    {
      statusBarSyntax.Text = _sErrMsg;

      // change icon
      try
      {
        Icon icc = new Icon(typeof(MasCalc.CalcForm), "syntaxERR.ico");
        statusBarSyntax.Icon = icc;
      }
      catch (System.Exception xcp)
      {
        MessageBox.Show(xcp.Message);
      }
    }
    #endregion

    #region BOTTONS REACTION
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    private void btnSolve_Click(object sender, System.EventArgs e)
    {
      // clear expression from break-line sequence
      m_sExpression = txtExp.Text;
      if (m_sExpression != "")
      {
        CalculateInput(m_sExpression);
        /**
         * Assume: input clean from break-line (txtExp is multiline).
         * Reassign expression to allow recalculation
         **/
        txtExp.Focus();
        txtExp.Clear();
        txtExp.Text = m_sExpression;
        txtExp.SelectAll();
      }
    }
    #endregion BOTTONS REACTION

    #region COUNT-SYSTEM MODIFIERS
    /// <summary>
    /// Array of colors for colored style of different count systems.
    /// Indexes from <code>T_CS.i_***</code>, access from <code>the(T_CS)</code>
    /// </summary>
    System.Drawing.Color[] m_csColor = {
      /*HEX*/Color.LightGreen, /*DEC*/Color.White, /*OCT*/Color.Wheat, /*BIN*/Color.LightSteelBlue
    };

    /// <summary>
    /// Get color style for selected Count System
    /// </summary>
    /// <param name="_c">count system</param>
    /// <returns>System.Drawing.Color</returns>
    private System.Drawing.Color the(T_CS _c)
    {
      return m_csColor[(int)_c % m_csColor.GetLength(0)];
    }
    /// <summary>
    /// Reflect selected Count System with colour</summary>
    /// <param name="_cb">Input or Output CS combo-box</param>
    /// <param name="_ctrl">Secondary control window</param>
    private void ReflectCSWithColor(ComboBox _cb, Control _ctrl)
    {
      Color color = the((Calc.T_CS)_cb.SelectedIndex);
      _cb.BackColor = color;
      _ctrl.BackColor = color;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// On change of Input Count System - change Output Count System
    /// automaticaly</summary>
    private void InputCountSystem_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      ReflectCSWithColor(InputCountSystem, txtExp);
      
      // these controls make sence only in decimal count-system
      bool bEnabled = ((OutputCountSystem.SelectedIndex == (int)T_CS.i_DEC)
        && (InputCountSystem.SelectedIndex == (int)T_CS.i_DEC));
      InputTrigSystem.Enabled = bEnabled;
      OutputPrecision.Enabled = bEnabled;
    }

    private void OutputCountSystem_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      ReflectCSWithColor(OutputCountSystem, listHist);
      
      // these controls make sence only in decimal count-system
      bool bEnabled = ((OutputCountSystem.SelectedIndex == (int)T_CS.i_DEC)
        && (InputCountSystem.SelectedIndex == (int)T_CS.i_DEC));
      InputTrigSystem.Enabled = bEnabled;
      OutputPrecision.Enabled = bEnabled;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    private void FocusOnMe(object _sender, System.EventArgs e)
    {
      (_sender as Control).Focus();
    }
    private void FocusOnInput(object _sender, System.EventArgs e)
    {
      txtExp.Focus();
    }
    #endregion COUNT-SYSTEM MODIFIERS

    #region NUM_PAD_BUTTONS_CLICKS
    private void NumPad_BTN_Click(object objSender, System.EventArgs e)
    {
      Button btn = (Button)objSender;

      // special reaction
      if (npDel == btn) // set previous expresion
      {
        if (m_alNpExp.Count != 0)
        {
          txtExp.Text = (string)m_alNpExp[0];
          m_alNpExp.RemoveAt(0);
        }

        txtExp.Focus();
        return;
      }
      else if (npAC == btn)
      {
        m_reg.ANSWER = new CNumber("0");
        CleanPage_Click(this, null);
        txtExp.Focus();
        txtExp.Clear();
        ShowStatusOK("AC");

        return;
      }
      else if (npSolve == btn)
      {
        txtExp.Text = CloseBraces(txtExp.Text);
        btnSolve_Click(this, null);
        txtExp.Clear();
        m_alNpExp.Clear();

        return;
      }

      // regular button: simple expression trancation
      // store current expression before changing
      m_alNpExp.Insert(0, txtExp.Text);

      if (npAns == btn) { txtExp.Text += "ans"; }
      else if (np1 == btn) { txtExp.Text += "1"; }
      else if (np2 == btn) { txtExp.Text += "2"; }
      else if (np3 == btn) { txtExp.Text += "3"; }
      else if (np4 == btn) { txtExp.Text += "4"; }
      else if (np5 == btn) { txtExp.Text += "5"; }
      else if (np6 == btn) { txtExp.Text += "6"; }
      else if (np7 == btn) { txtExp.Text += "7"; }
      else if (np8 == btn) { txtExp.Text += "8"; }
      else if (np9 == btn) { txtExp.Text += "9"; }
      else if (np0 == btn) { txtExp.Text += "0"; }
      else if (np00 == btn) { txtExp.Text += "00"; }
      else if (npDot == btn) { txtExp.Text += "."; }
      else if (npBrBeg == btn) { txtExp.Text += "("; }
      else if (npBrEnd == btn) { txtExp.Text += ")"; }
      else if (npPls == btn) { txtExp.Text += "+"; }
      else if (npMns == btn) { txtExp.Text += "-"; }
      else if (npMul == btn) { txtExp.Text += "*"; }
      else if (npDiv == btn) { txtExp.Text += "/"; }
      else if (npDgr2 == btn) { txtExp.Text += "^(2)"; }
      else if (npDgrY == btn) { txtExp.Text += "^"; }
      else if (npRt2 == btn) { txtExp.Text += "(2)#"; }
      else if (npRtY == btn) { txtExp.Text += "#"; }
      else if (npFct == btn) { txtExp.Text += "!("; }
      else if (npLog == btn) { txtExp.Text += "Log("; }
      else if (npLn == btn) { txtExp.Text += "Ln("; }
      else if (npLg == btn) { txtExp.Text += "Lg("; }
      else if (npExp == btn) { txtExp.Text += "Exp("; }
      else if (npCos == btn) { txtExp.Text += "Cos("; }
      else if (npSin == btn) { txtExp.Text += "Sin("; }
      else if (npTan == btn) { txtExp.Text += "Tan("; }
      else if (npAbs == btn) { txtExp.Text += "Abs("; }
      else if (npMod == btn) { txtExp.Text += "\\"; }
      else if (npNot == btn) { txtExp.Text += "Not("; }
      else if (npAnd == btn) { txtExp.Text += "And"; }
      else if (npOr == btn) { txtExp.Text += "Or"; }
      else if (npXor == btn) { txtExp.Text += "Xor"; }
      else if (npSftL == btn) { txtExp.Text += "<<"; }
      else if (npSftR == btn) { txtExp.Text += ">>"; }
      else if (npGcd == btn) { txtExp.Text += "Gcd"; }
      else
      {
        MessageBox.Show("Not implemented NumPad Button reaction.");
      }

      txtExp.Focus();
    }

    /// <summary>
    /// Detect how much open braces in `_str` and complate it with needed
    /// ammount of closing braces at the end.
    /// If there are lack for open braces - method return the same string
    /// that passed to him.</summary>
    /// <param name="_str">string to parse</param>
    /// <returns>result string</returns>
    private string CloseBraces(string _str)
    {
      char[] ach = _str.ToCharArray();
      int brace = 0;

      // count not closed braces (therefore opened.
      for (int i = 0; i < ach.Length; i++)
      {
        if (ach[i] == '(')
        {
          ++brace;
        }
        else if (ach[i] == ')')
        {
          --brace;
        }
      }

      if (brace < 0)
      {// on lack of open braces..
        return _str;
      }
      else if (brace > 0)
      {// append needed ammount of closing braces
        while ((brace--) > 0)
        {
          _str += ')';
        }
      }
      return _str;
    }
    #endregion NUM_PAD_BUTTONS_CLICKS

    #region APP COMMANDS
    #endregion APP COMMANDS

  };
}

//? EOF
