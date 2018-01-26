using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Rigidbody m_bullet;
    public float m_force = 50;

    private void Awake()
    {
        
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 150.0f;

        transform.Rotate(0, 0, z);
        transform.Rotate(0, x, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        //Vector3 _dir = new Vector3();
        //_dir.x = bullet.transform.*100
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * m_force,ForceMode.Impulse);
        //m_bullet.velocity = Vector3.right * 100;
        Debug.Log(bullet.transform.position);
        NetworkServer.Spawn(bullet);

        Destroy(bullet, 10.0f);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}