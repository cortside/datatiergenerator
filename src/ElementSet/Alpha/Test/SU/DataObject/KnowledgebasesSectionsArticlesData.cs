using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class KnowledgebasesSectionsArticlesData : Spring2.Core.DataObject.DataObject
    {

	private IdType knowledgebasesSectionsArticlesID = IdType.DEFAULT;
	private StringType title = StringType.DEFAULT;
	private DateType dateStart = DateType.DEFAULT;
	private DateType dateEnd = DateType.DEFAULT;
	private IntegerType parentID = IntegerType.DEFAULT;
	private IntegerType knowledgebasesID = IntegerType.DEFAULT;
	private BooleanType hasChild = BooleanType.DEFAULT;
	private IntegerType prevKnowledgebasesSectionsID = IntegerType.DEFAULT;
	private IntegerType nextKnowledgebasesSectionsID = IntegerType.DEFAULT;
	private BooleanType listInParentArticle = BooleanType.DEFAULT;
	private BooleanType includeSummaryinParent = BooleanType.DEFAULT;
	private IntegerType sort = IntegerType.DEFAULT;
	private BooleanType isSection = BooleanType.DEFAULT;
	private BooleanType isTemp = BooleanType.DEFAULT;

	public static readonly String KNOWLEDGEBASESSECTIONSARTICLESID = "KnowledgebasesSectionsArticlesID";
	public static readonly String TITLE = "Title";
	public static readonly String DATESTART = "DateStart";
	public static readonly String DATEEND = "DateEnd";
	public static readonly String PARENTID = "ParentID";
	public static readonly String KNOWLEDGEBASESID = "KnowledgebasesID";
	public static readonly String HASCHILD = "HasChild";
	public static readonly String PREVKNOWLEDGEBASESSECTIONSID = "PrevKnowledgebasesSectionsID";
	public static readonly String NEXTKNOWLEDGEBASESSECTIONSID = "NextKnowledgebasesSectionsID";
	public static readonly String LISTINPARENTARTICLE = "ListInParentArticle";
	public static readonly String INCLUDESUMMARYINPARENT = "IncludeSummaryinParent";
	public static readonly String SORT = "Sort";
	public static readonly String ISSECTION = "IsSection";
	public static readonly String ISTEMP = "IsTemp";

	public IdType KnowledgebasesSectionsArticlesID
	{
	    get
	    {
		return this.knowledgebasesSectionsArticlesID;
	    }
	    set
	    {
		this.knowledgebasesSectionsArticlesID = value;
	    }
	}

	public StringType Title
	{
	    get
	    {
		return this.title;
	    }
	    set
	    {
		this.title = value;
	    }
	}

	public DateType DateStart
	{
	    get
	    {
		return this.dateStart;
	    }
	    set
	    {
		this.dateStart = value;
	    }
	}

	public DateType DateEnd
	{
	    get
	    {
		return this.dateEnd;
	    }
	    set
	    {
		this.dateEnd = value;
	    }
	}

	public IntegerType ParentID
	{
	    get
	    {
		return this.parentID;
	    }
	    set
	    {
		this.parentID = value;
	    }
	}

	public IntegerType KnowledgebasesID
	{
	    get
	    {
		return this.knowledgebasesID;
	    }
	    set
	    {
		this.knowledgebasesID = value;
	    }
	}

	public BooleanType HasChild
	{
	    get
	    {
		return this.hasChild;
	    }
	    set
	    {
		this.hasChild = value;
	    }
	}

	public IntegerType PrevKnowledgebasesSectionsID
	{
	    get
	    {
		return this.prevKnowledgebasesSectionsID;
	    }
	    set
	    {
		this.prevKnowledgebasesSectionsID = value;
	    }
	}

	public IntegerType NextKnowledgebasesSectionsID
	{
	    get
	    {
		return this.nextKnowledgebasesSectionsID;
	    }
	    set
	    {
		this.nextKnowledgebasesSectionsID = value;
	    }
	}

	public BooleanType ListInParentArticle
	{
	    get
	    {
		return this.listInParentArticle;
	    }
	    set
	    {
		this.listInParentArticle = value;
	    }
	}

	public BooleanType IncludeSummaryinParent
	{
	    get
	    {
		return this.includeSummaryinParent;
	    }
	    set
	    {
		this.includeSummaryinParent = value;
	    }
	}

	public IntegerType Sort
	{
	    get
	    {
		return this.sort;
	    }
	    set
	    {
		this.sort = value;
	    }
	}

	public BooleanType IsSection
	{
	    get
	    {
		return this.isSection;
	    }
	    set
	    {
		this.isSection = value;
	    }
	}

	public BooleanType IsTemp
	{
	    get
	    {
		return this.isTemp;
	    }
	    set
	    {
		this.isTemp = value;
	    }
	}
    }
}
