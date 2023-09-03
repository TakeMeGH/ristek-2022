using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaking
{
    public int w;
    public int h;
    public float sz;
    Vector3 pos;
    public bool[,] grid;
    int[] baris = {1, -1, 0, 0};
    int[] kolom = {0, 0, 1, -1};  
    bool[,] vis;   

    public GridMaking(int w, int h, float sz, Vector3 pos){
        this.w = w;
        this.h = h; 
        this.sz = sz;
        this.pos = pos;
        this.vis = new bool[w,h];

        grid = new bool[w, h];
        for(int i = 0; i < w; i++){
            for(int j = 0; j < h; j++){
                grid[i,j] = true;
                Debug.DrawLine(getRealPos(i, j), getRealPos(i, j + 1), Color.white, 100f);
                Debug.DrawLine(getRealPos(i, j), getRealPos(i + 1, j), Color.white, 100f);
            }
        }
        Debug.DrawLine(getRealPos(w, 0), getRealPos(w, h), Color.white, 100f);
        Debug.DrawLine(getRealPos(0, h), getRealPos(w, h), Color.white, 100f);
    }

    public Vector3 getRealPos(int x, int y) {
        return new Vector3(x, y) * sz + pos;
    }

    public Vector3 getIdx(Vector3 realPos) {
        int x = (int)((realPos - pos).x / sz);
        int y = (int)((realPos - pos).y / sz);
        return new Vector3(x, y, 0);
    }

    public void setValue(int x, int y, bool val) {
        if (x >= 0 && y >= 0 && x < w && y < h) {
            grid[x, y] = val;
        }
    }

    public void setByRealPos(Vector3 realPos, bool val) {
        Vector3 temp = getIdx(realPos);
        setValue((int)temp.x, (int)temp.y, val);
    }

    public bool getGridVal(int x, int y) {
        if (x >= 0 && y >= 0 && x < w && y < h) {
            return grid[x, y];
        }
        return false;
    }

    public bool getByRealPos(Vector3 realPos) {
        Vector3 temp = getIdx(realPos);
        return getGridVal((int)temp.x, (int)temp.y);
    }

    public Vector3 findNearestTrue(int x, int y){
        for(int i = 0; i < w; i++){
            for(int j = 0; j < h; j++){
                vis[i,j] = false;
            }
        }
        vis[x,y] = true;
        Queue<Node> q = new Queue<Node>();
        q.Enqueue(new Node(x, y));
        while(q.Count >= 1){
            Node cur = q.Dequeue();
            if(getGridVal(cur.x, cur.y)){
                return getRealPos(cur.x, cur.y);
            }

            for(int i = 0; i < 4; i++){
                int nx = cur.x + baris[i];
                int ny = cur.y + kolom[i];
                if(nx >= 0 && nx < w && ny >= 0 && ny <= h && vis[nx,ny] == false){
                    vis[nx,ny] = true;
                    q.Enqueue(new Node(nx, ny));
                }
            }
        }
        return getRealPos(x, y);
    }

    public Vector3 FindNearestByRealPos(Vector3 realPos){
        Vector3 temp = getIdx(realPos);
        return findNearestTrue((int)temp.x, (int)temp.y);
    }

  internal void setValue(object x, object y, bool v)
  {
    throw new NotImplementedException();
  }
}
