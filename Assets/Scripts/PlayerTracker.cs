using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTracker : MonoBehaviour {

    public Transform player;
    public float height;
    public int playerNumber = 1;
    public Color trackerColor = Color.white;
    public Text trackerText;
    private SpriteRenderer trackerArrow;
    private SpriteRenderer tracker;

    public Transform DeadSprite;

    public float killTime = 0.5f;
    public Image timerUI;
    public Transform ArrowPivot;
    private CharacterMotor cm;

	// Use this for initialization
	void Start () {
        cm = player.GetComponent<CharacterMotor>();
        trackerArrow = ArrowPivot.GetComponentInChildren<SpriteRenderer>();
        tracker = GetComponent<SpriteRenderer>();
        trackerArrow.color = trackerColor;
        tracker.color = trackerColor;
        trackerText.text = "P" + playerNumber;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(player != null){
            Vector3 v3Screen = Camera.main.WorldToViewportPoint(player.transform.position);
            if (v3Screen.x > -0.01f && v3Screen.x < 1.01f && v3Screen.y > -0.01f && v3Screen.y < 1.01f)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position + (player.transform.up * height), Time.deltaTime * 500f);
                ArrowPivot.rotation = Quaternion.Slerp(ArrowPivot.rotation, Quaternion.FromToRotation(Vector2.up, player.transform.up), Time.deltaTime * 75f);
            }
            else
            {
                v3Screen.x = Mathf.Clamp(v3Screen.x, 0.05f, 0.95f);
                v3Screen.y = Mathf.Clamp(v3Screen.y, 0.05f, 0.95f);
                transform.position = Vector3.Lerp(transform.position, Camera.main.ViewportToWorldPoint(v3Screen), Time.deltaTime * 500f);
                ArrowPivot.rotation = Quaternion.Slerp(ArrowPivot.rotation, Quaternion.FromToRotation(Vector2.up, transform.position - player.position), Time.deltaTime * 75f);
            }
        }
	}
    float timer = 0;
    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(player.position, cm.gravityPoint.position) > 10f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
            }

            if (timer > killTime)
            {
                StartCoroutine(KillPlayer());
            }
        }
        timerUI.fillAmount = timer / killTime;
    }

    IEnumerator KillPlayer()
    {
        Destroy(player.gameObject);
        DeadSprite.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        
    }
}
