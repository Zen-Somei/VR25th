using System.Collections;

using UnityEngine;

using Leap;
using Leap.Unity;


public class AUTDcollision: MonoBehaviour
{

    //public GameObject Target;
    public GameObject wall;
    //private Collision_hand _collision;
    public bool _isCollided = false;
    public AudioClip drumSound;
    private AudioSource audioSource;

    //[SerializeField]
    public GameObject m_ProviderObject;

    LeapServiceProvider m_Provider;


    void Start()
    {
        wall = GameObject.Find("Wall");
        //_collision = wall.GetComponent<Collision_hand>();

        audioSource = gameObject.GetComponent<AudioSource>();

        m_Provider = m_ProviderObject.GetComponent<LeapServiceProvider>();
    }

    void Update()
    {
        Frame frame = m_Provider.CurrentFrame;

        // 右手を取得する
        Hand rightHand = null;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                rightHand = hand;
                break;
            }
        }
        if (rightHand == null)
        {
            return;
        }

        Vector3 position = rightHand.PalmPosition.ToVector3();
        var handPos = position;
        float focusX = handPos.x + 0.01f;
        float focusY = handPos.z + 0.0675f + 0.005f;//handPos.y;
        float focusZ = handPos.y - 0.001f;

        Vector3 focus = new Vector3(focusX, focusY, focusZ);
        //UnityEngine.Debug.Log(focus);

        if (focusZ < 0.25f)
        {
            wall.GetComponent<Renderer>().material.color = UnityEngine.Color.yellow;

            if ((focusZ > 0.23f) && (!_isCollided))
            {

                audioSource.PlayOneShot(drumSound);  //sound
                _isCollided = true;
                //StartCoroutine("MusicPlay");
            }

        }
        else
        {
            wall.GetComponent<Renderer>().material.color = UnityEngine.Color.red;


            if (focusZ > 0.27f)
            {
                _isCollided = false;
            }
            //StartCoroutine("Wait");
        }



    }








}
