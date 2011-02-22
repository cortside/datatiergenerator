using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.PrettyPrinter;

namespace Spring2.DataTierGenerator.Generator.Writer {
    public class SourceUnit {
    	private CompilationUnit unit;
    	private IList<ISpecial> specials;

    	private List<NamespaceUnit> namespaces = new List<NamespaceUnit>();
	private List<UsingDeclaration> imports = new List<UsingDeclaration>();

	public SourceUnit(String content) {
	    using (ICSharpCode.NRefactory.IParser parser = ParserFactory.CreateParser(SupportedLanguage.CSharp, new StringReader(content))) {
		parser.Parse();
		if (parser.Errors.Count > 0) {
		    throw new Exception(parser.Errors.ErrorOutput);
		}
		specials = parser.Lexer.SpecialTracker.RetrieveSpecials();
		unit = parser.CompilationUnit;
	    	NRefactoryUtil.InsertSpecials(unit, specials);
		//List<ISpecial> rightSpecials = new List<ISpecial>();
		//rightSpecials.AddRange(specials);
		//MapSpecials(unit.Children, rightSpecials);
		//
		//if (rightSpecials.Count > 0) {
		//    System.Diagnostics.Debug.WriteLine("There are specials that did not get mapped to members.");
		//}
	    }

	    foreach (INode child in unit.Children) {
		if (child is UsingDeclaration) {
		    imports.Add(child as UsingDeclaration);
		} else if (child is NamespaceDeclaration) {
		    namespaces.Add(new NamespaceUnit(child as NamespaceDeclaration));
		} else if (child is TypeDeclaration) {
		    if (namespaces.Count == 0) {
			namespaces.Add(new NamespaceUnit());
		    }
		    namespaces[0].AddType(new TypeUnit(child as TypeDeclaration));
		} else {
		    throw new Exception("Unknown node type: " + child.GetType() + " - " + child.ToString());
		}
	    }

	}

	public SourceUnit(CompilationUnit unit, IList<ISpecial> specials) {
	    this.unit = unit;
	    this.specials = specials;
	}

	public void Merge(SourceUnit generatedUnit) {
	    RemoveGeneratedMembers(unit.Children, generatedUnit.unit.Children);

	    foreach (INode child in generatedUnit.unit.Children) {
		// need to have a wrapper for the CompilationUnit that has
		// * this could be what keeps the CU and the specials collection together
		// * methods to expose imports and namespaces
		// * then namespaces needs to have wrapper (like NamespaceUnit?) that exposes types
		// * then types needs to have wrapper that exposes members
		// will need to deal with the specials collection

		if (child is UsingDeclaration) {
		    MergeUsing(child as UsingDeclaration);
		} else if (child is NamespaceDeclaration) {
		    MergeNamespace(child as NamespaceDeclaration);
		} else if (child is TypeDeclaration) {
		    MergeType(unit.Children, child as TypeDeclaration);
		} else {
		    throw new Exception("Unknown node type: " + child.GetType() + " - " + child.ToString());
		}
	    }
	}

	//private static void MapSpecials(List<INode> children, List<ISpecial> unmappedSpecials) {
	//    foreach (INode node in children) {
	//        if (node.UserData != null) {
	//            System.Diagnostics.Debug.WriteLine(node.UserData);
	//        }
	//        List<ISpecial> memberSpecials = GetMemberSpecials(unmappedSpecials, node);
	//        node.UserData = memberSpecials;
	//        foreach(ISpecial memberSpecial in memberSpecials) {
	//            unmappedSpecials.Remove(memberSpecial);
	//        }

	//        MapSpecials(node.Children, unmappedSpecials);
	//    }
	//}

	//private static List<ISpecial> GetMemberSpecials(IList<ISpecial> specials, INode node) {
	//    List<ISpecial> results = new List<ISpecial>();

	//    foreach (ISpecial special in specials) {
	//        if (special.StartPosition < node.StartLocation) {
	//            results.Add(special);
	//        }
	//    }
	//    return results;
	//}

    	private static void RemoveGeneratedMembers(List<INode> existingNodes, List<INode> generatedNodes) {
	    List<INode> nodesToRemove = new List<INode>();

	    foreach(INode node in existingNodes) {
	    	Boolean removed = false;
                INode gNode = null;
                bool found = false;
                foreach (INode n in generatedNodes) {
                    if (CompareNodes(node, n)) {
                        found = true;
                        gNode = n;
                        break;
                    }
                }
                if (!found && HasGenerateAttribute(node)) {
                    nodesToRemove.Add(node);
                    removed = true;
                }
		if (!removed && gNode != null) {
		    RemoveGeneratedMembers(node.Children, gNode.Children);
		}
	    }

	    foreach(INode node in nodesToRemove) {
	    	existingNodes.Remove(node);
	    }
	}

        private static bool CompareNodes(INode left, INode right) {
            if (left is UsingDeclaration && right is UsingDeclaration) {
                return CompareUsingNodes((UsingDeclaration)left, (UsingDeclaration)right);
            }
            if (left is ConstructorDeclaration && right is ConstructorDeclaration) {
                return CompareConstructorNodes((ConstructorDeclaration)left, (ConstructorDeclaration)right);
            }
            if (left is FieldDeclaration && right is FieldDeclaration) {
                return CompareFieldNodes((FieldDeclaration)left, (FieldDeclaration)right);
            }
            if (left is MemberNode && right is MemberNode) {
                return CompareMemberNodes((MemberNode)left, (MemberNode)right);
            }
            if (left is NamespaceDeclaration && right is NamespaceDeclaration) {
                return CompareNamespaceNodes((NamespaceDeclaration)left, (NamespaceDeclaration)right);
            }
            if (left is TypeDeclaration && right is TypeDeclaration) {
                return CompareTypeNodes((TypeDeclaration)left, (TypeDeclaration)right);
            }
            return false;
        }

    	private static bool HasGenerateAttribute(INode node) {
            if (node is AttributedNode) {
                AttributedNode field = node as AttributedNode;

                foreach (AttributeSection section in field.Attributes) {
                    foreach (ICSharpCode.NRefactory.Ast.Attribute attribute in section.Attributes) {
                        if (attribute.Name == "Generate") {
                            return true;
                        }
                    }
                }
            }

    	    return false;
    	}

    	public String ToCSharp2() {
	    IOutputAstVisitor outputVisitor = new CSharpOutputVisitor();
	    SetPrettyPrintOptions(outputVisitor);

	    using (SpecialNodesInserter.Install(specials, outputVisitor)) {
		unit.AcceptVisitor(outputVisitor, null);
	    }
	    return outputVisitor.Text;
	}

	public String ToCSharp() {
	    IOutputAstVisitor outputVisitor = new CSharpOutputVisitor();
	    SetPrettyPrintOptions(outputVisitor);

	    using (SpecialNodesByMapInserter.Install(new Hashtable(), outputVisitor)) {
		unit.AcceptVisitor(outputVisitor, null);
	    }
	    String code = outputVisitor.Text;
	    return NRefactoryUtil.FixSourceFormatting(code);
	}

	public String ToNodeMap() {
	    StringBuilder sb = new StringBuilder();
	    ToNodeMap(unit.Children, sb, 0);
	    return sb.ToString();
	}

	private void ToNodeMap(List<INode> nodes, StringBuilder sb, Int32 level) {
	    level++;
	    
	    foreach(INode node in nodes) {
		sb.Append(level).Append(" - ").AppendLine(node.GetType().Name);
	    	ToNodeMap(node.Children, sb, level);
		if (node is MethodDeclaration) {
		    MethodDeclaration method = node as MethodDeclaration;
		    ToNodeMap(method.Body.Children, sb, level);
		}
		if (node is IfElseStatement) {
			IfElseStatement ifelse = node as IfElseStatement;
			ToNodeMap(ifelse.Condition.Children, sb, level);
		}
	    }
	}

	private void SetPrettyPrintOptions(IOutputAstVisitor outputVisitor) {
	    PrettyPrintOptions options = outputVisitor.Options as PrettyPrintOptions;
	    options.NamespaceBraceStyle = BraceStyle.EndOfLine;
	    options.ClassBraceStyle = BraceStyle.EndOfLine;
	    options.MethodBraceStyle = BraceStyle.EndOfLine;
	    options.PropertyBraceStyle = BraceStyle.EndOfLine;
	    options.PropertyGetBraceStyle = BraceStyle.EndOfLine;
	    options.PropertySetBraceStyle = BraceStyle.EndOfLine;
	    options.ConstructorBraceStyle = BraceStyle.EndOfLine;
	    options.TabSize = 8;
	    options.IndentSize = 4;
	    options.IndentationChar = ' ';
	}

    	private void MergeUsing(UsingDeclaration child) {
	    if (child.Usings.Count != 1) {
		throw new ArgumentOutOfRangeException("child", "didn't expect to have more than 1 in Usings collection");
	    }
	    Int32 insertAt = 0;
	    Boolean add = true;
	    foreach (INode node in unit.Children) {
		if (node is UsingDeclaration) {
		    UsingDeclaration element = node as UsingDeclaration;
		    if (element.Usings.Count != 1) {
			throw new Exception("didn't expect to have more than 1 in the Usings collection");
		    }
		    if (CompareUsingNodes(element, child)) {
			add = false;
		    }
		    insertAt++;
		}
	    }

	    if (add) {
	    	unit.Children.Insert(insertAt, child);
	    }
	}

        private static bool CompareUsingNodes(UsingDeclaration left, UsingDeclaration right) {
            return left.Usings[0].Name == right.Usings[0].Name;
        }

	//private void AddSpecialsForNode(INode node, Hashtable specialMap) {
	//    List<ISpecial> memberSpecials = specialMap[node] as List<ISpecial>;
	//    if (memberSpecials != null && memberSpecials.Count > 0) {
	//        ((List<ISpecial>)specials).AddRange(memberSpecials);
	//        node.UserData = memberSpecials;
	//    }
	//    foreach(INode child in node.Children) {
	//        AddSpecialsForNode(child, specialMap);
	//    }
	//}

    	private void MergeNamespace(NamespaceDeclaration child) {
	    Int32 insertAt = 0;
	    Boolean add = true;
	    foreach (INode node in unit.Children) {
		if (node is UsingDeclaration) {
		    insertAt++;
		} else if (node is NamespaceDeclaration) {
		    NamespaceDeclaration element = node as NamespaceDeclaration;
		    if (CompareNamespaceNodes(element, child)) {
			add = false;
		    	MergeTypes(element, child);
		    }
		}
	    }

	    if (add) {
		unit.Children.Insert(insertAt, child);
	    }
	}

        private static bool CompareNamespaceNodes(NamespaceDeclaration left, NamespaceDeclaration right) {
	    return left.Name == right.Name;
        }

	private void MergeType(List<INode> collection, TypeDeclaration child) {
	    Boolean add = true;
	    foreach (INode node in collection) {
		if (node is TypeDeclaration) {
		    TypeDeclaration element = node as TypeDeclaration;
		    if (CompareTypeNodes(element, child)) {
			add = false;
			MergeMembers(element, child);
		    }
		}
	    }
	    if (add) {
	    	collection.Add(child);
	    }
	}

        private static bool CompareTypeNodes(TypeDeclaration left, TypeDeclaration right) {
            return left.Name == right.Name;
        }

	private void MergeTypes(NamespaceDeclaration left, NamespaceDeclaration right) {
	    foreach(INode node in right.Children) {
		if (node is TypeDeclaration)
	    	MergeType(left.Children, node as TypeDeclaration);
	    }
	}

	private void MergeMembers(TypeDeclaration left, TypeDeclaration right) {
	    foreach (INode node in right.Children) {
		if (node is MemberNode) {
		    MergeMember(left.Children, node as MemberNode);
		} else if (node is FieldDeclaration) {
		    MergeField(left.Children, node as FieldDeclaration);
		} else if (node is ConstructorDeclaration) {
		    MergeConstructor(left.Children, node as ConstructorDeclaration);
		} else if (node is TypeDeclaration) { // subclasses, for example
		    MergeType(left.Children, node as TypeDeclaration);
		} else {
		    System.Diagnostics.Debug.WriteLine(node.GetType());
		}
	    }
	}

	private void MergeConstructor(List<INode> collection, ConstructorDeclaration child) {
	    Boolean add = true;
	    Int32 index = -1;
            Boolean replace = false;
	    foreach (INode node in collection) {
		if (node is ConstructorDeclaration) {
		    ConstructorDeclaration element = node as ConstructorDeclaration;
                    /*
		    bool paramsAreTheSame = IsParamListTheSame(element.Parameters, child.Parameters);
		    bool modifySettingIsTheSame = ( element.Modifier == child.Modifier );

		    // for this particular case, we accept an old, private constructor over the new, internal one, rather than both
		    modifySettingIsTheSame |= ( element.Modifier == Modifiers.Internal && child.Modifier == Modifiers.Private );

		    if ( (element.Name == child.Name) && (paramsAreTheSame) && modifySettingIsTheSame) {
                        if (HasGenerateAttribute(node)) {
                            replace = true;
                            index = collection.IndexOf(element);
                        }
			add = false;
			break;
		    }
                    */
                    if (CompareConstructorNodes(element, child)) {
                        if (HasGenerateAttribute(node)) {
                            replace = true;
                            index = collection.IndexOf(element);
                        }
                        add = false;
                        break;
                    }
                }
	    }
	    if (add) {
		collection.Add(child);
	    } else if (replace) {
		// need to get rid of the [Generate()] metadata
		collection.RemoveAt(index);
		collection.Insert(index, child);
	    }
	}

        private static bool CompareConstructorNodes(ConstructorDeclaration left, ConstructorDeclaration right) {
            bool paramsAreTheSame = IsParamListTheSame(left.Parameters, right.Parameters);
            bool modifySettingIsTheSame = (left.Modifier == right.Modifier);

            // for this particular case, we accept an old, private constructor over the new, internal one, rather than both
            modifySettingIsTheSame |= (left.Modifier == Modifiers.Private && right.Modifier == Modifiers.Internal);

            return (left.Name == right.Name) && (paramsAreTheSame) && modifySettingIsTheSame;
        }

	private void MergeMember(List<INode> collection, MemberNode child) {
	    Boolean add = true;
	    Boolean replace = false;
	    Int32 index = -1;
	    foreach (INode node in collection) {

		if (node is MemberNode) {
		    MemberNode element = node as MemberNode;

                    /*
		    // Check for duplicates as MemberNode (typical case immediately below, Method-specific checks after)
		    if ( (element.Name == child.Name) && (element.InterfaceImplementations.Count == child.InterfaceImplementations.Count) ) {
			bool elementAndChildAreTheSame = true; // assume it is true unless we find proof otherwise

			// verify that the interface implementations (if any) are the same
			foreach (InterfaceImplementation elementInterfaceImpl in element.InterfaceImplementations) {
			    // find the same interfaceImpl value in child
			    bool bThisInterfaceIsFound = false;
			    foreach (InterfaceImplementation childInterfaceImpl in child.InterfaceImplementations) {
				if (childInterfaceImpl.InterfaceType.Type == elementInterfaceImpl.InterfaceType.Type) {
				    bThisInterfaceIsFound = true;
				    break;
				}
			    }
			    elementAndChildAreTheSame &= bThisInterfaceIsFound;
			    if (!elementAndChildAreTheSame) {
				break;
			    }
			}


			// Check for duplicates as MethodNode, including compare of parameter lists
			if (elementAndChildAreTheSame && element is MethodDeclaration) {
			    MethodDeclaration elementAsMethod = element as MethodDeclaration;
			    MethodDeclaration childAsMethod = child as MethodDeclaration;
			    elementAndChildAreTheSame &= IsParamListTheSame(elementAsMethod.Parameters, child.Parameters);
			}

			if (elementAndChildAreTheSame) {
			    add = false;
                            if (HasGenerateAttribute(node)) {
                                replace = true;
                                index = collection.IndexOf(element);
                            }
			}
		    }*/

                    if (CompareMemberNodes(element, child)) {
                        add = false;
                        if (HasGenerateAttribute(node)) {
                            replace = true;
                            index = collection.IndexOf(element);
                        }
                    }

		}
		
	    }
	    if (replace) {
		collection.RemoveAt(index);
		collection.Insert(index, child);
	    }
	    if (add) {
		collection.Add(child);
	    }
	}

        private static bool CompareMemberNodes(MemberNode left, MemberNode right) {
            bool areSame = false;
            // Check for duplicates as MemberNode (typical case immediately below, Method-specific checks after)
            if ((left.Name == right.Name) && (left.InterfaceImplementations.Count == right.InterfaceImplementations.Count)) {
                areSame = true; // assume it is true unless we find proof otherwise

                // verify that the interface implementations (if any) are the same
                foreach (InterfaceImplementation leftInterfaceImpl in left.InterfaceImplementations) {
                    // find the same interfaceImpl value in child
                    bool bThisInterfaceIsFound = false;
                    foreach (InterfaceImplementation rightInterfaceImpl in right.InterfaceImplementations) {
                        if (rightInterfaceImpl.InterfaceType.Type == leftInterfaceImpl.InterfaceType.Type) {
                            bThisInterfaceIsFound = true;
                            break;
                        }
                    }
                    areSame &= bThisInterfaceIsFound;
                    if (!areSame) {
                        break;
                    }
                }


                // Check for duplicates as MethodNode, including compare of parameter lists
                if (areSame && left is MethodDeclaration) {
                    MethodDeclaration leftAsMethod = left as MethodDeclaration;
                    MethodDeclaration rightAsMethod = right as MethodDeclaration;
                    areSame &= IsParamListTheSame(leftAsMethod.Parameters, right.Parameters);
                }
            }

            return areSame;
        }

	private void MergeField(List<INode> collection, FieldDeclaration child) {
	    Boolean add = true;
	    Int32 index = -1;
            Boolean replace = false;
	    foreach (INode node in collection) {
		if (node is FieldDeclaration) {
		    FieldDeclaration element = node as FieldDeclaration;
		    if (CompareFieldNodes(element, child)) {
                        if (HasGenerateAttribute(node)) {
                            index = collection.IndexOf(element);
                            replace = true;
                        }
			add = false;
			break;
		    }
		}
	    }
	    if (add) {
		collection.Add(child);
	    } else if (replace) {
		// need to get rid of the [Generate()] metadata
		collection.RemoveAt(index);
		collection.Insert(index, child);
	    }
	}

        private static bool CompareFieldNodes(FieldDeclaration left, FieldDeclaration right) {
            return left.Fields[0].Name == right.Fields[0].Name;
        }

	private static bool IsParamListTheSame(List<ParameterDeclarationExpression> params1, List<ParameterDeclarationExpression> params2) {
	    int params1Count = params1.Count;
	    int params2Count = params2.Count;
	    bool paramsAreTheSame = (params1Count == params2Count);
	    if (paramsAreTheSame) {
		for (int curParamPos = 0; curParamPos < params1Count; curParamPos++) {
		    ParameterDeclarationExpression curParam1 = params1[curParamPos];
		    ParameterDeclarationExpression curParam2 = params2[curParamPos];
		    paramsAreTheSame &= TypeReference.AreEqualReferences(curParam2.TypeReference, curParam1.TypeReference);
		    if (!paramsAreTheSame) {
			break;
		    }
		}
	    }
	    return paramsAreTheSame;
	}

    }
}
