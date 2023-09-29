using UnityEngine;

//pathFinding에서 사용할 Node class
public class Node
{
    public bool walkable;

    public Vector3 worldPosition; //노드의 월드 좌표
    public int nodeX; // nodeArray에서의 Node x좌표
    public int nodeY; // nodeArray에서의 Node y좌표

    public int gCost; // 시작 노드에서 현재 노드까지 오는데 필요한 비용
    public int hCost; // 현재 노드에서 목표 노드까지 가는데 필요한 비용
    public int fCost { get { return gCost+hCost; } } // 전체 비용

    public Node parent; // 부모 노드

    public Node(bool walkable, Vector3 worldPos, int nodeX, int nodeY)
    {
        this.walkable = walkable;         // 통행가능 여부
        this.worldPosition = worldPos;  // 월드 좌표
        this.nodeX = nodeX;              // 2차원 배열 nodeArray에서의 x 좌표
        this.nodeY = nodeY;              // 2차원 배열 nodeArray에서의 y 좌표
    }
}
