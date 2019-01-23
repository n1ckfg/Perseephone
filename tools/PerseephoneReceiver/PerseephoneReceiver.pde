import oscP5.*;
import netP5.*;

OscP5 oscP5;
String ipNumber = "127.0.0.1";
int receivePort = 7110;
int sendPort = 7111;
NetAddress myRemoteLocation;

String[] osceletonNames = {
  "head", "neck", "torso", "r_shoulder", "r_elbow", "r_hand", "l_shoulder", "l_elbow", "l_hand", "r_hip", "r_knee", "r_foot", "l_hip", "l_knee", "l_foot"
};

ArrayList<Joint> joints = new ArrayList<Joint>();

void setup() {
  size(800, 600, P3D);
  pixelDensity(displayDensity());
  oscP5 = new OscP5(this, receivePort);
  myRemoteLocation = new NetAddress(ipNumber, sendPort);

  for (int i=0; i<osceletonNames.length; i++) {
    joints.add(new Joint(osceletonNames[i], 0, 0, 0, 0));
  }
}

void draw() {
  background(0);
  for (int i=0; i<joints.size(); i++) {
    Joint joint = joints.get(i);
    pushMatrix();
    translate(joint.co.x, joint.co.y, joint.co.z);
    ellipse(0,0,10,10);
    popMatrix();
  }
}

void oscEvent(OscMessage msg) {
  if (msg.checkAddrPattern("/joint") && msg.checkTypetag("sifff")) {
    for (int i=0;i<osceletonNames.length;i++) {
      if (msg.get(0).stringValue().equals(osceletonNames[i])) {
        Joint joint = joints.get(i);
        joint.co.x = msg.get(2).floatValue();
        joint.co.y = msg.get(3).floatValue();
        joint.co.z = msg.get(4).floatValue();
      }
    }
  }
}
