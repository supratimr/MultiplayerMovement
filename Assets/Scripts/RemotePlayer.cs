using UnityEngine;

public class RemotePlayer : MonoBehaviour
{
    private Rigidbody mRigidBody;

    private void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
    }

    public void UpdatePosition(float x, float y, float z)
    {
        Vector3 displacement = new Vector3(x, y, z);
        //Debug.Log("RP Displacement :: " + displacement );
        transform.position = displacement;
        //Debug.Log("RP New Pos:: " + transform.position);
    }

    public void UpdatePosition(short? x, short? y, short? z)
    {
        float xPos = x.HasValue ? Utility.FromByte(x.Value) : transform.position.x;
        float yPos = y.HasValue ? Utility.FromByte(y.Value): transform.position.y;
        float zPos = z.HasValue ? Utility.FromByte(z.Value) : transform.position.z; 

        Vector3 displacement = new Vector3(xPos, yPos, zPos);
        Debug.Log("RP Displacement :: " + displacement);
        mRigidBody.MovePosition(displacement);
        //Debug.Log("RP New Pos:: " + transform.position);
    }
}
