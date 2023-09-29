using UnityEngine;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour
{
    public LayerMask unWalkableMask;//����� �� ���� ���̾�

    public Vector2Int bottomLeft; //������ �� ���� ��ǥ (���ϴ� ����� ��ǥ)
    public int nodeArraySizeX, nodeArraySizeY;//���� ����, ���� ��� ��
    public int nodeScale=1; // ����� ũ�� = Ÿ���� ũ��

    public Transform enemySpawner, home; // ���� �����Ǵ� ����, �Ʊ� ����


    Node[,] nodeArray;//������ ���� 2���� �迭

    [HideInInspector]
    public List<Node> path= new List<Node>();  //���� ��θ� ���� �迭

    [HideInInspector]
    public bool canFindPath; // ��θ� ã�� �� �ִ��� üũ

    private void Start()
    {
        FindPath(); // ���� ���� �� ���� 1ȸ ��� Ž�� ����
    }
    public void FindPath()
    {
        canFindPath = true;

        path.Clear(); // path�� ��� ������ �ʱ�ȭ

        nodeArray=new Node[nodeArraySizeX,nodeArraySizeY]; //������ ���� 2���� �迭 ����

        MakeNodeArray(); // nodeArray�� Node���� �ִ´�.

        Node startNode=NodeFromWorldPoint(enemySpawner.position); // Ž�� ������ ���
        Node targetNode=NodeFromWorldPoint(home.position); //������ ���

        List<Node> openSet = new List<Node>(); //Ȯ���� ������ ���� openSet
        HashSet<Node> closedSet = new HashSet<Node>(); //Ȯ���� ���� ������ ���� closedSet

        openSet.Add(startNode); //openset�� ���� ��� �ֱ�

        while (openSet.Count > 0) //���¼¿� Node�� ���������� ��� ����
        {
            Node currentNode = openSet[0]; // ù��° ������ Ȯ�� ���� 

            for(int i=1;i<openSet.Count;i++)//openSet�� ����ִ� ��� ��� �߿�
            {
                //f���� ���� ���� ���(���� f���� ���ٸ� h���� �� ���� ���)�� ���� Ž�� ���� ���� 
                if (openSet[i].fCost <= currentNode.fCost && openSet[i].hCost<currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            //currentNode�� Ž���� �������Ƿ� openSet���� ����� closedSet�� �߰�
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode ==targetNode) //��ǥ ��忡 �����ߴٸ� path�� ����� �ְ� Ž�� ����
            {
                //���� ��η� Ž���� ������ �������� ���󰡸� path�� ��� �޼���
                RetracePath(startNode,targetNode);
               
                canFindPath = true;

                //��� Ž�� ����
                return;
            }

            // �̿� ���鿡�� cost �� �Ҵ��ϰ� openSet�� �־ Ž�� ���
            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                //����Ұ��� ����̰ų� closedSet�� ����� ��� ����
                if (!neighbour.walkable || closedSet.Contains(neighbour)) 
                    continue;

                //gCost(��������� ���� �ּҺ��) + �����忡�� �̿������� ���µ� �ʿ��� ���
                int newCostToNeighbour = currentNode.gCost + GetHCost(currentNode, neighbour);

                //1.���� ������ ���� �ּ� ��� + �����忡�� �̿������� ���� ��� < �̿������� ���� ����� ���
                // - �ش� ����� Cost���� �ּ�ȭ�ؼ� ���� �Ҵ��Ѵ�.

                //2. ���� openSet�� ��ư��� ���� ����� ���
                // - �ش� ����� Cost ���� ���� �Ҵ��ϰ�, openSet�� �ִ´�.
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour; //gCost ����
                    neighbour.hCost = GetHCost(neighbour, targetNode); //hCost ����

                    neighbour.parent = currentNode; //���� ��带 �θ� ����     

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        //��� Ž���� �����µ� path �迭�� ��ΰ� ������� ������ 
        if (path.Count == 0)
        {
            canFindPath = false; // ��� Ž�� �Ұ� üũ
        }

    }

    //���� Node���� nodeArray�� ��� �ִ� �޼ҵ�
    void MakeNodeArray()
    {
        for (int i = 0; i < nodeArraySizeX; i++)
        {
            for (int j = 0; j < nodeArraySizeY; j++)
            {
                //�� ������ �ϴ� �� ��ǥ�� node�� ��ǥ * node ũ�� ���� ���ؼ� ���忡���� ��� ��ǥ�� ���Ѵ�. 
                Vector3 worldPoint = Vector3.right * (bottomLeft.x + i * nodeScale) + Vector3.forward * (bottomLeft.y + j * nodeScale);

                //�̵��� �� �ִ� ������� üũ
                bool walkable = !(Physics.CheckSphere(worldPoint + Vector3.down * (0.2f), nodeScale / 2 - 0.1f, unWalkableMask));

                //(�̵� ����, ���忡���� ��ǥ, �迭������ ��ǥ)
                nodeArray[i, j] = new Node(walkable, worldPoint, i, j);
            }
        }
    }

    //Node�� ���� ��ǥ�� �������� NodeArray �迭 ���� ��ǥ�� ���ϱ� ���� �޼���
    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        //(�ش� ����� ��ǥ - �� �� ��ǥ)�� ����� ������� ������ nodeArray������ node ��ǥ�� ���Ѵ�.
        int x = Mathf.RoundToInt((worldPos.x - bottomLeft.x) / nodeScale);
        int y = Mathf.RoundToInt((worldPos.z - bottomLeft.y) / nodeScale);

        return nodeArray[x, y];
    }

    //���� Ž������ ��忡�� Ž�������� ���� ���(��,��,��,��)���� ã�� ���� �޼���
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        //���� ���� ����� �ֺ� 8���⿡ �ִ� �̿� ����
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                //���� ���� �밢�� ��带 �����ϰ� ��,��,��,�츸 ����
                if (i * j != 0 || (i == 0 && j == 0))
                    continue;

                int checkX = node.nodeX + i;
                int checkY = node.nodeY + j;

                //üũ�ϴ� ��尡 nodeArray �迭�� �ִٸ� neighbours�� �߰�
                if (checkX >= 0 && checkX < nodeArraySizeX && checkY >= 0 && checkY < nodeArraySizeY)
                {
                    neighbours.Add(nodeArray[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    //���������� Ž���� ������ path �迭�� ��� �޼ҵ�
    void RetracePath(Node startNode,Node targetNode)
    {
        //targetNode���� �����ؼ� 
        Node currentNode = targetNode;

        //startNode�� ������ ������
        while(currentNode != startNode) 
        {
            path.Add(currentNode);

            //�θ� ��带 ���� �������� �迭�� �߰�
            currentNode = currentNode.parent;
        }
        path.Reverse();//�迭 ���� ������
    }

    // hcost �� ���ϱ� ���� �޼ҵ�
    // hCost = nodeA���� nodeB���� ��� ��ֹ��� �����ϰ� ���� �ּ� Cost��
    int GetHCost(Node nodeA,Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.nodeX - nodeB.nodeX);
        int dstY = Mathf.Abs(nodeA.nodeY - nodeB.nodeY);

        // ���� ���̸� �������� ������ �� cost�� 1 , �밢������ ������ �� cost�� �뷫 1.4
        // float���� ������ ����� �����Ƿ� cost�� ���� 10, �밢�� 14�� ����
        if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
        else return 14*dstX+10* (dstY - dstX);
    }

}
