import oscP5.*;
import netP5.*;

OscP5 oscP5;
int receivePort = 7110;

String[] osceletonNames = {
  "head", "neck", "torso", "r_shoulder", "r_elbow", "r_hand", "l_shoulder", "l_elbow", "l_hand", "r_hip", "r_knee", "r_foot", "l_hip", "l_knee", "l_foot"
};

ArrayList<Joint> joints = new ArrayList<Joint>();
float scaler = 100.0;

void setup() {
  size(800, 600, P3D);
  pixelDensity(displayDensity());
  oscP5 = new OscP5(this, receivePort);

  for (int i=0; i<osceletonNames.length; i++) {
    joints.add(new Joint(osceletonNames[i], 0, 0, 0, 0));
  }
}

void draw() {
  background(0);
  for (int i=0; i<joints.size(); i++) {
    Joint joint = joints.get(i);
    pushMatrix();
    translate(joint.co.x, joint.co.y, 0);//joint.co.z);
    ellipse(0,0,20,20);
    popMatrix();
  }
}

void oscEvent(OscMessage msg) {
  println(msg);
  if (msg.checkAddrPattern("/joint") && msg.checkTypetag("sifff")) {
    for (int i=0;i<osceletonNames.length;i++) {
      if (msg.get(0).stringValue().equals(osceletonNames[i])) {
        Joint joint = joints.get(i);
        float x = msg.get(2).floatValue();
        float y = msg.get(3).floatValue();
        float z = msg.get(4).floatValue();
        joint.co = new PVector(x, y, z).mult(scaler);
        joint.co.x += width/2;
        println(joint.co);
      }
    }
  }
}
