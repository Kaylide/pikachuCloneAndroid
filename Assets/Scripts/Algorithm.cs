using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Algorithm : MonoBehaviour
{
    public int Row, Col;// Số dòng và số cột của ma trận.
    public int[][] Matrix;// Khai báo một ma trận.
    public Vector3[][] Pos; // Ma trận và vị trí của các pikachu tương ứng.
    public GameController controller;
    public static int Min_X; // biên lưu thông tin vị trí nhỏ nhất của mảng pikachu
    public static int Min_Y; // biên lưu thông tin vị trí nhỏ nhất của mảng pikachu
    public  int Width; // chiều cao mặt định của một pikachu
    public  int Height; // chiều dài mặt định của một pikachu
    private int barrier = 0;
    public Sprite[] SourceSprites;
    public GridLayoutGroup gridLayout;
    private LineRenderer lineRenderer;
    //  public List<Sprite> ListSprites = new List<Sprite>();
    void Awake()
    {       
        gridLayout = gameObject.GetComponent<GridLayoutGroup>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    // Start is called before the first frame update
    public void CreateMatrix(int row, int col) {
        Row = row;
        Col = col;     
        Matrix = new int[Row][];
        Pos = new Vector3[Row][];
        Min_X = -Col * Width / 2;
        Min_Y = -(Row) * Height / 2;

        for (int i = 0; i < Row; i++)
        {
            Matrix[i] = new int[Col];
            Pos[i] = new Vector3[Col];
            for (int j = 0; j < Col; j++)
            {
                Pos[i][j] = new Vector3(0, 0, 0);
                Pos[i][j].x = Min_X + j * Width + Width / 2;
                Pos[i][j].y = Min_Y + i * Height + Height / 2;
                Pos[i][j].z = i / 10;
            }
        }
    }

    public void showMatrix()
    {
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Col; j++)
            {
                Debug.Log("" + Matrix[i][j]);
            }
        }
    }
    // Update is called once per frame
    public GameObject AddPikachu(int index, Vector3 pos, int width, int height, int i, int j)
    {
        GameObject btn = Instantiate(Resources.Load("Button")) as GameObject;
        btn.transform.position = pos;
        btn.transform.SetParent(this.transform, true);
        Sprite sprite = controller.spriteList[index];
        btn.GetComponent<Image>().sprite = sprite;
        btn.transform.localScale = new Vector3(Mathf.Abs(width * 1.0f), Mathf.Abs(-height * 1.0f), 1);
        btn.name = i + "," + j;
        gridLayout.spacing = new Vector2(btn.transform.localScale.x, btn.transform.localScale.y);
        gridLayout.cellSize = new Vector2(1, 1);
        return btn;
    }
    public void RandomMap()
    {
        int index = 0;
        for (int i = 0; i < Row; i++)
        {

            for (int j = 0; j < Col; j++)
            {
                if (i == 0 || i == Row - 1 || j == 0 || j == Col - 1)
                {
                    GameObject temp = Instantiate(Resources.Load("Wall")) as GameObject;
                    temp.transform.position = Pos[i][j];
                    temp.name = "Wall";
                    temp.layer = 5;
                    temp.transform.SetParent(this.transform, true);
                    temp.transform.localScale = new Vector3(Mathf.Abs(Width / 2 * 1.0f), Mathf.Abs(-Height / 2 * 1.0f), 1);
                }
                else {
                    AddPikachu(index, Pos[i][j], Width, Height, i, j);
                    Matrix[i][j] = int.Parse(controller.spriteList[index].name);
                    index++;
                }
            }
        }
    }
    public void GetRamdomSprite(List<Sprite> sprites, int level) {
        int totalSprite4 = 0;
        int totalSprite2 = 0;
        int value = 0;
        int count = 0;
        if ((Row - 2) * (Col - 2) >= 30)
        {
            totalSprite4 = (((Row - 2) * (Col - 2)) / 4) - level; // 15
            Debug.Log("" + totalSprite4);

            totalSprite2 = ((Row - 2) * (Col - 2) - (totalSprite4 * 4)) / 2;// 12
            Debug.Log("" + totalSprite2);
        }
        else {
            totalSprite2 = (Row - 2) * (Col - 2) / 2;
        }
        int Max = totalSprite4 + totalSprite2 + level;
        if (Max > 35 && controller.typeGame.Equals("ClassicGame")) {
            Max = 35;
        }
        if (Max > 24 && controller.typeGame.Equals("ExtendGame"))
        {
            Max = 24;
        }
        while (totalSprite4 > 0) {
            value = UnityEngine.Random.Range(0, Max);
            if (!sprites.Contains(SourceSprites[value]))
            {
                count = 0;
                while (count < 4)
                {
                    sprites.Add(SourceSprites[value]);
                    count++;
                }
                totalSprite4--;
            }
        }
        while (totalSprite2 > 0)
        {
            value = UnityEngine.Random.Range(0, Max);
            if (!sprites.Contains(SourceSprites[value]))
            {
                count = 0;
                while (count < 2)
                {
                    sprites.Add(SourceSprites[value]);
                    count++;
                }
                totalSprite2--;
            }
        }
        Debug.Log("Add Ramdom Sprite were finished");
        ShuffleListSprite(sprites);
    }
    public void ShuffleListSprite(List<Sprite> sprites) {
        Sprite temp;
        for (int i = 0; i < sprites.Count; i++) {
            temp = sprites[i];
            int random = UnityEngine.Random.Range(i, sprites.Count);
            sprites[i] = sprites[random];
            sprites[random] = temp;
        }
        Debug.Log("Shuffle Sprite success");
    }
    public bool checkLineX(int y1, int y2, int x)
    {
        // find point have column max and min
        Debug.Log("Check Line X");
        int min = Mathf.Min(y1, y2);
        int max = Mathf.Max(y1, y2);
        // run column
        for (int y = min + 1; y < max; y++)
        {
            if (Matrix[x][y] > 0)
            { // if see barrier then die
                Debug.Log("die: " + x + "" + y);
                return false;
            }
            Debug.Log("ok: " + x + "" + y);
        }
        // not die -> success
        return true;
    }
    public bool checkLineY(int x1, int x2, int y)
    {
        Debug.Log("Check Line y");
        int min = Mathf.Min(x1, x2);
        int max = Mathf.Max(x1, x2);
        for (int x = min + 1; x < max; x++)
        {
            if (Matrix[x][y] > 0)
            {
                Debug.Log("die: " + x + "" + y);
                return false;
            }
            Debug.Log("ok: " + x + "" + y);
        }
        return true;
    }
    public int checkRectX(Vector3Int p1, Vector3Int p2)
    {
        // find point have y min and max
        Debug.Log("Check Rect X");
        Vector3Int pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y)
        {
            pMinY = p2;
            pMaxY = p1;
        }
        for (int y = pMinY.y; y < pMaxY.y; y++)
        {
            // check three line
            if (y > pMinY.y && Matrix[pMinY.x][y] > 0)
            {
                return -1;
            }
            if ((Matrix[pMaxY.x][y] == 0 || y == pMaxY.y) && checkLineY(pMinY.x, pMaxY.x, y)
                && checkLineX(y, pMaxY.y, pMaxY.x))
            {

                Debug.Log("Rect x");
                Debug.Log("(" + pMinY.x + "," + pMinY.y + ") -> ("
                        + pMinY.x + "," + y + ") -> (" + pMaxY.x + "," + y
                        + ") -> (" + pMaxY.x + "," + pMaxY.y + ")");
                // if three line is true return column y
                return y;
            }
        }
        // have a line in three line not true then return -1
        return -1;
    }

    public int checkRectY(Vector3Int p1, Vector3Int p2)
    {
        // find point have y min
        Debug.Log("Check Rect Y");
        Vector3Int pMinX = p1, pMaxX = p2;
        if (p1.x > p2.x)
        {
            pMinX = p2;
            pMaxX = p1;
        }
        // find line and y begin
        for (int x = pMinX.x; x < pMaxX.x; x++)
        {
            if (x > pMinX.x && Matrix[x][pMinX.y] > 0)
            {
                return -1;
            }
            if ((Matrix[x][pMaxX.y] == 0 || x == pMaxX.x)
                    && checkLineX(pMinX.y, pMaxX.y, x)
                    && checkLineY(x, pMaxX.x, pMaxX.y))
            {

                Debug.Log("Rect y");
                Debug.Log("(" + pMinX.x + "," + pMinX.y + ") -> (" + x
                        + "," + pMinX.y + ") -> (" + x + "," + pMaxX.y
                        + ") -> (" + pMaxX.x + "," + pMaxX.y + ")");
                return x;
            }
        }
        return -1;
    }
    public int checkMoreLineX(Vector3Int p1, Vector3Int p2, int type)
    {
        // find point have y min
        Debug.Log("Check More Line X");
        Vector3Int pMinY = p1, pMaxY = p2;
        if (p1.y > p2.y)
        {
            pMinY = p2;
            pMaxY = p1;
        }
        // find line and y begin
        int y = pMaxY.y + type;
        int row = pMinY.x;
        int colFinish = pMaxY.y;
        if (type == -1)
        {
            colFinish = pMinY.y;
            y = pMinY.y + type;
            row = pMaxY.x;
            Debug.Log("colFinish = " + colFinish);
        }
        // check more
        if ((Matrix[row][colFinish] == 0 || pMinY.y == pMaxY.y)
                && checkLineX(pMinY.y, pMaxY.y, row))
        {
            while (Matrix[pMinY.x][y] == barrier
                    && Matrix[pMaxY.x][y] == barrier)
            {
                if (checkLineY(pMinY.x, pMaxY.x, y))
                {

                    Debug.Log("TH X " + type);
                    Debug.Log("(" + pMinY.x + "," + pMinY.y + ") -> ("
                            + pMinY.x + "," + y + ") -> (" + pMaxY.x + "," + y
                            + ") -> (" + pMaxY.x + "," + pMaxY.y + ")");
                    return y;
                }
                y += type;
            }
        }
        return -1;
    }

    public int checkMoreLineY(Vector3Int p1, Vector3Int p2, int type)
    {
        Debug.Log("Check More Line Y");
        Vector3Int pMinX = p1, pMaxX = p2;
        if (p1.x > p2.x)
        {
            pMinX = p2;
            pMaxX = p1;
        }
        int x = pMaxX.x + type;
        int col = pMinX.y;
        int rowFinish = pMaxX.x;
        if (type == -1)
        {
            rowFinish = pMinX.x;
            x = pMinX.x + type;
            col = pMaxX.y;
        }
        if ((Matrix[rowFinish][col] == 0 || pMinX.x == pMaxX.x)
                && checkLineY(pMinX.x, pMaxX.x, col))
        {
            while (Matrix[x][pMinX.y] == barrier
                    && Matrix[x][pMaxX.x] == barrier)
            {
                if (checkLineX(pMinX.y, pMaxX.y, x))
                {
                    Debug.Log("TH Y " + type);
                    Debug.Log("(" + pMinX.x + "," + pMinX.y + ") -> ("
                            + x + "," + pMinX.y + ") -> (" + x + "," + pMaxX.y
                            + ") -> (" + pMaxX.x + "," + pMaxX.y + ")");
                    return x;
                }
                x += type;
            }
        }
        return -1;
    }
    public void ConnectTowPoint(Vector3Int p1, Vector3Int p2)
    {
        lineRenderer = new LineRenderer();
        lineRenderer = controller.MatricBtn[p1.x][p1.y].GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, controller.MatricBtn[p1.x][p1.y].transform.position);
        lineRenderer.SetPosition(1, controller.MatricBtn[p2.x][p2.y].transform.position);

    }
    public IEnumerator ClearLine(Vector3Int p) {
        yield return new WaitForSeconds(0.3f);
        lineRenderer = controller.MatricBtn[p.x][p.y].GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        Debug.Log("Clear Line");
    }
    public bool checkdLine(Vector3Int p1, Vector3Int p2) {
        if (!p1.Equals(p2) && Matrix[p1.x][p1.y] == Matrix[p2.x][p2.y])
        {
            // check line with x
            if (p1.x == p2.x)
            {
                if (checkLineX(p1.y, p2.y, p1.x))
                    return true;
            }
            // check line with y
            if (p1.y == p2.y)
            {
                if (checkLineY(p1.x, p2.x, p1.y))
                    return true;
            }
            int t = -1; // t is column find
            // check in rectangle with x
            if ((t = checkRectX(p1, p2)) != -1)
                return true;
            // check in rectangle with y
            if ((t = checkRectY(p1, p2)) != -1)
                return true;
            // check more right
            if ((t = checkMoreLineX(p1, p2, 1)) != -1)
                return true;
            // check more left
            if ((t = checkMoreLineX(p1, p2, -1)) != -1)
                return true;
            // check more down
            if ((t = checkMoreLineY(p1, p2, 1)) != -1)
                return true;
            // check more up
            if ((t = checkMoreLineY(p1, p2, -1)) != -1)
                return true;
        }
        return false;
    }
    public void GoRightButton() {
        for (int i = 1; i < Row - 1; i++) {
            for (int j = Col - 2; j > 1; j--) {              
                if (Matrix[i][j] < 0 && j > 1 )
                {
                    if (Matrix[i][j - 1] < 0)
                    {
                        if (Matrix[i][j - 2] > 0 && j - 2 > 0) {
                            SwapButtonX(i, j - 1, -1);
                            SwapButtonX(i, j, -1);
                        }                                                 
                    }
                    else if (Matrix[i][j - 1] > 0)
                    {
                        SwapButtonX(i, j, -1);
                    }                   
                }                                                       
            }
        }
    }
    public void GoLeftButton()
    {
        for (int i = 1; i < Row - 1; i++)
        {
            for (int j = 1; j < Col - 1; j++)
            {
                if (Matrix[i][j] < 0 && j < Col - 1)
                {
                    if (Matrix[i][j + 1] < 0)
                    {
                        if (Matrix[i][j + 2] > 0 && j + 2 < Col - 1)
                        {
                            SwapButtonX(i, j + 1, 1);
                            SwapButtonX(i, j, 1);
                        }
                    }
                    else if (Matrix[i][j + 1] > 0)
                    {
                        SwapButtonX(i, j, 1);
                    }
                }
            }
        }
    }
    public void GoBottomtButton()
    {
        for (int i = 1; i < Row - 1; i++)
        {
            for (int j = 1; j < Col - 1; j++)
            {
                if (Matrix[i][j] < 0 && i < Row - 1)
                {
                    if (Matrix[i + 1][j] < 0)
                    {
                        if (Matrix[i + 2][j] > 0 && i + 2 < Row - 1)
                        {
                            SwapButtonY(i + 1, j, 1);
                            SwapButtonY(i, j, 1);
                        }
                    }
                    else if (Matrix[i + 1][j] > 0)
                    {
                        SwapButtonY(i, j, 1);
                    }
                }
            }
        }
    }
    public void GoTopButton()
    {
        for (int i = Row - 2; i > 1 ; i--)
        {
            for (int j = 1; j < Col -1 ; j++)
            {
                if (Matrix[i][j] < 0 && i > 1)
                {
                    if (Matrix[i-1][j] < 0)
                    {
                        if (Matrix[i-2][j] > 0 && i - 2 > 0)
                        {
                            SwapButtonY(i - 1, j, -1);
                            SwapButtonY(i, j, -1);
                        }
                    }
                    else if (Matrix[i - 1][j] > 0)
                    {
                        SwapButtonY(i, j, -1);
                    }
                }
            }
        }
    }
    public void UpdateValueButton() {
        for (int i = 1; i < Row - 1; i++)
        {
            for (int j = 1; j < Col - 1; j++)
            {
                if (Matrix[i][j] < 0)
                {
                    Matrix[i][j] = 0;
                    controller.btnList.Remove(controller.MatricBtn[i][j]);
                    Debug.Log("Update value success !");                   
                }
            }
        }       
    }
    public void SwapButtonX( int i ,int j, int type) {
        Matrix[i][j] = Matrix[i][j +type];
        Matrix[i][j + type ] = -1;
        Vector3 Pos1 = controller.MatricBtn[i][j].transform.position;
        Sprite sprite1 = controller.MatricBtn[i][j].transform.GetComponent<Button>().image.sprite;
        controller.MatricBtn[i][j].transform.position = controller.MatricBtn[i][j + type ].transform.position;
        controller.MatricBtn[i][j + type ].transform.position = Pos1;
        controller.MatricBtn[i][j].transform.GetComponent<Button>().image.sprite = controller.MatricBtn[i][j +type].transform.GetComponent<Button>().image.sprite;
        controller.MatricBtn[i][j + type].transform.GetComponent<Button>().image.sprite = sprite1;
        controller.MatricBtn[i][j].transform.GetComponent<Button>().interactable = true;
        controller.MatricBtn[i][j + type ].transform.GetComponent<Button>().interactable = false;
        controller.MatricBtn[i][j].transform.GetComponent<Image>().color = new Color (1,1,1,1);
        controller.MatricBtn[i][j + type].transform.GetComponent<Image>().color = new Color (0,0,0,0);
    }
    public void SwapButtonY(int i, int j, int type)
    {
        Matrix[i][j] = Matrix[i+ type][j];
        Matrix[i+type][j] = -1;
        Vector3 Pos1 = controller.MatricBtn[i][j].transform.position;
        Sprite sprite1 = controller.MatricBtn[i][j].transform.GetComponent<Button>().image.sprite;
        controller.MatricBtn[i][j].transform.position = controller.MatricBtn[i + type][j].transform.position;
        controller.MatricBtn[i + type][j].transform.position = Pos1;
        controller.MatricBtn[i][j].transform.GetComponent<Button>().image.sprite = controller.MatricBtn[i + type][j].transform.GetComponent<Button>().image.sprite;
        controller.MatricBtn[i + type][j].transform.GetComponent<Button>().image.sprite = sprite1;
        controller.MatricBtn[i][j].transform.GetComponent<Button>().interactable = true;
        controller.MatricBtn[i + type][j].transform.GetComponent<Button>().interactable = false;
        controller.MatricBtn[i][j].transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        controller.MatricBtn[i + type][j].transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);

    }
    public bool checkTwoPoint(Vector3Int p1, Vector3Int p2)
    {
        
        if (!p1.Equals(p2) && Matrix[p1.x][p1.y] == Matrix[p2.x][p2.y]) {
            // check line with x
            if (p1.x == p2.x)
            {
                if (checkLineX(p1.y, p2.y, p1.x))
                {
                    ConnectTowPoint(p1, p2);
                    StartCoroutine(ClearLine(p1));                   
                    return true ;
                }
            }
            // check line with y
            if (p1.y == p2.y)
            {
                if (checkLineY(p1.x, p2.x, p1.y))
                {
                    ConnectTowPoint(p1, p2);
                    StartCoroutine(ClearLine(p1));
                    ClearLine(p1);
                    return true;
                }
            }
            int t = -1; // t is column find
            // check in rectangle with x
            if ((t = checkRectX(p1, p2)) != -1)
            {
                ConnectTowPoint(p1, new Vector3Int(p1.x, t, 0));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector3Int(p2.x, t, 0));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector3Int(p1.x, t, 0), new Vector3Int(p2.x, t, 0));
                StartCoroutine(ClearLine(new Vector3Int(p1.x, t, 0)));
               // ClearLine(lineRenderer, new Vector3Int(p1.x, t, 0));
                return true;
            }

            // check in rectangle with y
            if ((t = checkRectY(p1, p2)) != -1)
            {
                ConnectTowPoint(p1, new Vector3Int(t, p1.y, 0));
                StartCoroutine(ClearLine( p1));              
                ConnectTowPoint(p2, new Vector3Int(t, p2.y, 0));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector3Int(t, p1.y, 0), new Vector3Int(t, p2.y, 0));
                StartCoroutine(ClearLine(new Vector3Int(t, p1.y, 0)));
                return true;
            }
            // check more right
            if ((t = checkMoreLineX(p1, p2, 1)) != -1)
            {
                ConnectTowPoint(p1, new Vector3Int(p1.x, t, 0));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector3Int(p2.x, t, 0));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector3Int(p1.x, t, 0), new Vector3Int(p2.x, t, 0));
                StartCoroutine(ClearLine(new Vector3Int(p1.x, t, 0)));
                ClearLine(new Vector3Int(p1.x, t, 0));
                return true;
            }
            // check more left
            if ((t = checkMoreLineX(p1, p2, -1)) != -1)
            {
                ConnectTowPoint(p1, new Vector3Int(p1.x, t, 0));
                StartCoroutine(ClearLine(p1));              
                ConnectTowPoint(p2, new Vector3Int(p2.x, t, 0));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector3Int(p1.x, t, 0), new Vector3Int(p2.x, t, 0));
                StartCoroutine(ClearLine(new Vector3Int(p1.x, t, 0)));
                return true;
            }
            // check more down
            if ((t = checkMoreLineY(p1, p2, 1)) != -1)
            {
                ConnectTowPoint(p1, new Vector3Int(t, p1.y, 0));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector3Int(t, p2.y, 0));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector3Int(t, p1.y, 0), new Vector3Int(t, p2.y, 0));
                StartCoroutine(ClearLine(new Vector3Int(t, p1.y, 0)));
                return true;
            }
            // check more up
            if ((t = checkMoreLineY(p1, p2, -1)) != -1)
            {
                ConnectTowPoint(p1, new Vector3Int(t, p1.y, 0));
                StartCoroutine(ClearLine(p1));
                ConnectTowPoint(p2, new Vector3Int(t, p2.y, 0));
                StartCoroutine(ClearLine(p2));
                ConnectTowPoint(new Vector3Int(t, p1.y, 0), new Vector3Int(t, p2.y, 0));
                StartCoroutine(ClearLine(new Vector3Int(t, p1.y, 0)));
                return true;
            }           
        }
        return false;
    }
    public int getRow()
    {
        return Row;
    }

    public void setRow(int row)
    {
        this.Row = row;
    }

    public int getCol()
    {
        return Col;
    }

    public void setCol(int col)
    {
        this.Col = col;
    }

    public int[][] getMatrix()
    {
        return Matrix;
    }

    public void setMatrix(int[][] matrix)
    {
        this.Matrix = matrix;
    }
}
