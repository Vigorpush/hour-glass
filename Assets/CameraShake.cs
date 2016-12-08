 using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour {
public bool Shaking;
protected float ShakeDecay;
protected float ShakeIntensity;
protected Vector3 OriginalPos;
protected Quaternion OriginalRot;
void Start()
{
        OriginalRot = transform.rotation;
        Shaking = false;
}
// Update is called once per frame
void Update ()
{
if(ShakeIntensity > 0)
{
transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);
ShakeIntensity -= ShakeDecay;
}
else if (Shaking)
{
Shaking = false;
            transform.rotation = OriginalRot;

}
}
/*void OnGUI() {
if (GUI.Button(new Rect(10, 200, 50, 30), "Shake"))
DoShake();
Debug.Log("Shake");
}*/
public void DoShake()
{
OriginalPos = transform.position;

ShakeIntensity = 0.1f;
ShakeDecay = 0.02f;
Shaking = true;
}
}