using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator.Parser;
using Spring2.DataTierGenerator.Element;

namespace Spring2.DataTierGenerator {
    public class Configuration : ConfigurationData {

	public Configuration () {
	}

	public Configuration(XmlNode root, ParserValidationDelegate vd) {
	    if (root.HasChildNodes) {
		for (Int32 i=0; i<root.ChildNodes.Count; i++) {
		    XmlNode node = root.ChildNodes[i];
		    if (node.Name.Equals("setting")) {
			switch(node.Attributes["name"].Value.ToLower()) {
			    case "rootnamespace":
				this.rootNameSpace = node.Attributes["value"].Value;
				break;
			    case "rootdirectory":
				this.rootDirectory = node.Attributes["value"].Value;
				break;
			    case "typesclassdirectory":
				this.typesClassDirectory = node.Attributes["value"].Value;
				break;
			    case "daoclassdirectory":
				this.daoClassDirectory = node.Attributes["value"].Value;
				break;
			    case "reportextractiondaoclassdirectory":
				this.reportExtractionDaoClassDirectory = node.Attributes["value"].Value;
				break;
			    case "doclassdirectory":
				this.doClassDirectory = node.Attributes["value"].Value;
				break;
			    case "collectionclassdirectory":
				this.collectionClassDirectory = node.Attributes["value"].Value;
				break;
			    case "generatedataobjectclasses":
				this.generateDataObjectClasses = Boolean.Parse(node.Attributes["value"].Value);
				break;
			    case "generatereportextractiondaoclasses":
				this.generateReportExtractionDaoClasses = Boolean.Parse(node.Attributes["value"].Value);
				break;
			    case "dataobjectbaseclass":
				this.dataObjectBaseClass= node.Attributes["value"].Value;
				break;
			    case "daobaseclass":
				this.daoBaseClass= node.Attributes["value"].Value;
				break;
			    case "reportextractiondaobaseclass":
				this.reportExtractionDaoBaseClass= node.Attributes["value"].Value;
				break;
			    case "enumbaseclass":
				this.enumBaseClass= node.Attributes["value"].Value;
				break;
			    case "testclassdirectory":
				this.testClassDirectory = node.Attributes["value"].Value;
				break;
			    default:
				vd(ParserValidationArgs.NewWarning("Unrecognized configuration option: " + node.Attributes["name"].Value + " = " + node.Attributes["value"].Value));
				break;
			}
		    }
		}
	    }

	    // Default report extraction attributes to the dao attributes.
	    if (this.reportExtractionDaoBaseClass == String.Empty) {
		this.reportExtractionDaoBaseClass = this.daoBaseClass;
	    }
	    if (this.reportExtractionDaoClassDirectory == String.Empty) {
		this.reportExtractionDaoClassDirectory = this.daoClassDirectory;
	    }
	}

	// methods
	public String GetProcName(String table, String type) {
	    String s;

	    s = "proc" + table.Replace(" ", "_") + type;
	    s = "sp" + table.Replace(" ", "_") + "_" + type;

	    return s;
	}

	public String GetDAONameSpace(String table) {
	    String s;

	    s = this.rootNameSpace;
	    if (daoClassDirectory.Length>0) {
		s += "." + daoClassDirectory;
	    }

	    return s;
	}

	public String GetReportExtractionDAONameSpace(String table) {
	    String s;

	    s = this.rootNameSpace;
	    if (reportExtractionDaoClassDirectory.Length>0) {
		s += "." + daoClassDirectory;
	    }

	    return s;
	}

	public String GetDONameSpace(String table) {
	    String s;

	    s = this.rootNameSpace;
	    if (doClassDirectory.Length>0) {
		s += "." + doClassDirectory;
	    }

	    return s;
	}

	public String GetBusinessLogicNameSpace() {
	    // TODO:  this is hardcoded!  Need to deal with namespaces better - maybe defined on the entity themselves.
	    String s = this.rootNameSpace + "." + "BusinessLogic";
	    return s;
	}

	public String GetDAOClassName(String table) {
	    String s;

	    s = "cls" + table.Replace(" ", "_");
	    s = table.Replace(" ", "_") + "DAO";

	    return s;
	}

	public String GetReportExtractionDAOClassName(String table) {
	    String s;

	    s = "cls" + table.Replace(" ", "_");
	    s = table.Replace(" ", "_") + "DAO";

	    return s;
	}

	public String GetDOClassName(String table) {
	    String s;

	    s = "cls" + table.Replace(" ", "_");
	    s = table.Replace(" ", "_") + "Data";

	    return s;
	}

	public String GetTypeClassName(String name) {
	    return name;
	}

	public String GetTypeNameSpace(String name) {
	    String s = this.rootNameSpace;
	    if (typesClassDirectory.Length>0) {
		s += "." + typesClassDirectory;
	    }
	    return s;
	}

	public String GetCollectionClassName(String name) {
	    return name;
	}

	public String GetCollectionNameSpace(String name) {
	    String s = this.rootNameSpace;
	    if (collectionClassDirectory.Length>0) {
		s += "." + collectionClassDirectory;
	    }
	    return s;
	}

	public String GetTestNameSpace(String table) {
	    String s;

	    s = this.rootNameSpace;
	    if (testClassDirectory.Length>0) {
		s += "." + testClassDirectory;
	    }

	    return s;
	}

	/// <summary>
	/// Returns the phrase to use to get the sql format of a type.  Currently
	/// only used with Velocity generator.
	/// </summary>
	/// <param name="field">Field whose sql format is needed.</param>
	/// <returns>String for converting the field to sql format (like artProjectId.DBValue)</returns>
	public String GetSqlConversion(PropertyElement field) {
	    return String.Format(field.Type.ConvertToSqlTypeFormat, "", field.GetFieldFormat(), "", "", field.GetFieldFormat());
	}

	/// <summary>
	/// Returns the syntax to get the data type of a field from a DataReader object.
	/// Currently only used with Velocity generator.
	/// </summary>
	/// <param name="field">Field whose reader syntax is needed.</param>
	/// <returns>Reader syntax to use.</returns>
	public String GetReaderString(PropertyElement field) {
	    String readerMethod = String.Format(field.Column.SqlType.ReaderMethodFormat, "dataReader", field.Column.Name);
	    if (field.Type.ConvertFromSqlTypeFormat.Length >0) {
		readerMethod = 
		    String.Format(field.Type.ConvertFromSqlTypeFormat, "data", field.GetMethodFormat(), readerMethod, "dataReader", field.Column.Name);
	    } 
	    return readerMethod;
	}

	/// <summary>
	/// Returns the syntax to get the data type of a field from a stored procedure 
	/// return value.  Note it assumes the command used to execute the stored
	/// procedure is named "cmd".  Currently only used with Velocity generator.
	/// </summary>
	/// <param name="field">Field whose reader syntax is needed.</param>
	/// <returns>Reader syntax to use.</returns>
	public String GetProcedureReturnString(PropertyElement field) {
	    String readerMethod = "(Int32)(cmd.Parameters[\"RETURN_VALUE\"].Value)";
	    if (field.Type.ConvertFromSqlTypeFormat.Length >0) {
		readerMethod = 
		    String.Format(field.Type.ConvertFromSqlTypeFormat, "", "", 
		    readerMethod, "", "");
	    } 
	    return readerMethod;
	}

	/// <summary>
	/// Returns the syntax to get the data type of a field from a select Scope_Identity().
	/// Note it assumes the id value has been put in an int variable named returnId.
	/// Currently only used with Velocity generator.
	/// </summary>
	/// <param name="field">Field whose reader syntax is needed.</param>
	/// <returns>Reader syntax to use.</returns>
	public String GetSelectIdentityString(PropertyElement field) {
	    String readerMethod = "returnId";
	    if (field.Type.ConvertFromSqlTypeFormat.Length >0) {
		readerMethod = 
		    String.Format(field.Type.ConvertFromSqlTypeFormat, "", "", 
		    readerMethod, "", "");
	    } 
	    return readerMethod;
	}

	/// <summary>
	/// Returns sorted list of name spaces needed for the entity.
	/// </summary>
	/// <param name="entity">Entity code is being generated for</param>
	/// <param name="isDaoClass">Indicates if a DAO class is being generated.</param>
	/// <returns></returns>
	public ArrayList GetUsingNamespaces(IPropertyContainer entity, Boolean isDaoClass) {

	    ArrayList namespaces = new ArrayList();
	    namespaces.Add("System");

	    if (isDaoClass) {
		namespaces.Add("System.Collections");
		namespaces.Add("System.Configuration");
		namespaces.Add("System.Data");
		namespaces.Add("System.Data.SqlClient");
		namespaces.Add("Spring2.Core.DAO");
		namespaces.Add(GetDONameSpace(null));
	    }

	    foreach (PropertyElement field in entity.Fields) {
		if (!field.Type.Package.Equals(String.Empty) && !namespaces.Contains(field.Type.Package)) {
		    namespaces.Add(field.Type.Package);
		}
	    }

	    Array names = namespaces.ToArray(typeof(String));
	    Array.Sort(names);

	    return new ArrayList(names);
	}

	public static string PrepareExpression(string withEmbedded, IExpressionContainer container, string idString, ParserValidationDelegate vd) {
	    string checkExpression = withEmbedded;
	    string retVal = String.Empty;
	    int leftBrace = 0;
	    int startPos = 0;
	    for(leftBrace=checkExpression.IndexOf("{", startPos); startPos >=0; leftBrace=checkExpression.IndexOf("{", startPos)) {
		if (leftBrace == -1) {
		    // No more strings to replace.
		    retVal += checkExpression.Substring(startPos, checkExpression.Length - startPos);
		    break;
		} else {
		    // Concatenate portion of string without embedded references.
		    retVal += checkExpression.Substring(startPos, leftBrace - startPos);
		}

		int rightBrace = checkExpression.IndexOf("}", leftBrace);
		if (rightBrace == -1) {
		    if (vd != null) {
			vd(ParserValidationArgs.NewError("The " + idString + " has a left brace({} with no corresonding right brace(}}"));
		    }
		    return "";
		}

		// Isolate the property reference and concatenate it's expansion.
		string expressionReference = checkExpression.Substring(leftBrace+1, rightBrace - leftBrace - 1);
		retVal += container.GetExpressionSubstitution(expressionReference, idString, vd);
		
		// On to the next reference.
		startPos = rightBrace + 1;
	    }

	    return retVal;
	}
    }
}
