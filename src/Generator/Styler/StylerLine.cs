using System;
using System.Text.RegularExpressions;
using Spring2.Core.Types;

namespace Spring2.DataTierGenerator.Generator.Styler {
    /// <summary>
    /// Summary description for StylerLine.
    /// </summary>
    public class StylerLine {
	private String text;
	private StylerLineList lines;
	private Int32 indent = 0;

	public StylerLine(String text, StylerLineList lines) {
	    this.text = text.Trim();
	    this.lines = lines;
	}

	public string Text {
	    get { return text; }
	}

	public int Indent {
	    get { return indent; }
	    set { indent = value; }
	}

	public bool IsType {
	    get {
		if (!IsComment && (
		    IsNameSpace ||
		    IsClass ||
		    IsStruct ||
		    IsEnum)) {
		    return true;
		} else {
		    return false;
		}
	    }
	}

	public bool IsFlow {
	    get {
		if (!IsComment && (
		    IsIf ||
		    IsElse ||
		    IsElseIf ||
		    IsTry ||
		    IsCatch ||
		    IsFinally ||
		    IsFor ||
		    IsForEach ||
		    IsWhile ||
		    IsSwitch
		    )) {
		    return true;
		} else {
		    return false;
		}
	    }
	}

	public bool IsFunction{
	    get {
		if (Regex.IsMatch(text, @"^\s*(\w+)\s+(\w+).*\(+") &&
		    !IsDeclaration &&
		    !IsComment &&
		    !IsType &&
		    !IsFlow &&
		    !IsReturn) {
		    return true;
		} else {
		    return false;
		}
	    }
	}

	public bool IsProperty {
	    get {
		Int32 index = lines.IndexOf(this);
		if (Regex.IsMatch(text, @"^\s*(\w+)\s+(\w+).*") &&
		    !IsDeclaration &&
		    !IsComment &&
		    !IsType &&
		    !IsFlow &&
		    !IsFunction &&
		    !IsReturn && (EndWithBrace || lines[index+1].StartWithOpenBrace)) {
		    return true;
		} else {
		    return false;
		}
	    }
	}

	private static Regex removeEndBrace = new Regex(@"\{\s*$");
	public StylerLine RemoveEndBrace{
	    get {
		return new StylerLine(removeEndBrace.Replace(this.text, ""), lines);
	    }
	}

	private static Regex addHangingBrace = new Regex(@"\S+\s*");
	public StylerLine AddHangingBrace{
	    get {
		string blank = addHangingBrace.Replace(this.text, "");
		return new StylerLine(blank + "{", lines);
	    }
	}

	private static Regex isDeclarationRegex = new Regex(@"\;\s*(\/+|$)");
	private BooleanType isDeclaration = BooleanType.DEFAULT;
	public bool IsDeclaration{
	    get {
		if (!isDeclaration.IsValid) {
		    isDeclaration = BooleanType.GetInstance(isDeclarationRegex.IsMatch(this.text));
		}
		return isDeclaration.ToBoolean();
	    }
	}

	private static Regex isCommentRegex = new Regex(@"^(\s*\/+|\s*\*+|\s*\#+)");
	private BooleanType isComment = BooleanType.DEFAULT;
	public bool IsComment{
	    get {
		if (!isComment.IsValid) {
		    isComment = BooleanType.GetInstance(isCommentRegex.IsMatch(this.text));
		}
		return isComment.ToBoolean();
	    }
	}

	private static Regex endWithBraceRegex = new Regex(@"\{\s*$");
	private BooleanType endWithBrace = BooleanType.DEFAULT;
	public bool EndWithBrace{
	    get {
		if (!endWithBrace.IsValid) {
		    endWithBrace = BooleanType.GetInstance(endWithBraceRegex.IsMatch(this.text));
		}
		return endWithBrace.ToBoolean();
	    }
	}

	private static Regex isHangingBraceRegex = new Regex(@"^\s*\{");
	private BooleanType isHangingBrace = BooleanType.DEFAULT;
	public bool IsHangingBrace{
	    get {
		if (!isHangingBrace.IsValid) {
		    isHangingBrace = BooleanType.GetInstance(isHangingBraceRegex.IsMatch(this.text));
		}
		return isHangingBrace.ToBoolean();
	    }
	}

//    if (!.IsValid) {
//	= BooleanType.GetInstance(Regex.IsMatch(this.text));
//    }
//    return .ToBoolean();
    
    private static Regex isBlankLineRegex = new Regex(@"^\s*$");
	private BooleanType isBlankLine = BooleanType.DEFAULT;
	public bool IsBlankLine{
	    get {
		if (!isBlankLine.IsValid) {
		     isBlankLine = BooleanType.GetInstance(isBlankLineRegex.IsMatch(this.text));
		}
		return isBlankLine.ToBoolean();
	    }
	}

	private static Regex isNameSpaceRegex = new Regex(@"(^|\s+)namespace\s+");
	private BooleanType isNameSpace = BooleanType.DEFAULT;
	public bool IsNameSpace{
	    get {
		if (!isNameSpace.IsValid) {
		    isNameSpace= BooleanType.GetInstance(isNameSpaceRegex.IsMatch(this.text));
		}
		return isNameSpace.ToBoolean();
	    }
	}

	private static Regex isClassRegex = new Regex(@"(public|internal|abstract|private|^)(\s+|^)class\s+\w+");
	private BooleanType isClass = BooleanType.DEFAULT;
	public bool IsClass{
	    get {
		if (!isClass.IsValid) {
		    isClass = BooleanType.GetInstance(isClassRegex.IsMatch(this.text));
		}
		return isClass.ToBoolean();
	    }
	}

	private static Regex isStructRegex = new Regex(@"\s+struct\s+");
	private BooleanType isStruct = BooleanType.DEFAULT;
	public bool IsStruct{
	    get {
		if (!isStruct.IsValid) {
		    isStruct = BooleanType.GetInstance(isStructRegex.IsMatch(this.text));
		}
		return isStruct.ToBoolean();
	    }
	}

	private static Regex isEnumRegex = new Regex(@"\s+enum\s+");
	private BooleanType isEnum = BooleanType.DEFAULT;
	public bool IsEnum{
	    get {
		if (!isEnum.IsValid) {
		    isEnum = BooleanType.GetInstance(isEnumRegex.IsMatch(this.text));
		}
		return isEnum.ToBoolean();
	    }
	}

	private static Regex isIfRegex = new Regex(@"(^|\s+|\}+)if(\s+|\(+|$)");
	private BooleanType isIf = BooleanType.DEFAULT;
	public bool IsIf{
	    get {
		if (!isIf.IsValid) {
		    isIf = BooleanType.GetInstance(isIfRegex.IsMatch(this.text));
		}
		return isIf.ToBoolean();
	    }
	}

	private static Regex isElseRegex = new Regex(@"(^|\s*|\}+)else(\s*|\{+|$|/s*\/\/.*$)");
	private BooleanType isElse = BooleanType.DEFAULT;
	public bool IsElse{
	    get {
		if (!isElse.IsValid) {
		    isElse = BooleanType.GetInstance(isElseRegex.IsMatch(this.text));
		}
		return isElse.ToBoolean();
	    }
	}

	private static Regex isElseIfRegex = new Regex(@"(^|\s+|\}+)else if(\s+|\(+|$)");
	private BooleanType isElseIf = BooleanType.DEFAULT;
	public bool IsElseIf{
	    get {
		if (!isElseIf.IsValid) {
		    isElseIf = BooleanType.GetInstance(isElseIfRegex.IsMatch(this.text));
		}
		return isElseIf.ToBoolean();
	    }
	}

	private static Regex isTryRegex = new Regex(@"^\s*try(\s*|\(+|$)");
	private BooleanType isTry = BooleanType.DEFAULT;
	public bool IsTry{
	    get {
		if (!isTry.IsValid) {
		    isTry = BooleanType.GetInstance(isTryRegex.IsMatch(this.text));
		}
		return isTry.ToBoolean();
	    }
	}

	private static Regex isCatchRegex = new Regex(@"(^|\s+|\}+)catch(\s+|\(+|$)");
	private BooleanType isCatch = BooleanType.DEFAULT;
	public bool IsCatch{
	    get {
		if (!isCatch.IsValid) {
		    isCatch = BooleanType.GetInstance(isCatchRegex.IsMatch(this.text));
		}
		return isCatch.ToBoolean();
	    }
	}

	private static Regex isFinallyRegex = new Regex(@"(^|\s+|\}+)finally(\s+|\{+|$)");
	private BooleanType isFinally = BooleanType.DEFAULT;
	public bool IsFinally{
	    get {
		if (!isFinally.IsValid) {
		    isFinally = BooleanType.GetInstance(isFinallyRegex.IsMatch(this.text));
		}
		return isFinally.ToBoolean();
	    }
	}

	private static Regex isForRegex = new Regex(@"(^|\s+|\}+)for(\s*\(+|$)");
	private BooleanType isFor = BooleanType.DEFAULT;
	public bool IsFor{
	    get {
		if (!isFor.IsValid) {
		    isFor = BooleanType.GetInstance(isForRegex.IsMatch(this.text));
		}
		return isFor.ToBoolean();
	    }
	}

	private static Regex isForEachRegex = new Regex(@"^\s*foreach(\s+|\(+|$)");
	private BooleanType isForEach = BooleanType.DEFAULT;
	public bool IsForEach{
	    get {
		if (!isForEach.IsValid) {
		    isForEach = BooleanType.GetInstance(isForEachRegex.IsMatch(this.text));
		}
		return isForEach.ToBoolean();
	    }
	}

	private static Regex isWhileRegex = new Regex(@"(^\s*|\}+)while(\s+|\(+|$)");
	private BooleanType isWhile = BooleanType.DEFAULT;
	public bool IsWhile{
	    get {
		if (!isWhile.IsValid) {
		    isWhile = BooleanType.GetInstance(isWhileRegex.IsMatch(this.text));
		}
		return isWhile.ToBoolean();
	    }
	}

	private static Regex isSwitchRegex = new Regex(@"(^|\s+|\}+)switch(\s+|\(+|$)");
	private BooleanType isSwitch = BooleanType.DEFAULT;
	public bool IsSwitch{
	    get {
		if (!isSwitch.IsValid) {
		    isSwitch = BooleanType.GetInstance(isSwitchRegex.IsMatch(this.text));
		}
		return isSwitch.ToBoolean();
	    }
	}

	private static Regex isCaseRegex = new Regex(@"(^\s*|\}+)case(\s+|\(+|$)");
	private BooleanType isCase = BooleanType.DEFAULT;
	public bool IsCase{
	    get {
		if (!isCase.IsValid) {
		    isCase = BooleanType.GetInstance(isCaseRegex.IsMatch(this.text));
		}
		return isCase.ToBoolean();
	    }
	}

	private static Regex isGetRegex = new Regex(@"(^\s*|\}+)get\s*({|$)");
	private BooleanType isGet = BooleanType.DEFAULT;
	public bool IsGet{
	    get {
		if (!isGet.IsValid) {
		    isGet = BooleanType.GetInstance(isGetRegex.IsMatch(this.text));
		}
		return isGet.ToBoolean();
	    }
	}

	private static Regex isSetRegex = new Regex(@"(^\s*|\}+)set\s*({|$)");
	private BooleanType isSet = BooleanType.DEFAULT;
	public bool IsSet{
	    get {
		if (!isSet.IsValid) {
		    isSet = BooleanType.GetInstance(isSetRegex.IsMatch(this.text));
		}
		return isSet.ToBoolean();
	    }
	}

	private static Regex endWithCloseBraceRegex = new Regex(@"\}\s*$");
	private BooleanType endWithCloseBrace = BooleanType.DEFAULT;
	public bool EndWithCloseBrace{
	    get {
		if (!endWithCloseBraceAndComment.IsValid) {
		    endWithCloseBrace = BooleanType.GetInstance(endWithCloseBraceRegex.IsMatch(text));
		}
		return endWithCloseBrace.ToBoolean();
	    }
	}

	private static Regex endWithCloseBraceAndCommentRegex = new Regex(@"\}\s*(\/\/|$)");
	private BooleanType endWithCloseBraceAndComment = BooleanType.DEFAULT;
	public bool EndWithCloseBraceAndComment{
	    get {
		if (!endWithCloseBraceAndComment.IsValid) {
		    endWithCloseBraceAndComment = BooleanType.GetInstance(endWithCloseBraceAndCommentRegex.IsMatch(text));
		}

		return endWithCloseBraceAndComment.ToBoolean();
	    }
	}

	private static Regex startWithOpenBraceRegex = new Regex(@"^\s*\}");
	private BooleanType startWithOpenBrace = BooleanType.DEFAULT;
	public bool StartWithOpenBrace{
	    get {
		if (!startWithOpenBrace.IsValid) {
		    startWithOpenBrace = BooleanType.GetInstance(startWithOpenBraceRegex.IsMatch(this.text));
		}
		return startWithOpenBrace.ToBoolean();
	    }
	}

	private static Regex isReturnRegex = new Regex(@"^\s*return\s+");
	private BooleanType isReturn = BooleanType.DEFAULT;
	public bool IsReturn{
	    get {
		if (!isReturn.IsValid) {
		    isReturn = BooleanType.GetInstance(isReturnRegex.IsMatch(this.text));
		}
		return isReturn.ToBoolean();
	    }
	}

	private static Regex isLockRegex = new Regex(@"(^\s*|\}+)lock\s*\(");
	private BooleanType isLock = BooleanType.DEFAULT;
	public bool IsLock{
	    get {
		if (!isLock.IsValid) {
		    isLock = BooleanType.GetInstance(isLockRegex.IsMatch(this.text));
		}
		return isLock.ToBoolean();
	    }
	}

	private static Regex endWithBraceAndCommentRegex = new Regex(@"\{\s*(\/\/|$)");
	private BooleanType endWithBraceAndComment = BooleanType.DEFAULT;
	public bool EndWithBraceAndComment{
	    get {
		if (!endWithBraceAndComment.IsValid) {
		    endWithBraceAndComment = BooleanType.GetInstance(endWithBraceAndCommentRegex.IsMatch(text));
		}

		return endWithBraceAndComment.ToBoolean();
	    }
	}

	private static Regex endWithSemicolonRegex = new Regex(@"\;\s*(\/+|$)");
	private BooleanType endWithSemicolon = BooleanType.DEFAULT;
	public bool EndWithSemicolon{
	    get {
		if (!endWithSemicolon.IsValid) {
		    endWithSemicolon = BooleanType.GetInstance(endWithSemicolonRegex.IsMatch(this.text));
		}
		return endWithSemicolon.ToBoolean();
	    }
	}

	private static Regex isClosingBraceRegex = new Regex(@"^\s*\}\s*(\;|\/\/|$)");
	private BooleanType isClosingBrace = BooleanType.DEFAULT;
	/// <summary>
	/// Check to see if the line is:
	///   }
	///   } // comment
	///   };
	/// </summary>
	public bool IsClosingBrace{
	    get {
		if (!isClosingBrace.IsValid) {
		    isClosingBrace = BooleanType.GetInstance(isClosingBraceRegex.IsMatch(text));
		}
		return isClosingBrace.ToBoolean();
	    }
	}

	private static Regex isSwitchClosingBraceRegex = new Regex(@"^\s*\}\s*\;(\/\/|$)");
	private BooleanType isSwitchClosingBrace = BooleanType.DEFAULT;
	public bool IsSwitchClosingBrace{
	    get {
		if (!isSwitchClosingBrace.IsValid) {
		    isSwitchClosingBrace = BooleanType.GetInstance(isSwitchClosingBraceRegex.IsMatch(this.text));
		}
		return isSwitchClosingBrace.ToBoolean();
	    }
	}

	private static Regex isBreakRegex = new Regex(@"^\s*break\s*\;");
	private BooleanType isBreak = BooleanType.DEFAULT;
	public bool IsBreak{
	    get {
		if (!isBreak.IsValid) {
		    isBreak = BooleanType.GetInstance(isBreakRegex.IsMatch(this.text));
		}
		return isBreak.ToBoolean();
	    }
	}

	private static Regex isSwitchDefaultRegex = new Regex(@"^\s*default\s*:\s*$");
	private BooleanType isSwitchDefault = BooleanType.DEFAULT;
	public bool IsSwitchDefault{
	    get {
		if (!isSwitchDefault.IsValid) {
		    isSwitchDefault = BooleanType.GetInstance(isSwitchDefaultRegex.IsMatch(this.text));
		}
		return isSwitchDefault.ToBoolean();
	    }
	}

	private static Regex endWithContinuationOperatorRegex = new Regex(@"(\+\s*$|,\s*$|&&\s*$|\|\|\s*$|\(\s*$|\*\s*$)");
	private BooleanType endWithContinuationOperator = BooleanType.DEFAULT;
	/// <summary>
	/// Ends with something that would mean the next line is a continuation (+/,/&&/||/()
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public bool EndWithContinuationOperator{
	    get {
		if (!endWithContinuationOperator.IsValid) {
		    endWithContinuationOperator = BooleanType.GetInstance(endWithContinuationOperatorRegex.IsMatch(this.text));
		}
		return endWithContinuationOperator.ToBoolean();
	    }
	}

	private static Regex startWithContinuationOperatorRegex = new Regex(@"(^\s*\*|^\s*\+|^\s*,|^\s*&&|^\s*\|\|)");
	private BooleanType startWithContinuationOperator = BooleanType.DEFAULT;
	/// <summary>
	/// Ends with something that would mean the next line is a continuation (+/,/&&/||)
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public bool StartWithContinuationOperator{
	    get {
		if (!startWithContinuationOperator.IsValid) {
		    startWithContinuationOperator = BooleanType.GetInstance(startWithContinuationOperatorRegex.IsMatch(this.text));
		}
		return startWithContinuationOperator.ToBoolean();
	    }
	}

	private static Regex isSingleLineGetRegex = new Regex(@"(^\s*)get\s*{*\s*\w+");
	private BooleanType isSingleLineGet = BooleanType.DEFAULT;
	public bool IsSingleLineGet{
	    get {
		if (!isSingleLineGet.IsValid) {
		    isSingleLineGet = BooleanType.GetInstance(isSingleLineGetRegex.IsMatch(this.text));
		}
		return isSingleLineGet.ToBoolean();
	    }
	}

	private static Regex isSingleLineSetRegex = new Regex(@"(^\s*)set\s*{*\s*\w+");
	private BooleanType isSingleLineSet = BooleanType.DEFAULT;
	public bool IsSingleLineSet{
	    get {
		if (!isSingleLineSet.IsValid) {
		    isSingleLineSet = BooleanType.GetInstance(isSingleLineSetRegex.IsMatch(this.text));
		}
		return isSingleLineSet.ToBoolean();
	    }
	}

	private static Regex isSingleLineGetWithCommentRegex = new Regex(@"(^\s*)get\s*{*\s*\w+");
	private BooleanType isSingleLineGetWithComment = BooleanType.DEFAULT;
	public bool IsSingleLineGetWithComment{
	    get {
		if (!isSingleLineGetWithComment.IsValid) {
		    isSingleLineGetWithComment = BooleanType.GetInstance(isSingleLineGetWithCommentRegex.IsMatch(this.text));
		}
		return isSingleLineGetWithComment.ToBoolean();
	    }
	}

	private static Regex isSingleLineSetWithCommentRegex = new Regex(@"(^\s*)set\s*{*\s*\w+");
	private BooleanType isSingleLineSetWithComment = BooleanType.DEFAULT;
	public bool IsSingleLineSetWithComment{
	    get {
		if (!isSingleLineSetWithComment.IsValid) {
		    isSingleLineSetWithComment = BooleanType.GetInstance(isSingleLineSetWithCommentRegex.IsMatch(this.text));
		}
		return isSingleLineSetWithComment.ToBoolean();
	    }
	}

	private BooleanType startsWithClosingBrace = BooleanType.DEFAULT;
    	public bool StartsWithClosingBrace {
	    get {
		if (!startsWithClosingBrace.IsValid) {
		    startsWithClosingBrace = BooleanType.GetInstance(text.StartsWith("}"));
		}
		return startsWithClosingBrace.ToBoolean();
	    }
	}

    }
}