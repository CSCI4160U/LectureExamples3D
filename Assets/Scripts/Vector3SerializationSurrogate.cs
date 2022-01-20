using UnityEngine;
using System.Runtime.Serialization;

public class Vector3SerializationSurrogate : ISerializationSurrogate {
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {
        Vector3 vec = (Vector3)obj;
        info.AddValue("x", vec.x);
        info.AddValue("y", vec.y);
        info.AddValue("z", vec.z);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {
        Vector3 vec = (Vector3)obj;

        vec.x = (float)(info.GetValue("x", typeof(float)));
        vec.y = (float)(info.GetValue("y", typeof(float)));
        vec.z = (float)(info.GetValue("z", typeof(float)));

        obj = vec; // point object to our new vector
        return obj; 
    }
}
