#include "Node.h"

using namespace Nodes;

Node::Node() {
	this->x = 0;
	this->y = 0;
	this->z = 0;
	this->value = 0;
}

Node::Node(int x, int y) {
	this->x = x;
	this->y = y;
	this->z = 0;
	this->value = 0;
}

Node::Node(int x, int y, int z) : Node::Node(x, y) {
	this->z = z;
}

Node::Node(int x, int y, double value) : Node::Node(x, y) {
	this->value = value;
}

Node::Node(int x, int y, int z, double value) : Node::Node(x, y, z) {
	this->value = value;
}

Node::~Node() {

}

int Node::getX() {
	return x;
}

void Node::setX(int x) {
	this->x = x;
}

int Node::getY() {
	return y;
}

void Node::setY(int y) {
	this->y = y;
}

int Node::getZ() {
	return z;
}

void Node::setZ(int Z) {
	this->z = z;
}

double Node::getValue() {
	return value;
}

void Node::setValue(double value) {
	this->value = value;
}

// Compares position values
template<typename T>
bool Node::equals(T obj) {
	if (obj == null) {
		return false;
	}

	// Attempt cast to same obj type
	Node* n = dynamic_cast<Node*> &obj;

	// If same obj type, compare values
	if (n != 0) {
		if (n->getX() == x && n->getY() == y && n->getZ() == z) {
			return true;
		}
	}

	// Otherwise, false
	return false;
}

// Compares location values
template<typename T>
int Node::compareTo(T obj) {
	if (obj == null) {
		return -1;
	}

	// Attempt cast to same obj type
	Node* n = dynamic_cast<Node*> & obj;

	// If same obj type, compare values
	if (n != 0) {
		if (n->getValue() == value) {
			return true;
		}
	}
	
	// Otherwise, false
	return false;
}