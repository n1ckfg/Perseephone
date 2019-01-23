# Perseephone

Send Osceleton format skeleton data from Orbbec Persee over OSC.

<pre>
Joint names (15) are:
"head", "neck", "torso", "r_shoulder", "r_elbow", "r_hand", "l_shoulder", "l_elbow", "l_hand", "r_hip", "r_knee", "r_foot", "l_hip", "l_knee", "l_foot"

Channel name is always:
"/joint"

Message pattern is:
sifff

Message format is:
1. joint name (string)
2. skeleton index (int)
3. x (float, 0.0-1.0)
4. y (float, 0.0-1.0)
5. z (float, 0.0-1.0)
</pre>
