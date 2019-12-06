﻿/*****************
* 프로그램명 : Run_Beach.cs

* 작성자 : 천은정 ( 권순규, 김성수, 유병주, 전승원 )

* 작성일 : 2019년 11월 27일

* 프로그램 설명 : 게임 시작 시 오브젝트를 움직이는 코드이다. 좌우 움직임과 충돌 시 뒤로 물러남과 동시에 생명이 하나 깍인다.

***********************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Run_Beach : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 pos;    // 현재위치
    AudioSource audioSource;
    float delta = 0.9f; // 좌우 이동가능한 최대값
    public float speed = 1.0f;    // 이동속도

    public GameObject Timer;        // Timer
    public GameObject BackGround;   // 게임 종료시 실행되는 panel
    public GameObject Panel;        // 문제 뜨는 panel
    public GameObject o;
    public GameObject x;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * speed;     // 앞으로 자동 이동

        if (Input.GetKeyDown(KeyCode.LeftArrow))        // 왼쪽 버튼 클릭 시
        {
            transform.position += Vector3.left * delta;       // 왼쪽 이동
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))       // 오른쪽 버튼 클릭 시
        {
            transform.position += Vector3.right * delta;    // 오른쪽 이동
            audioSource.Play();
        }

    }

    private void OnCollisionEnter(Collision collision)    // 부딪힐 시 뒤로 물러남
    {
        if (collision.transform.tag == "enemy")
        {
            this.transform.Translate(Vector3.back * speed);
            PlayerStats.Instance.TakeDamage();      // 생명이 하나 사라진다.

            if (PlayerStats.Instance.Health == 0)       // 생명이 0이 되면 게임이 끝난다.
            {
                speed = 0;      // 움직임 멈추기
                Timer.gameObject.SetActive(false);     // 오브젝트 비활성화             
                Panel.gameObject.SetActive(false);     // 오브젝트 비활성화
                o.gameObject.SetActive(false);
                x.gameObject.SetActive(false);
                BackGround.gameObject.SetActive(true);     // 오브젝트 활성화

            }
        }
    }
    private void OnTriggerEnter(Collider other)     // Trigger 충돌 발생
    {
        if (other.transform.tag == "question")
        {
            Panel.gameObject.SetActive(true);
        }
        if (other.transform.tag == "check")
        {
            o.gameObject.SetActive(false);
            x.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)      // Trigger 충돌 끝
    {
        if (other.transform.tag == "question")
        {
            Panel.gameObject.SetActive(false);
        }
    }

}
