using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {
	public class Entity : Spring2.Core.DataObject.DataObject {

		protected String name = String.Empty;
		protected String sqlObject = String.Empty;
		protected String sqlView = String.Empty;
		protected ArrayList fields = new ArrayList();

		public String Name {
			get { return this.name; }
			set { this.name = value; }
		}

		public String SqlObject {
			get { return this.sqlObject; }
			set { this.sqlObject = value; }
		}

		public String SqlView {
			get { return this.sqlView; }
			set { this.sqlView = value; }
		}

		public ArrayList Fields {
			get { return this.fields; }
			set { this.fields = value; }
		}


		public String ToXml() {
			StringBuilder sb = new StringBuilder();
			sb.Append("<entity name=\"").Append(name).Append("\"");
			sb.Append(" sqlobject=\"").Append(sqlObject).Append("\"");
			sb.Append(" sqlview=\"").Append(sqlView).Append("\"");
			sb.Append(" />");

			return sb.ToString();
		}


		public static Entity FindEntityBySqlObject(ArrayList entities, String sqlObject) {
			foreach (Entity entity in entities) {
				if (entity.SqlObject == sqlObject) {
					return entity;
				}
			}
			return null;
		}

		public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types) {
			ArrayList entities = new ArrayList();
			XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("entity");
			foreach (XmlNode node in elements) {
				Entity entity = new Entity();
				entity.Name = node.Attributes["name"].Value;
				if (node.Attributes["sqlobject"] != null) {
					entity.SqlObject = node.Attributes["sqlobject"].Value;
					if (options.UseViews) {
						entity.SqlView = "vw" + entity.SqlObject;
					}
				}
				if (node.Attributes["sqlview"] != null) {
					entity.SqlView = node.Attributes["sqlview"].Value;
				}
				entity.Fields = Field.ParseFromXml(doc, entity, sqltypes, types);
				entities.Add(entity);
			}
			return entities;
		}

		public Boolean HasUpdatableFields() {
			Boolean has = false;
			foreach (Field field in fields) {
				if (!field.IsIdentity && !field.IsPrimaryKey && !field.IsRowGuidCol) {
					has=true;	
				}
			}
			return has;
		}

	}
}
