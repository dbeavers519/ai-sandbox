#ifndef NODE_H
#define NODE_H

#include "Comparable.h"

namespace Nodes {
	class Node : virtual Base::Comparable<Node> {
		private:
			int x;
			int y;
			int z;
			double value;

		public:
			Node();
			Node(int x, int y);
			Node(int x, int y, int z);
			Node(int x, int y, double value);
			Node(int x, int y, int z, double value);
			~Node();
			int getX();
			void setX(int x);
			int getY();
			void setY(int y);
			int getZ();
			void setZ(int z);
			double getValue();
			void setValue(double value);
			template<typename T>
			bool equals(T obj);
			template<typename T>
			int compareTo(T obj);
	};
}

#endif