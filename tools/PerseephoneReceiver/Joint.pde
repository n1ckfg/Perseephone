class Joint {
  
  String name;
  int id;
  PVector co;
  
  Joint(String _name, int _id, float _x, float _y, float _z) {
    name = _name;
    id = _id;
    co = new PVector(_x, _y, _z);
  }
  
}
