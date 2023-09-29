using UnityEngine;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour
{
    public LayerMask unWalkableMask;//통과할 수 없는 레이어

    public Vector2Int bottomLeft; //기준이 될 월드 좌표 (좌하단 노드의 좌표)
    public int nodeArraySizeX, nodeArraySizeY;//맵의 가로, 세로 노드 수
    public int nodeScale=1; // 노드의 크기 = 타일의 크기

    public Transform enemySpawner, home; // 적이 스폰되는 지점, 아군 기지


    Node[,] nodeArray;//노드들을 담을 2차원 배열

    [HideInInspector]
    public List<Node> path= new List<Node>();  //최종 경로를 담을 배열

    [HideInInspector]
    public bool canFindPath; // 경로를 찾을 수 있는지 체크

    private void Start()
    {
        FindPath(); // 게임 시작 시 최초 1회 경로 탐색 실행
    }
    public void FindPath()
    {
        canFindPath = true;

        path.Clear(); // path에 담긴 값들을 초기화

        nodeArray=new Node[nodeArraySizeX,nodeArraySizeY]; //노드들을 담을 2차원 배열 생성

        MakeNodeArray(); // nodeArray에 Node들을 넣는다.

        Node startNode=NodeFromWorldPoint(enemySpawner.position); // 탐색 시작할 노드
        Node targetNode=NodeFromWorldPoint(home.position); //목적지 노드

        List<Node> openSet = new List<Node>(); //확인할 노드들을 담은 openSet
        HashSet<Node> closedSet = new HashSet<Node>(); //확인이 끝난 노드들을 담은 closedSet

        openSet.Add(startNode); //openset에 시작 노드 넣기

        while (openSet.Count > 0) //오픈셋에 Node가 남아있으면 계속 진행
        {
            Node currentNode = openSet[0]; // 첫번째 노드부터 확인 시작 

            for(int i=1;i<openSet.Count;i++)//openSet에 들어있는 모든 노드 중에
            {
                //f값이 가장 낮은 노드(만약 f값이 같다면 h값이 더 낮은 노드)를 다음 탐색 노드로 지정 
                if (openSet[i].fCost <= currentNode.fCost && openSet[i].hCost<currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            //currentNode의 탐색이 끝났으므로 openSet에서 지우고 closedSet에 추가
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode ==targetNode) //목표 노드에 도착했다면 path에 결과값 넣고 탐색 종료
            {
                //최종 경로로 탐색된 노드들을 역순으로 따라가며 path에 담는 메서드
                RetracePath(startNode,targetNode);
               
                canFindPath = true;

                //경로 탐색 종료
                return;
            }

            // 이웃 노드들에게 cost 값 할당하고 openSet에 넣어서 탐색 대기
            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                //통행불가한 노드이거나 closedSet의 노드일 경우 제외
                if (!neighbour.walkable || closedSet.Contains(neighbour)) 
                    continue;

                //gCost(현재노드까지 오는 최소비용) + 현재노드에서 이웃노드까지 가는데 필요한 비용
                int newCostToNeighbour = currentNode.gCost + GetHCost(currentNode, neighbour);

                //1.현재 노드까지 오는 최소 비용 + 현재노드에서 이웃노드까지 가는 비용 < 이웃노드까지 가는 비용일 경우
                // - 해당 노드의 Cost값을 최소화해서 새로 할당한다.

                //2. 아직 openSet에 들아가지 않은 노드일 경우
                // - 해당 노드의 Cost 값을 새로 할당하고, openSet에 넣는다.
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour; //gCost 설정
                    neighbour.hCost = GetHCost(neighbour, targetNode); //hCost 설정

                    neighbour.parent = currentNode; //현재 노드를 부모 노드로     

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        //경로 탐색이 끝났는데 path 배열에 경로가 들어있지 않으면 
        if (path.Count == 0)
        {
            canFindPath = false; // 경로 탐색 불가 체크
        }

    }

    //맵의 Node들을 nodeArray에 모두 넣는 메소드
    void MakeNodeArray()
    {
        for (int i = 0; i < nodeArraySizeX; i++)
        {
            for (int j = 0; j < nodeArraySizeY; j++)
            {
                //맵 좌측과 하단 끝 좌표에 node의 좌표 * node 크기 값을 더해서 월드에서의 노드 좌표를 구한다. 
                Vector3 worldPoint = Vector3.right * (bottomLeft.x + i * nodeScale) + Vector3.forward * (bottomLeft.y + j * nodeScale);

                //이동할 수 있는 노드인지 체크
                bool walkable = !(Physics.CheckSphere(worldPoint + Vector3.down * (0.2f), nodeScale / 2 - 0.1f, unWalkableMask));

                //(이동 여부, 월드에서의 좌표, 배열에서의 좌표)
                nodeArray[i, j] = new Node(walkable, worldPoint, i, j);
            }
        }
    }

    //Node의 월드 좌표를 바탕으로 NodeArray 배열 상의 좌표를 구하기 위한 메서드
    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        //(해당 노드의 좌표 - 맵 끝 좌표)를 노드의 사이즈로 나누어 nodeArray에서의 node 좌표를 구한다.
        int x = Mathf.RoundToInt((worldPos.x - bottomLeft.x) / nodeScale);
        int y = Mathf.RoundToInt((worldPos.z - bottomLeft.y) / nodeScale);

        return nodeArray[x, y];
    }

    //현재 탐색중인 노드에서 탐색가능한 인접 노드(상,하,좌,우)들을 찾기 위한 메서드
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        //현재 노드와 노드의 주변 8방향에 있는 이웃 노드들
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                //현재 노드와 대각선 노드를 제외하고 상,하,좌,우만 진행
                if (i * j != 0 || (i == 0 && j == 0))
                    continue;

                int checkX = node.nodeX + i;
                int checkY = node.nodeY + j;

                //체크하는 노드가 nodeArray 배열에 있다면 neighbours에 추가
                if (checkX >= 0 && checkX < nodeArraySizeX && checkY >= 0 && checkY < nodeArraySizeY)
                {
                    neighbours.Add(nodeArray[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    //최종적으로 탐색한 노드들을 path 배열에 담는 메소드
    void RetracePath(Node startNode,Node targetNode)
    {
        //targetNode부터 시작해서 
        Node currentNode = targetNode;

        //startNode에 도착할 때까지
        while(currentNode != startNode) 
        {
            path.Add(currentNode);

            //부모 노드를 따라 역순으로 배열에 추가
            currentNode = currentNode.parent;
        }
        path.Reverse();//배열 순서 뒤집기
    }

    // hcost 를 구하기 위한 메소드
    // hCost = nodeA부터 nodeB까지 모든 장애물을 무시하고 가는 최소 Cost값
    int GetHCost(Node nodeA,Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.nodeX - nodeB.nodeX);
        int dstY = Mathf.Abs(nodeA.nodeY - nodeB.nodeY);

        // 노드들 사이를 직선으로 움직일 때 cost는 1 , 대각선으로 움직일 때 cost는 대략 1.4
        // float보다 정수가 계산이 빠르므로 cost를 직선 10, 대각선 14로 설정
        if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
        else return 14*dstX+10* (dstY - dstX);
    }

}
