using System.Collections;
using System.Collections.Generic;

using System;

public class TestClass {

	public int c { get; set; }

	public TestClass(int c) {
		this.c = c;
	}

	public override bool Equals(object obj) {
		var @class = obj as TestClass;
		return @class != null &&
			   c == @class.c;
	}

	public override int GetHashCode() {
		return 1944343330 + c.GetHashCode();
	}

	//public int CompareTo(object obj) {
	//	return c.CompareTo()
	//}
}
