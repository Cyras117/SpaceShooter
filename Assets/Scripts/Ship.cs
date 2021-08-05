//using System.Threading.Tasks.Dataflow;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{   
    [SerializeField] private Transform cannon;
    [SerializeField] private GameObject[] lasers;
    [SerializeField]private float shootingTime = 0.1f;
    [SerializeField]private Transform spawnPoint;
    [SerializeField]private AudioClip shootSound;
    [SerializeField]private AudioClip deathSound;
    private bool canMove = true;
    private bool canShoot = true;
    private int lives = 3;
    private bool shooting;
    private Animator anim;
    private int laserToshoot = 0;
    public int getLives()
    {
        return lives;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(
            new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        var cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y
        );

        Vector3 position = transform.position;
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        //go right
        if(x > 0)
            anim.SetBool("tRight",true);

        //go left
        if(x < 0)
            anim.SetBool("tLeft",true);

        //reset right left
        if (x==0)
        {
            anim.SetBool("tLeft",false);
            anim.SetBool("tRight",false);
        }

        if(canMove){
            position.x += x * 5.0f * Time.deltaTime;
            position.y += y * 5.0f * Time.deltaTime;
            transform.position = position;
        }
        
        float clampX = Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax);
        float clampY = Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax);

        transform.position = new Vector3(clampX, clampY, transform.position.z);

        //Isso aqui é foda

        if (Input.GetKey(KeyCode.F1))
        {
            if (0 >= lasers.Length) {
                Debug.Log("Missing attache Laser assets to the script");
            }
            else
            {
                laserToshoot = 0;
            }
        }
        if (Input.GetKey(KeyCode.F2)) {
            if (1 > lasers.Length-1)
            {
                laserToshoot = 0;
            }
            else
            {
                laserToshoot = 1;
            }
        }
        if (Input.GetKey(KeyCode.F3))
        {
            if (2 > lasers.Length - 1)
            {
                laserToshoot = 0;
            }
            else
            {
                laserToshoot = 2;
            }
        }
        if (Input.GetKey(KeyCode.F4))
        {
            if (3> lasers.Length - 1)
            {
                laserToshoot = 0;
            }
            else
            {
                laserToshoot = 3;
            }
        }
        if (Input.GetKey(KeyCode.F5))
        {
            if (4 > lasers.Length - 1)
            {
                laserToshoot = 0;
            }
            else
            {
                laserToshoot = 4;
            }
        }
        if (Input.GetKey(KeyCode.F6))
        {
            if (5 > lasers.Length - 1)
            {
                laserToshoot = 0;
            }
            else
            {
                laserToshoot = 5;
            }
        }
        if (Input.GetKey(KeyCode.F7))
        {
            if (6 > lasers.Length - 1)
            {
                laserToshoot = 0;
            }
            else
            {
                laserToshoot = 6;
            }
        }
        if (Input.GetKey(KeyCode.F8))
        {
            if (7 > lasers.Length - 1)
            {
                laserToshoot = 0;
            }
            else
            {
                laserToshoot = 7;
            }
        }
        if (Input.GetKey(KeyCode.F9))
        {
            if (8 > lasers.Length - 1)
            {
                laserToshoot = 0;
            }
            else
            {
                laserToshoot = 8;
            }
        }
        if (Input.GetKey(KeyCode.F11))
        {
            if (9 > lasers.Length - 1)
            {
                laserToshoot = 0;
            }
            else
            {
                laserToshoot = 9;
            }
        }

        if (Input.GetKey(KeyCode.Space))
            Shot(lasers[laserToshoot]);
        
    }

    void Shot (GameObject laser)
    {
        if (shooting || !canShoot) return;
        shooting = true;

        GameObject newShot = Instantiate(laser, cannon.position, cannon.rotation);
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        newShot.TryGetComponent(out Rigidbody2D rb);
        rb.AddForce(Vector3.up * 5, ForceMode2D.Impulse);
        Destroy(newShot, 2);
        StartCoroutine(Cooldown());
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Meteoro"){
            if (lives > 0)
            {
                lives--;
                StartCoroutine(Respawn(other));
            }
            else
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    IEnumerator Respawn(Collision2D other){
        Destroy(other.gameObject);
        canMove = false;
        canShoot = false;
        anim.SetBool("isDead",true);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        yield return new WaitForSeconds(0.8f);
        gameObject.transform.position = spawnPoint.transform.position;
        anim.SetBool("isDead",false);
        canMove = true;
        canShoot = true;
    }


    IEnumerator Cooldown ()
    {
        yield return new WaitForSeconds(shootingTime);
        shooting = false;
    }
}
