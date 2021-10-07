using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingObject : MonoBehaviour
{
    public float speed;

    private Vector3 vector;

    private bool isMove = true;

    private Animator animator;
    private GameObject[,] fow;
    private bool[,] is_visited;

    private int cur_x;
    private int cur_y;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] tmp_gameobject;
        is_visited = new bool[17, 17];
        animator = GetComponent<Animator>();
        fow = new GameObject[17, 17];
        tmp_gameobject = GameObject.FindGameObjectsWithTag("FOW");
        for (int i = 0; i < tmp_gameobject.Length; i++)
        {
            int tmp_x;
            int tmp_y;
            tmp_x = (int)tmp_gameobject[i].transform.position.x;
            tmp_y = (int)tmp_gameobject[i].transform.position.y;
            SpriteRenderer spr = tmp_gameobject[i].GetComponent<SpriteRenderer>();
            Color color = spr.color;
            color.a = 1f;
            spr.color = color;
            fow[tmp_x, tmp_y] = tmp_gameobject[i];
            is_visited[tmp_x, tmp_y] = false;
        }
        cur_x = (int)Math.Round(transform.position.x);
        cur_y = (int)Math.Round(transform.position.y);
        for (int i = cur_x - 1; i <= cur_x + 1; i++)
        {
            if (i < 0 || i >= 17)
                continue;
            for (int j = cur_y - 1; j <= cur_y + 1; j++)
            {
                if (j < 0 || j >= 17)
                    continue;
                SpriteRenderer spr = fow[i, j].GetComponent<SpriteRenderer>();
                Color color = spr.color;
                color.a = 0f;
                is_visited[i, j] = true;
                spr.color = color;
            }
        }
    }


    IEnumerator MoveCoroutine()
    {
        int next_x;
        int next_y;
        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

        animator.SetFloat("DirX", vector.x);
        animator.SetFloat("DirY", vector.y);
        animator.SetBool("Walking", true);

        Vector2 cur_dir = transform.position;
        Vector2 next_dir = cur_dir + new Vector2(vector.x * speed, vector.y * speed);
        next_x = (int)Math.Round(next_dir.x);
        next_y = (int)Math.Round(next_dir.y);
        if (next_x != cur_x || next_y != cur_y)
        {
            cur_x = next_x;
            cur_y = next_y;
            for (int i=cur_x-2; i<=cur_x+2; i++)
            {
                if (i < 0 || i >= 17)
                    continue;
                for (int j=cur_y-2; j<=cur_y+2; j++)
                {
                    if (j < 0 || j >= 17)
                        continue;
                    SpriteRenderer spr = fow[i, j].GetComponent<SpriteRenderer>();
                    Color color = spr.color;
                    if (cur_x - 2 < i && i < cur_x + 2 && cur_y - 2 < j && j < cur_y + 2)
                    {
                        color.a = 0f;
                        is_visited[i, j] = true;
                    }
                    else if (is_visited[i, j])
                        color.a = 0.4f;
                    else
                        color.a = 1f;
                    spr.color = color;
                }
            }
        }
        transform.position = next_dir;
        yield return new WaitForSeconds(0.01f);
        isMove = true;
        animator.SetBool("Walking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                isMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
