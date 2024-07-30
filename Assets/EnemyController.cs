using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private CharacterController _characterController;
    public GameObject target;
    [SerializeField] private float _gravity = .1f;
    [SerializeField] private float speed = 7.0f;
    public float _yVelocity;

    [SerializeField] private float timer = 5;
    private float bulletTime;

    public GameObject bulletObject;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        target = GameObject.Find("Player");
        bulletTime = timer; 
    }

    void FixedUpdate()
    {
       
        //print(Physics.Raycast(transform.position, -Vector3.up, 1.5f));
        if (_characterController.isGrounded || Physics.Raycast(transform.position, -Vector3.up, 1.5f))
        {
            if (_yVelocity < 0)
            {
                _yVelocity = _gravity;
            }
        }
        else if (_yVelocity > -3f)
        {
            _yVelocity -= _gravity;
        }
        var dist = Vector3.Distance(transform.position, target.transform.position);
        var movevec = new Vector3();
        if (dist < 20)
        {
            shootAtPlayer();
            var look = target.transform.position;
            look.y = transform.position.y;
            transform.LookAt(look);
            movevec = (target.transform.position - transform.position).normalized * speed;
        }
        movevec.y = _yVelocity;
        _characterController.Move(movevec * Time.deltaTime);
    }


    void shootAtPlayer()
    {  
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;
        bulletTime = timer;
        GameObject bulletObj = Instantiate(bulletObject, spawnPoint.position,spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * 3000);
        Destroy(bulletObj, 3f);
    }
}
