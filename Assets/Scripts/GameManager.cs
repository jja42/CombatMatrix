using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool paused;
    public Enemy enemy;
    public bool finish;
    public bool menu;

    [System.Serializable]
    public class GridSquare
    {
        public Vector3 pos;
        public Vector2 index;
        public GridSquare(Vector3 position, int x_index, int y_index)
        {
            pos = position;
            index = new Vector2(x_index, y_index);
        }
    }

    [SerializeField]
    public List<GridSquare> PlayerGrid;
    public List<GridSquare> EnemyGrid;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !finish && !menu)
        {
            paused = !paused;
            UI_Manager.instance.Pause_UI.SetActive(paused);
        }
    }

    public GridSquare GetGridSquare(int x, int y, bool player)
    {
        if (player)
        {
            Vector2 search_index = new Vector2(x, y);
            return PlayerGrid.Find(square => square.index.Equals(search_index));
        }
        else
        {
            Vector2 search_index = new Vector2(x, y);
            return EnemyGrid.Find(square => square.index.Equals(search_index));
        }
    }

    public void UpdateEnemyTargetPosition(Vector2 index) 
    {
        enemy.TargetPos = GetGridSquare((int)index.x, (int)index.y, false).index;
    }

    public void Loss()
    {
        finish = true;
        UI_Manager.instance.Loss_UI.SetActive(true);
    }
    public void Win()
    {
        finish = true;
        UI_Manager.instance.Win_UI.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
