using System;
using System.Text;
using System.Text.RegularExpressions;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibRoslynDocument.Models.Documents;

namespace Bau.Libraries.LibRoslynDocument.Processor.Writers.Html
{
	/// <summary>
	///		Generador de un archivo HTML para documentación
	/// </summary>
	public class HtmlWriter : IDocumentWriter
	{ // Variables privadas
			private HtmlConversor objConversor = new HtmlConversor();

		/// <summary>
		///		Graba la documentación Html en un archivo
		/// </summary>
		public void Save(DocumentFileModel objDocument, MLIntermedialBuilder objMLBuilder, 
										 string strRootFileNameTemplate, string strPath)
		{ Save(objDocument.Name, ConvertMLBulider(objDocument, strPath, objDocument.GetPathLocal(), objDocument.Name, objDocument.Name, 
																								 objMLBuilder, strRootFileNameTemplate),
					 System.IO.Path.Combine(strPath, objDocument.GetPathLocal()));
		}

		/// <summary>
		///		Graba la documentación Html a partir de los datos pasados como parámetros
		/// </summary>
		public void Save(string strTitle, string strDescription, MLIntermedialBuilder objMLBuilder, 
										 string strRootFileNameTemplate, string strPath)
		{	Save(strTitle, ConvertMLBulider(null, strPath, "", strTitle, strDescription, 
																				 objMLBuilder, strRootFileNameTemplate), strPath);
		}

		/// <summary>
		///		Convierte una estructura XML en HTML
		/// </summary>
		private string ConvertMLBulider(DocumentFileModel objDocument, string strOutputPath, string strRootPath, 
																		string strTitle, string strDescription, 
																		MLIntermedialBuilder objMLBuilder, string strFileNameTemplate)
		{ string strResult = new Repository.Templates.TemplateRepository().LoadTextRootTemplate(strFileNameTemplate);
			string strHtml = objConversor.Convert(strRootPath, objMLBuilder);

				// Reemplaza los datos de la plantilla de entrada
					if (strResult.IsEmpty())
						strResult = strHtml;
					else
						{ // Asigna el título y la descripción
								strResult = strResult.ReplaceWithStringComparison("{{Title}}", strTitle);
								strResult = strResult.ReplaceWithStringComparison("{{Description}}", strDescription);
							// Crea el vínculo a la página superior
								if (objDocument != null && objDocument.Parent != null)
									strResult = strResult.ReplaceWithStringComparison("{{TopPage}}", string.Format("<a href='{0}'>{1}</a>",
																																																 GetUrl(objDocument.Parent, ""),
																																																 objDocument.Parent.Name));
								else
									strResult = strResult.ReplaceWithStringComparison("{{TopPage}}", "");
							// Cambia los vínculos
								strResult = UpdateLinks(System.IO.Path.Combine(strRootPath, "filler.htm"), strResult);
							// Asigna el cuerpo
								strResult = strResult.ReplaceWithStringComparison("{{Body}}", strHtml);
						}
				// Devuelve el texto
					return strResult;
		}

		/// <summary>
		///		Obtiene la Url a un documento
		/// </summary>
		private string GetUrl(DocumentFileModel objDocument, string strRootPath)
		{ string strUrl = objDocument.GetUrl(strRootPath);

				// Añade el nombre de página
					strUrl = System.IO.Path.Combine(strUrl, objConversor.GetFileName(objDocument.Name));
				// Devuelve la URL cambiando los separadores
					return strUrl.Replace('\\', '/');
		}

		/// <summary>
		///		Modifica los vínculos
		/// </summary>
		private string UpdateLinks(string strUrlActualDocument, string strContent)
		{	StringBuilder sbBuilder = new StringBuilder();
			Match objMatch = Regex.Match(strContent, @"\s*(href|src)\s*=\s*(?:[""'](?<1>[^""']*)[""']|(?<1>\S+))");
			int intLastPosition = 0;

				// Mientras se encuentre una cadena
					while (objMatch.Success) 
						{	// Añade a la cadena de salida lo anterior
								sbBuilder.Append(strContent.Substring(intLastPosition, objMatch.Index - intLastPosition));
							// Añade el contenido
								sbBuilder.Append(ReplaceInner(strUrlActualDocument, objMatch.Value));
							// Guarda la posición actual
								intLastPosition = objMatch.Index + objMatch.Length;
							// Obtiene la siguiente coincidencia
								objMatch = objMatch.NextMatch();
						}
				// Añade el resto de la cadena
					if (intLastPosition < strContent.Length)
						sbBuilder.Append(strContent.Substring(intLastPosition));
				// Devuelve el contenido
					return sbBuilder.ToString();
		}

		/// <summary>
		///		Reemplaza la cadena interna
		/// </summary>
		private string ReplaceInner(string strUrlActualDocument, string strContent)
		{ Match objMatch = Regex.Match(strContent, @"[""'](?<1>[^""']*)[""']");
			string strResult = null;

				// Cambia la cadena entre comillas
					if (objMatch.Success)
						{ string strUrlTarget = objMatch.Value.Trim().Substring(1, objMatch.Value.Trim().Length - 2);

								// Añade el contenido anterior a la Url
									strResult = strContent.Substring(0, objMatch.Index);
								// Transforma la URL
									if (strUrlTarget.StartsWith("http:", StringComparison.CurrentCultureIgnoreCase) ||
											strUrlTarget.StartsWith("https:", StringComparison.CurrentCultureIgnoreCase))
										strResult += "\"" + strUrlTarget + "\"";
									else
										strResult += "\"" + LibHelper.Files.HelperFileUri.GetUrlRelative(strUrlActualDocument, strUrlTarget) + "\"";
								// Añade el contenido siguiente a la Url
									strResult += strContent.Substring(objMatch.Index + objMatch.Length);
							}
				// Devuelve el resultado
					if (!strResult.IsEmpty())
						return strResult;
					else
						return strContent;
		}

		/// <summary>
		///		Graba un archivo HTML
		/// </summary>
		private void Save(string strName, string strText, string strPath)
		{	string strFileName = System.IO.Path.Combine(strPath, objConversor.GetFileName(strName));

				// Crea el directorio
					LibHelper.Files.HelperFiles.MakePath(strPath);
				// Graba el archivo de documentación
					LibHelper.Files.HelperFiles.SaveTextFile(strFileName, strText);
		}
	}
}
