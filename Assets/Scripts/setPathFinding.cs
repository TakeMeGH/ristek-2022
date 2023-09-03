using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class setPathFinding : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float size = 1f;
    [SerializeField] Vector3 pos = new Vector3(0, 0, 0);
    [SerializeField] int h;
    [SerializeField] int w;
    [SerializeField] private Camera cam;
    [SerializeField] GameObject squareObject;

    GridMaking grid;
    List<Vector3> path;
    GameObject[,] squareList;
    int [,] dir;
    int[] baris = {1, -1, 0, 0};
    int[] kolom = {0, 0, 1, -1};    
    // struct node{
    //   public int x, y;
    //   public node(int x, int y){
    //     this.x = x;
    //     this.y = y;
    //   }
    // };
    [SerializeField] List<Node> setFalseNode = new List<Node>();


    void Start()
    {
      grid = new GridMaking(w, h, size, pos);
      squareList = new GameObject[w,h];
      dir = new int[w, h];
      Debug.Log(SceneManager.GetActiveScene().name);
      if(SceneManager.GetActiveScene().name == "Level 1"){
        setFalseNode = new LevelGrid().getLevel1();
      }
      else if(SceneManager.GetActiveScene().name == "Level 2"){
        setFalseNode = new LevelGrid().getLevel2();
      }
      else if(SceneManager.GetActiveScene().name == "Level 3"){
        setFalseNode = new LevelGrid().getLevel3();
      }
      for(int i = 0; i < setFalseNode.Count; i++){
        grid.setValue(setFalseNode[i].x, setFalseNode[i].y, false);
        // debug
        // GameObject square = Instantiate(squareObject, grid.getRealPos(setFalseNode[i].x, setFalseNode[i].y) + new Vector3(size / 2, size / 2, 0), Quaternion.identity);
        // square.transform.localScale = new Vector3(size, size, 1);
      }      
    }

    
    // Update is called once per frame
    void Update()
    {
        // debug
        // if (Input.GetMouseButtonDown(1)) {
        //     Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        //     if(grid.getByRealPos(mouseWorldPosition) == true){
        //       Vector3 mouseIdx = grid.getIdx(mouseWorldPosition);
        //       grid.setValue((int)mouseIdx.x, (int)mouseIdx.y, false);

        //       GameObject square = Instantiate(squareObject, grid.getRealPos((int)mouseIdx.x, (int)mouseIdx.y) + new Vector3(size / 2, size / 2, 0), Quaternion.identity);
        //       square.transform.localScale = new Vector3(size, size, 1);
        //       squareList[(int)mouseIdx.x, (int)mouseIdx.y] = square;
        //     }
        //     else{
        //       Vector3 mouseIdx = grid.getIdx(mouseWorldPosition);
        //       grid.setValue((int)mouseIdx.x, (int)mouseIdx.y, true);
        //       Destroy(squareList[(int)mouseIdx.x, (int)mouseIdx.y]);
        //     }
        //     Vector3 getIdx = grid.getIdx(mouseWorldPosition);
        //     // Debug.Log(grid.getByRealPos(mouseWorldPosition) + " " + getIdx.x + " " + getIdx.y + " CLICK");
        //     // pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
        //     // pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        // }
        // else if(Input.GetMouseButtonDown(0)){
        //   string s = "";
        //   for(int i = 0; i < w; i++){
        //     for(int j = 0; j < h; j++){
        //       if(grid.getGridVal(i, j) == false){
        //         s += "path.Add(new Node(" + i + ", " + j + "));\n";
        //       }
        //     }
        //   }
        //   Debug.Log(s);
        // }
    }

    

    public List<Vector3> bfs(Vector3 source, Vector3 finish){
      for(int i = 0; i < w; i++){
        for(int j = 0; j < h; j++){
          dir[i,j] = -1;
        }
      }
      List<Vector3> path = new List<Vector3>();
      Vector3 startPoint = grid.getIdx(source);
      if(startPoint.x < 0 || startPoint.x > w || startPoint.y < 0 || startPoint.y >= h){
        return path;
      }
      source = grid.FindNearestByRealPos(source);
      finish = grid.FindNearestByRealPos(finish);
      // Debug.Log("start pos " + source + " " + finish);
      Queue<Node> q = new Queue<Node>();
      Vector3 idx = grid.getIdx(source);
      int sourceX = (int)idx.x;
      int sourceY = (int)idx.y;
      q.Enqueue(new Node(sourceX, sourceY));
      dir[sourceX, sourceY] = 1;
      while(q.Count > 0){
        Node cur = q.Dequeue();
        for(int i = 0; i < 4; i++){
          int nx = cur.x + baris[i];
          int ny = cur.y + kolom[i];
          if(nx >= 0 && nx < w && ny >= 0 && ny < h && grid.getGridVal(nx,ny) == true && dir[nx,ny] == -1){
            dir[nx,ny] = i;
            q.Enqueue(new Node(nx, ny));
            if(grid.getGridVal(nx,ny) == false){
            }
          }
        }
      }
      Vector3 idxFinish = grid.getIdx(finish);

      int curX = (int)idxFinish.x;
      int curY = (int)idxFinish.y;

      path.Add(grid.getRealPos(curX, curY));

      while(true){
        if(dir[curX,curY] == -1) break;
        int nx = curX - baris[dir[curX, curY]];
        int ny = curY - kolom[dir[curX, curY]];
        if(curX == sourceX && curY == sourceY){
          break;
        }
        // Debug.Log(nx + " " + ny);
        path.Add(grid.getRealPos(nx, ny));
        curX = nx; curY = ny;
      }
      // string s = "";
      // for(int i = 0; i < path.Count; i++){
      //   s += path[i].x + " " + path[i].y;
      //   s += '\n';
      // }
      // Debug.Log(s);
      return path;
    }
}
