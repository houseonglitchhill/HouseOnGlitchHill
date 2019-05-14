using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
	public Light flashlight;
    public float moveSpeed;
    public float jumpForce;
    public float cameraSpeed;
    public float jumpRate;
    public float maxPitch;
    public float minPitch;
    public bool reversed;

    [SerializeField]    // so we can see the private field below
    private bool hasMasterKey;

    private Vector3 jump;
    private Rigidbody rb;
    private float nextJump = 0;
    private float yaw = -16.0f;
    private float pitch = 0.0f;

    private GameController gc;

    // Use this for initialization
    void Start()
    {
        gc = GameController.FindObjectOfType<GameController>();

        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

        mainCamera = GetComponentInChildren<Camera>();

        hasMasterKey = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float v, h;
        if (reversed) {
            v = Input.GetAxis("Horizontal");
            h = Input.GetAxis("Vertical");
        } else {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }

        Vector3 movement = transform.forward * v * moveSpeed * Time.deltaTime;
        Vector3 sidestep = transform.right * h * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement + sidestep);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextJump)
        {
            nextJump = Time.time + jumpRate;
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }

        RaycastHit hit;
        Vector3 f = mainCamera.transform.forward;

        Debug.DrawRay(transform.position, f, Color.blue);

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            //Debug.Log("Interacting");
            if (hit.collider.tag == "Door" && (Input.GetKeyDown(KeyCode.E)))
            {
                hit.collider.GetComponent<Door>().OpenOrCloseDoor();
            }
        }
    }

    private void Update()
    {
        yaw += cameraSpeed * Input.GetAxis("Mouse X");
        pitch -= cameraSpeed * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        //Pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gc.PauseGame();
        }

		//Use flashlight
		if (Input.GetKeyDown (KeyCode.F)) 
		{
			flashlight.enabled = !flashlight.enabled;
		}
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player - other Collider: " + other.tag);
        if (other.tag == "Key")
        {
            gc.GrabKey();
        }
    }

    public bool HasMasterKey
    {
        get; set;
    }
}