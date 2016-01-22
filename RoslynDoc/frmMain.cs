using System;
using System.Windows.Forms;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Base;
using Bau.Libraries.LibRoslynDocument;
using Bau.Libraries.LibRoslynDocument.Models.Documents;
using Bau.Libraries.LibRoslynManager;
using Bau.Libraries.LibRoslynManager.Parser;

namespace RoslynDoc
{
	/// <summary>
	///		Formulario principal
	/// </summary>
	public partial class frmMain : Form
	{ 
		public frmMain()
		{	InitializeComponent();
		}

		/// <summary>
		///		Inicializa los controles del formulario
		/// </summary>
		private void InitForm()
		{ DocumentationParameters objParameter = new DocumentationParameters();

				// Inicializa el combo de tipos
					cboDocumentType.Items.Clear();
					cboDocumentType.AddItem((int) DocumentationParameters.DocumentationType.Html, "Html");
					cboDocumentType.AddItem((int) DocumentationParameters.DocumentationType.Xml, "Xml");
					cboDocumentType.AddItem((int) DocumentationParameters.DocumentationType.Nhtml, "Nhtml");
					cboDocumentType.SelectedID = (int) objParameter.IDType;
				// Inicializa el combo de modos
					cboMode.Items.Clear();
					cboMode.AddItem((int) DocumentationParameters.DocumentationMode.SimpleStructs, "Simple");
					cboMode.AddItem((int) DocumentationParameters.DocumentationMode.ComplexStructs, "Compleja");
					cboMode.SelectedID = (int) objParameter.Mode;
				// Inicializa los checkbox
					chkShowPublic.Checked = objParameter.ShowPublic;
					chkShowInternal.Checked = objParameter.ShowInternal;
					chkShowProtected.Checked = objParameter.ShowProtected;
					chkShowPrivate.Checked = objParameter.ShowPrivate;
				// Directorio destino de la documentación
					pthTarget.PathName = System.IO.Path.Combine(Application.StartupPath, "Documents");
		}

		/// <summary>
		///		Comprueba los datos introducidos
		/// </summary>
		private bool ValidateTestFile()
		{ bool blnValidate = false;

				// Comprueba los datos
					if (fnFile.FileName.IsEmpty() || !System.IO.File.Exists(fnFile.FileName))
						Bau.Controls.Forms.Helper.ShowMessage(this, "Seleccione un archivo");
					else
						blnValidate = true;
				// Devuelve el valor que indica si los datos son correctos
					return blnValidate;
		}

		/// <summary>
		///		Compureba los datos introducidos de la solución
		/// </summary>
		private bool ValidateSolution()
		{ bool blnValidate = false;

				// Comprueba los datos
					if (fnSolution.FileName.IsEmpty() || !System.IO.File.Exists(fnSolution.FileName))
						Bau.Controls.Forms.Helper.ShowMessage(this, "Seleccione un archivo de solución");
					else if (pthTarget.PathName.IsEmpty())
						Bau.Controls.Forms.Helper.ShowMessage(this, "Seleccione el directorio donde se genera la documentación");
					else
						blnValidate = true;
				// Devuelve el valor que indica si los datos son correctos
					return blnValidate;
		}

		/// <summary>
		///		Carga una solución
		/// </summary>
		private void LoadSolution()
		{ if (ValidateSolution())
				{ ProgramModel objProgram = ParseSolution(fnSolution.FileName);

						// Muestra la documentación
							foreach (CompilationUnitModel objCompilationUnit in objProgram.CompilationUnits)
								{	// Muestra el nombre del archivo de compilación
										txtLog.AppendText("Unidad: " + objCompilationUnit.FileName + Environment.NewLine);
									// Muestra la documentación
										WriteDocument(objCompilationUnit.Root, 0);
									// Separa las líneas
										txtLog.AppendText(Environment.NewLine + new string('-', 30) + Environment.NewLine);
								}
				}
		}

		/// <summary>
		///		Escribe el documento
		/// </summary>
		private void WriteDocument(LanguageStructModel objRoot, int intIndent)
		{ // Escribe la documentación
				txtLog.AppendText(objRoot.Debug(intIndent));
			// Escribe los hijos
				foreach (LanguageStructModel objStruct in objRoot.Items)
					WriteDocument(objStruct, intIndent + 1);
		}

		/// <summary>
		///		Gemera la documentación
		/// </summary>
		private void GenerateDocuments()
		{ if (ValidateSolution())
				{ DocumentFileModelCollection objColDocuments;

						// Borra el directorio destino
							Bau.Libraries.LibHelper.Files.HelperFiles.KillPath(pthTarget.PathName);
						// Genera la documentación
							objColDocuments = new DocumentationGenerator(GetParametersDocuments(), pthTarget.PathName).Process(fnSolution.FileName);
						// Muestra la documentación
							txtLog.Text = objColDocuments.Debug(0);
						// Muestra los nodos
							trvNodes.Nodes.Clear();
							ShowDocumentNodes(objColDocuments, null);
							trvNodes.ExpandAll();
				}
		}

		/// <summary>
		///		Obtiene los parámetros de generación del documento
		/// </summary>
		private DocumentationParameters GetParametersDocuments()
		{ DocumentationParameters objParameters = new DocumentationParameters();

				// Asigna los valores del formulario
					objParameters.IDType = (DocumentationParameters.DocumentationType) (cboDocumentType.SelectedID ?? 0);
					objParameters.Mode = (DocumentationParameters.DocumentationMode) (cboMode.SelectedID ?? 0);
				// Inicializa los checkbox
					objParameters.ShowPublic = chkShowPublic.Checked;
					objParameters.ShowInternal = chkShowInternal.Checked;
					objParameters.ShowProtected = chkShowProtected.Checked;
					objParameters.ShowPrivate = chkShowPrivate.Checked;
				// Devuelve los parámetros
					return objParameters;
		}

		/// <summary>
		///		Muestra los documentos en el árbol
		/// </summary>
		private void ShowDocumentNodes(DocumentFileModelCollection objColDocuments, TreeNode trnParent)
		{ foreach (DocumentFileModel objDocument in objColDocuments)
				{ TreeNode trnNode = trvNodes.AddNode(trnParent, 0, 0, objDocument.Name, false);

						// Añade los documentos hijo
							ShowDocumentNodes(objDocument.Childs, trnNode);
				}
		}

		/// <summary>
		///		Interpreta una solución
		/// </summary>
		private ProgramModel ParseSolution(string strFileName)
		{ ProgramParser objGenerator = new ProgramParser();

				// Interpreta la solución
					return objGenerator.ParseSolution(strFileName);
		}

		/// <summary>
		///		Genera un modelo de pruebas
		/// </summary>
		private void CompileTestModel()
		{ if (ValidateTestFile())
				{ string strText = Bau.Libraries.LibHelper.Files.HelperFiles.LoadTextFile(fnFile.FileName);

						if (!strText.IsEmpty())
							{ ProgramParser objGenerator = new ProgramParser();
								ProgramModel objProgram;

									// Genera la documentación
										objProgram = objGenerator.ParseText(strText);
									// Muestra la documentación
										txtLog.Text = "";
										foreach (CompilationUnitModel objCompilationUnit in objProgram.CompilationUnits)
											WriteDocument(objCompilationUnit.Root, 0);
									// Muestra los nodos
										trvNodes.Nodes.Clear();
										foreach (CompilationUnitModel objCompilationUnit in objProgram.CompilationUnits)
											{ TreeNode trnNode = trvNodes.AddNode(null, 0, 0, "CompilationUnit: " + objCompilationUnit.FileName, false);

													ShowCompilationNodes(objCompilationUnit.Root.Items, trnNode);
											}
									// Expande los nodos
										trvNodes.ExpandAll();
							}
				}
		}

		/// <summary>
		///		Muestra los nodos de compilación
		/// </summary>
		private void ShowCompilationNodes(LanguageStructModelCollection objColItems, TreeNode trnParent)
		{ foreach (LanguageStructModel objItem in objColItems)
				{ TreeNode trnNode = trvNodes.AddNode(trnParent, 0, 0, objItem.IDType.ToString() + " - " + objItem.Name, false);

						// Carga los hijos
							ShowCompilationNodes(objItem.Items, trnNode);
				}
		}

		/// <summary>
		///		Muestra el árbol sintáctico directamente desde la salida de Roslyn
		/// </summary>
		private void ShowSintaxTree()
		{ if (ValidateTestFile())
				{ string strText = Bau.Libraries.LibHelper.Files.HelperFiles.LoadTextFile(fnFile.FileName);

						if (!strText.IsEmpty())
							{ SyntaxTreeDebug objGenerator = new SyntaxTreeDebug();

									// Genera la documentación
										objGenerator.GetSyntaxTree(strText);
									// Muestra la documentación
										txtLog.Text = objGenerator.ParsedText.ToString();
									// Muestra el árbol
										trvNodes.Nodes.Clear();
										ShowSintaxTree(objGenerator.Tree.GetRoot(), null);
									// Expande los nodos
										trvNodes.ExpandAll();
							}
				}
		}

		/// <summary>
		///		Muestra el árbol sintáctico
		/// </summary>
		private void ShowSintaxTree(SyntaxNodeOrToken objSyntaxNode, TreeNode trnParent)
		{ if (objSyntaxNode != null)
				{ TreeNode trnNode = trvNodes.AddNode(trnParent, 0, 0, objSyntaxNode.Kind().ToString(), false, 0, GetColor(objSyntaxNode));

						// Añade los nodos hijo
							foreach (SyntaxNodeOrToken objChild in objSyntaxNode.ChildNodesAndTokens())
								ShowSintaxTree(objChild, trnNode);
				}
		}

		/// <summary>
		///		Obtiene el color asociado al nodo 
		/// </summary>
		private System.Drawing.Color GetColor(SyntaxNodeOrToken objSyntaxNode)
		{ if (objSyntaxNode.IsNode)
				return System.Drawing.Color.Blue;
			else
				return System.Drawing.Color.ForestGreen;
		}

		private void cmdCompileTestModel_Click(Object sender, EventArgs e)
		{ CompileTestModel();
		}

		private void cmdShowSintaxTree_Click(Object sender, EventArgs e)
		{ ShowSintaxTree();
		}

		private void cmdLoadSolution_Click(Object sender, EventArgs e)
		{ LoadSolution();
		}

		private void cmdGenerateDocuments_Click(Object sender, EventArgs e)
		{ GenerateDocuments();
		}

		private void frmMain_Load(Object sender, EventArgs e)
		{ InitForm();
		}
	}
}
