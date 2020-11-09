using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_Speed = default;

    [SerializeField] private string m_HorizontalAxis = string.Empty;
    [SerializeField] private string m_VerticalAxis = string.Empty;

    [SerializeField] private RemotePlayer m_RemotePlayer = default;
    [SerializeField] private Vector3 m_FollowerOffset = default;

    [Tooltip ("In Seconds")]
    [SerializeField] private float m_PositionUpateInterval = 0.2f;

    private Rigidbody mRigidBody;
    private bool mInitialized;

    private float mTimer;

    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
        mInitialized = true;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if(mInitialized)
        {
            float horizontal = Input.GetAxisRaw(m_HorizontalAxis);
            float vertical = Input.GetAxisRaw(m_VerticalAxis);

            if (Mathf.Abs(horizontal) <= 0f && Mathf.Abs(vertical) <= 0f)
                mTimer = 0f;
            else
                mTimer += Time.deltaTime;

            Vector3 moved = new Vector3(horizontal, 0, vertical);
            moved = moved.normalized * m_Speed * Time.deltaTime;
            mRigidBody.MovePosition(transform.position + moved);

            if (mTimer >= m_PositionUpateInterval)
            {
                Vector3 sentPosition = transform.position + m_FollowerOffset;

                Debug.Log("Send Position original:: " + sentPosition + " ::Data size float:: " + sizeof(float) * 3 * 8);

                // In any axis, if value is changed send the data else send null.
                short? xPos = null;
                short? yPos = null;
                short? zPos = null;

                int dataCount = 0;

                if(Mathf.Abs(moved.x) > 0f)
                {
                    xPos = Utility.ToByte((float)(System.Math.Round((double)sentPosition.x, 1)));
                    dataCount += 1;
                }
                    

                if(Mathf.Abs(moved.y) > 0f)
                {
                    yPos = Utility.ToByte((float)(System.Math.Round((double)sentPosition.x, 1)));
                    dataCount += 1;
                }
                    

                if(Mathf.Abs(moved.z) > 0f)
                {
                    zPos = Utility.ToByte((float)(System.Math.Round((double)sentPosition.z, 1)));
                    dataCount += 1;
                }

                Debug.Log("Send Position optimized:: " + xPos + ":" + yPos + ":" + zPos + " ::Data size short:: " + sizeof(short) * dataCount * 8);

                m_RemotePlayer.UpdatePosition(xPos, yPos, zPos);

                mTimer = 0f;
            }
        }
    }
}

