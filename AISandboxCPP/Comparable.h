#ifndef COMPARABLE_H
#define COMPARABLE_H

namespace Base {
	template<typename T>
	class Comparable {
		public:
			virtual bool equals(T obj) = 0;
			virtual int compareTo(T obj) = 0;
	};
}

#endif

