using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class KnowledgebasesArticleTextData : Spring2.Core.DataObject.DataObject
    {

	private IdType knowledgebasesArticlesTextID = IdType.DEFAULT;
	private IntegerType knowledgebasesSectionsArticlesID = IntegerType.DEFAULT;
	private IntegerType knowledgebasesID = IntegerType.DEFAULT;
	private StringType text = StringType.DEFAULT;
	private DateType startDate = DateType.DEFAULT;
	private DateType endDate = DateType.DEFAULT;
	private StringType keywords = StringType.DEFAULT;

	public static readonly String KNOWLEDGEBASESARTICLESTEXTID = "KnowledgebasesArticlesTextID";
	public static readonly String KNOWLEDGEBASESSECTIONSARTICLESID = "KnowledgebasesSectionsArticlesID";
	public static readonly String KNOWLEDGEBASESID = "KnowledgebasesID";
	public static readonly String TEXT = "Text";
	public static readonly String STARTDATE = "StartDate";
	public static readonly String ENDDATE = "EndDate";
	public static readonly String KEYWORDS = "Keywords";

	public IdType KnowledgebasesArticlesTextID
	{
	    get
	    {
		return this.knowledgebasesArticlesTextID;
	    }
	    set
	    {
		this.knowledgebasesArticlesTextID = value;
	    }
	}

	public IntegerType KnowledgebasesSectionsArticlesID
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

	public StringType Text
	{
	    get
	    {
		return this.text;
	    }
	    set
	    {
		this.text = value;
	    }
	}

	public DateType StartDate
	{
	    get
	    {
		return this.startDate;
	    }
	    set
	    {
		this.startDate = value;
	    }
	}

	public DateType EndDate
	{
	    get
	    {
		return this.endDate;
	    }
	    set
	    {
		this.endDate = value;
	    }
	}

	public StringType Keywords
	{
	    get
	    {
		return this.keywords;
	    }
	    set
	    {
		this.keywords = value;
	    }
	}
    }
}
