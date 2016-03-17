using UnityEngine;
using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
    public static PlayerController Instance;
    public GameObject PlatformPrefab;
    public Text ScoreTxt;
    public ParticleSystem FlameEffect;
    public ParticleSystem DebrisEffect;
    [Range(30, 60)]
    public float BurstMultiplier;
    [Range(1, 10)]
    public float GroundMultiplier;
    [Range(1, 20)]
    public float SecureHeightBuffer = 1f;
    [Range(200, 500)]
    public float JumpMultiplier;

    private GameObject[] PlatformPool = new GameObject[5];
    private ObscuredInt _Score = 0;
    private bool IsGrounded = false;
    private Rigidbody RigBody;
    private float LastSecureHeight = 1f;

    public int Score
    {
        get
        {
            return _Score;
        }
        set
        {
            _Score = value;
            ScoreTxt.text = value.ToString();
        }
    }

	// Use this for initialization
	void Start () 
    {
        RigBody = GetComponent<Rigidbody>();
        Instance = this;
        for (int i = 0; i < PlatformPool.Length; i++)
        {
            PlatformPool[i] = Instantiate(PlatformPrefab) as GameObject;
            PlatformPool[i].name = "Platform" + i;
            PlatformPrefab.SetActive(false);
        }
	}

    void Update()
    {
        if (transform.position.y < LastSecureHeight - SecureHeightBuffer)
        {
            //Debug.Log(LastSecureHeight + ", " + transform.position.y);
            GameManager.Instance.KillPlayer();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IsGrounded = true;
        if (collision.collider.tag == "Platform" && !collision.transform.parent.GetComponent<PlatformController>().IsTouched)
        {
            AudioManager.Instance.Play(AudioManager.SoundTrack.Blip);
            //GameObject platform = GetAvailablePlatform();
            PlatformController platformController = collision.transform.parent.GetComponent<PlatformController>();
            //int nextPosition = Random.Range(0, platformController.PossiblePositions.Length);
            //Debug.Log(nextPosition);
            //Transform position = platformController.PossiblePositions[nextPosition].transform;
            //Transform position = collision.transform.parent.GetComponent<PlatformController>().PossiblePositions[nextPosition].transform;
            //platform.transform.position = position.position;
            //platform.transform.rotation = position.rotation;
            //platform.SetActive(true);
            platformController.GetComponent<PlatformController>().IsTouched = true;
            Score++;
        }
        else
        {
            if (collision.collider.tag == "Platform")
            {
                AudioManager.Instance.Play(AudioManager.SoundTrack.Blip);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Platform" && !collider.transform.parent.GetComponent<PlatformController>().IsHovered)
        {
            GameObject platform = GetAvailablePlatform();
            platform.GetComponent<Animator>().SetTrigger("Spawn");
            PlatformController platformController = collider.transform.parent.GetComponent<PlatformController>();
            int nextPosition = Random.Range(0, platformController.PossiblePositions.Length);
            Transform position = platformController.PossiblePositions[nextPosition].transform;
            platform.transform.position = position.position;
            platform.transform.rotation = position.rotation;
            platform.SetActive(true);
            platformController.GetComponent<PlatformController>().IsHovered = true;
            LastSecureHeight = collider.transform.position.y;
            //Score++;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        IsGrounded = false;
    }

    GameObject GetAvailablePlatform()
    {
        foreach (var item in PlatformPool)
        {
            if (!item.activeSelf)
            {
                return item;
            }
        }
        GameObject temp = PlatformPool[0];
        temp.GetComponent<PlatformController>().IsTouched = false;
        temp.GetComponent<PlatformController>().IsHovered = false;
        for (int i = 0; i < PlatformPool.Length - 1; i++)
        {
            PlatformPool[i] = PlatformPool[i + 1];
        }
        PlatformPool[PlatformPool.Length - 1] = temp;
        return PlatformPool[PlatformPool.Length - 1];
    }

    public void Burst()
    {
        FlameEffect.Play();
        if (IsGrounded)
        {
            RigBody.AddForce(RigBody.velocity * BurstMultiplier * GroundMultiplier);
        }
        else
        {
            RigBody.AddForce(RigBody.velocity * BurstMultiplier);
        }
        AudioManager.Instance.Play(AudioManager.SoundTrack.Explosion);
    }

    public void Jump()
    {
        //Debug.Log(transform.up);
        RigBody.AddForce(new Vector3(0, 1) * JumpMultiplier);
        DebrisEffect.Play();
        AudioManager.Instance.Play(AudioManager.SoundTrack.Jump);
    }
}
