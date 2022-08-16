using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] float speedOfEnemy = 0.8f;

    List<Node> path = new List<Node>();
    
    Enemy enemy;

    pathFinder pathFind;
    GridManager gridManager;

    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFind = FindObjectOfType<pathFinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathFind.StartCoordinates;
        }else
        {
            coordinates = gridManager.GetCoordinatesFromPos(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFind.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoords(pathFind.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.PenaltyGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for(int i = 1;i < path.Count;i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos  = gridManager.GetPositionFromCoords(path[i].coordinates);
            float travelPercent = 0f;

            //changing rotation to face waypoint
            transform.LookAt(endPos);

            //Moving the object smoothly
            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * Mathf.Abs(speedOfEnemy);
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
        }

        FinishPath(); 
    }

}
