using System;

namespace Spring2.DataTierGenerator {
	public class Entity {

		private String name;
		private String sqlObject;

		public Entity() {
		}

		public String Name {
			get { return name; }
			set { this.name = value; }
		}

		public String SqlObject {
			get { return sqlObject; }
			set { this.sqlObject = value; }
		}

	}
}
