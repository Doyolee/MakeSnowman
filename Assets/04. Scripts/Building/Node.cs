using UnityEngine;

//pathFinding���� ����� Node class
public class Node
{
    public bool walkable;

    public Vector3 worldPosition; //����� ���� ��ǥ
    public int nodeX; // nodeArray������ Node x��ǥ
    public int nodeY; // nodeArray������ Node y��ǥ

    public int gCost; // ���� ��忡�� ���� ������ ���µ� �ʿ��� ���
    public int hCost; // ���� ��忡�� ��ǥ ������ ���µ� �ʿ��� ���
    public int fCost { get { return gCost+hCost; } } // ��ü ���

    public Node parent; // �θ� ���

    public Node(bool walkable, Vector3 worldPos, int nodeX, int nodeY)
    {
        this.walkable = walkable;         // ���డ�� ����
        this.worldPosition = worldPos;  // ���� ��ǥ
        this.nodeX = nodeX;              // 2���� �迭 nodeArray������ x ��ǥ
        this.nodeY = nodeY;              // 2���� �迭 nodeArray������ y ��ǥ
    }
}
