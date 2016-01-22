namespace RoslynDoc
{
	partial class frmMain
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
			this.fnFile = new Bau.Controls.Files.TextBoxSelectFile();
			this.cmdShowSintaxTree = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdCompileTestModel = new System.Windows.Forms.Button();
			this.fnSolution = new Bau.Controls.Files.TextBoxSelectFile();
			this.label2 = new System.Windows.Forms.Label();
			this.cmdGenerateDocuments = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.pthTarget = new Bau.Controls.Files.TextBoxSelectPath();
			this.label4 = new System.Windows.Forms.Label();
			this.cboDocumentType = new Bau.Controls.Combos.ComboBoxExtended();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.chkShowPrivate = new System.Windows.Forms.CheckBox();
			this.chkShowProtected = new System.Windows.Forms.CheckBox();
			this.chkShowInternal = new System.Windows.Forms.CheckBox();
			this.chkShowPublic = new System.Windows.Forms.CheckBox();
			this.cboMode = new Bau.Controls.Combos.ComboBoxExtended();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.trvNodes = new Bau.Controls.Tree.TreeViewExtended();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// fnFile
			// 
			this.fnFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fnFile.BackColorEdit = System.Drawing.SystemColors.Window;
			this.fnFile.FileName = "C:\\Users\\jbautistam\\Proyectos\\Development\\Net\\Testing\\RoslynDoc\\RoslynDoc\\TestIte" +
    "m.cs";
			this.fnFile.Filter = "Archivos C# (*.cs)|*.cs";
			this.fnFile.Location = new System.Drawing.Point(62, 14);
			this.fnFile.Margin = new System.Windows.Forms.Padding(0);
			this.fnFile.MaximumSize = new System.Drawing.Size(10000, 20);
			this.fnFile.MinimumSize = new System.Drawing.Size(100, 20);
			this.fnFile.Name = "fnFile";
			this.fnFile.Size = new System.Drawing.Size(598, 20);
			this.fnFile.TabIndex = 1;
			this.fnFile.Type = Bau.Controls.Files.TextBoxSelectFile.FileSelectType.Load;
			// 
			// cmdShowSintaxTree
			// 
			this.cmdShowSintaxTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdShowSintaxTree.Location = new System.Drawing.Point(303, 49);
			this.cmdShowSintaxTree.Name = "cmdShowSintaxTree";
			this.cmdShowSintaxTree.Size = new System.Drawing.Size(132, 23);
			this.cmdShowSintaxTree.TabIndex = 2;
			this.cmdShowSintaxTree.Text = "Mostrar arbol sintáctico";
			this.cmdShowSintaxTree.UseVisualStyleBackColor = true;
			this.cmdShowSintaxTree.Click += new System.EventHandler(this.cmdShowSintaxTree_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.label1.Location = new System.Drawing.Point(13, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Archivo:";
			// 
			// cmdCompileTestModel
			// 
			this.cmdCompileTestModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCompileTestModel.Location = new System.Drawing.Point(441, 49);
			this.cmdCompileTestModel.Name = "cmdCompileTestModel";
			this.cmdCompileTestModel.Size = new System.Drawing.Size(219, 23);
			this.cmdCompileTestModel.TabIndex = 3;
			this.cmdCompileTestModel.Text = "Generación de modelo de pruebas";
			this.cmdCompileTestModel.UseVisualStyleBackColor = true;
			this.cmdCompileTestModel.Click += new System.EventHandler(this.cmdCompileTestModel_Click);
			// 
			// fnSolution
			// 
			this.fnSolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fnSolution.BackColorEdit = System.Drawing.SystemColors.Window;
			this.fnSolution.FileName = "C:\\Users\\jbautistam\\Proyectos\\Development\\Net\\Testing\\RoslynDoc\\RoslynDoc.sln";
			this.fnSolution.Filter = "Archivos de solución (*.sln)|*.sln|Archivos de proyecto (*.csproj)|*.csproj";
			this.fnSolution.Location = new System.Drawing.Point(65, 13);
			this.fnSolution.Margin = new System.Windows.Forms.Padding(0);
			this.fnSolution.MaximumSize = new System.Drawing.Size(10000, 20);
			this.fnSolution.MinimumSize = new System.Drawing.Size(100, 20);
			this.fnSolution.Name = "fnSolution";
			this.fnSolution.Size = new System.Drawing.Size(593, 20);
			this.fnSolution.TabIndex = 1;
			this.fnSolution.Type = Bau.Controls.Files.TextBoxSelectFile.FileSelectType.Load;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.label2.Location = new System.Drawing.Point(11, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Solución:";
			// 
			// cmdGenerateDocuments
			// 
			this.cmdGenerateDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGenerateDocuments.Location = new System.Drawing.Point(526, 147);
			this.cmdGenerateDocuments.Name = "cmdGenerateDocuments";
			this.cmdGenerateDocuments.Size = new System.Drawing.Size(132, 23);
			this.cmdGenerateDocuments.TabIndex = 13;
			this.cmdGenerateDocuments.Text = "Generar documentación";
			this.cmdGenerateDocuments.UseVisualStyleBackColor = true;
			this.cmdGenerateDocuments.Click += new System.EventHandler(this.cmdGenerateDocuments_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.label3.Location = new System.Drawing.Point(11, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Salida:";
			// 
			// pthTarget
			// 
			this.pthTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pthTarget.Location = new System.Drawing.Point(65, 41);
			this.pthTarget.Margin = new System.Windows.Forms.Padding(0);
			this.pthTarget.MaximumSize = new System.Drawing.Size(10000, 20);
			this.pthTarget.MinimumSize = new System.Drawing.Size(200, 20);
			this.pthTarget.Name = "pthTarget";
			this.pthTarget.PathName = "C:\\Users\\jbautistam\\Proyectos\\Development\\Net\\Testing\\RoslynDoc\\RoslynDoc\\Data";
			this.pthTarget.Size = new System.Drawing.Size(593, 20);
			this.pthTarget.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.label4.Location = new System.Drawing.Point(11, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Tipo:";
			// 
			// cboDocumentType
			// 
			this.cboDocumentType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDocumentType.FormattingEnabled = true;
			this.cboDocumentType.Location = new System.Drawing.Point(65, 70);
			this.cboDocumentType.Name = "cboDocumentType";
			this.cboDocumentType.SelectedID = null;
			this.cboDocumentType.Size = new System.Drawing.Size(261, 21);
			this.cboDocumentType.TabIndex = 5;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new System.Drawing.Point(3, 7);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(686, 209);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.chkShowPrivate);
			this.tabPage2.Controls.Add(this.chkShowProtected);
			this.tabPage2.Controls.Add(this.chkShowInternal);
			this.tabPage2.Controls.Add(this.chkShowPublic);
			this.tabPage2.Controls.Add(this.pthTarget);
			this.tabPage2.Controls.Add(this.cboMode);
			this.tabPage2.Controls.Add(this.cboDocumentType);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.cmdGenerateDocuments);
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.fnSolution);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(678, 183);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Generación de documentación";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// chkShowPrivate
			// 
			this.chkShowPrivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShowPrivate.AutoSize = true;
			this.chkShowPrivate.Location = new System.Drawing.Point(501, 95);
			this.chkShowPrivate.Name = "chkShowPrivate";
			this.chkShowPrivate.Size = new System.Drawing.Size(156, 17);
			this.chkShowPrivate.TabIndex = 11;
			this.chkShowPrivate.Text = "Documentar datos privados";
			this.chkShowPrivate.UseVisualStyleBackColor = true;
			// 
			// chkShowProtected
			// 
			this.chkShowProtected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShowProtected.AutoSize = true;
			this.chkShowProtected.Location = new System.Drawing.Point(332, 95);
			this.chkShowProtected.Name = "chkShowProtected";
			this.chkShowProtected.Size = new System.Drawing.Size(165, 17);
			this.chkShowProtected.TabIndex = 10;
			this.chkShowProtected.Text = "Documentar datos protegidos";
			this.chkShowProtected.UseVisualStyleBackColor = true;
			// 
			// chkShowInternal
			// 
			this.chkShowInternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShowInternal.AutoSize = true;
			this.chkShowInternal.Location = new System.Drawing.Point(501, 72);
			this.chkShowInternal.Name = "chkShowInternal";
			this.chkShowInternal.Size = new System.Drawing.Size(153, 17);
			this.chkShowInternal.TabIndex = 9;
			this.chkShowInternal.Text = "Documentar datos internos";
			this.chkShowInternal.UseVisualStyleBackColor = true;
			// 
			// chkShowPublic
			// 
			this.chkShowPublic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShowPublic.AutoSize = true;
			this.chkShowPublic.Location = new System.Drawing.Point(332, 72);
			this.chkShowPublic.Name = "chkShowPublic";
			this.chkShowPublic.Size = new System.Drawing.Size(155, 17);
			this.chkShowPublic.TabIndex = 8;
			this.chkShowPublic.Text = "Documentar datos públicos";
			this.chkShowPublic.UseVisualStyleBackColor = true;
			// 
			// cboMode
			// 
			this.cboMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMode.FormattingEnabled = true;
			this.cboMode.Location = new System.Drawing.Point(65, 97);
			this.cboMode.Name = "cboMode";
			this.cboMode.SelectedID = null;
			this.cboMode.Size = new System.Drawing.Size(261, 21);
			this.cboMode.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.label6.Location = new System.Drawing.Point(11, 101);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Modo:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.Maroon;
			this.label5.Location = new System.Drawing.Point(13, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(426, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Al generar la documentación se elimina el contenido del directorio destino";
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.cmdCompileTestModel);
			this.tabPage1.Controls.Add(this.cmdShowSintaxTree);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.fnFile);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(678, 183);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Pruebas";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabControl2
			// 
			this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl2.Controls.Add(this.tabPage4);
			this.tabControl2.Controls.Add(this.tabPage3);
			this.tabControl2.Location = new System.Drawing.Point(7, 222);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(682, 443);
			this.tabControl2.TabIndex = 1;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.trvNodes);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(674, 417);
			this.tabPage4.TabIndex = 1;
			this.tabPage4.Text = "Nodos";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// trvNodes
			// 
			this.trvNodes.CheckRecursive = false;
			this.trvNodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trvNodes.Location = new System.Drawing.Point(3, 3);
			this.trvNodes.Name = "trvNodes";
			this.trvNodes.ShowNodeToolTips = true;
			this.trvNodes.Size = new System.Drawing.Size(668, 411);
			this.trvNodes.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.txtLog);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(674, 417);
			this.tabPage3.TabIndex = 0;
			this.tabPage3.Text = "Log";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// txtLog
			// 
			this.txtLog.AcceptsReturn = true;
			this.txtLog.AcceptsTab = true;
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.Location = new System.Drawing.Point(3, 3);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(668, 411);
			this.txtLog.TabIndex = 0;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(698, 669);
			this.Controls.Add(this.tabControl2);
			this.Controls.Add(this.tabControl1);
			this.Name = "frmMain";
			this.Text = "Generador de documentación con Roslyn";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabControl2.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Bau.Controls.Files.TextBoxSelectFile fnFile;
		private System.Windows.Forms.Button cmdShowSintaxTree;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdCompileTestModel;
		private Bau.Controls.Files.TextBoxSelectFile fnSolution;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdGenerateDocuments;
		private System.Windows.Forms.Label label3;
		private Bau.Controls.Files.TextBoxSelectPath pthTarget;
		private System.Windows.Forms.Label label4;
		private Bau.Controls.Combos.ComboBoxExtended cboDocumentType;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TextBox txtLog;
		private Bau.Controls.Tree.TreeViewExtended trvNodes;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox chkShowPrivate;
		private System.Windows.Forms.CheckBox chkShowProtected;
		private System.Windows.Forms.CheckBox chkShowInternal;
		private System.Windows.Forms.CheckBox chkShowPublic;
		private Bau.Controls.Combos.ComboBoxExtended cboMode;
		private System.Windows.Forms.Label label6;
	}
}

