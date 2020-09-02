#ifndef MAP_H
#define MAP_H

namespace Maps {
	class Map {
		public:
			Map();
			Map(int width, int length);
			Map(int width, int length, int height);
			virtual ~Map();
			virtual int getWidth();
			virtual void setWidth(int width);
			virtual int getLength();
			virtual void setLength(int length);
			virtual int getHeight();
			virtual void setHeight(int height);
			virtual void loadMap(char* filename);
			virtual void displayMap();
		private:
			// X
			int width;
			// Y
			int length;
			// Z
			int height;
	};
}

#endif
