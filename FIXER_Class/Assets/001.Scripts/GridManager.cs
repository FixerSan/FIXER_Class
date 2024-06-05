using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEditor;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GridManager : MonoBehaviour
{
    //�̱��� �ν��Ͻ�
    public static GridManager Instance { get; set; }

    //�׸���
    private Grid grid;

    [Header("�׸��� ������")]
    public int gridSize;

    [Header("�׸��� ������")]
    public Vector3 gridOffset;
    

    public void Awake()
    {
        //�̱���
        if (GridManager.Instance == null)
            GridManager.Instance = this;
        else Destroy(this);

        //�׸��� �ʱ�ȭ
        grid = new Grid(gridSize);
    }

    //�׸��忡 �ֱ�
    public void EnGrid(GridObject _gridObject, int _x, int _y, int _z)
    {
        //���� ó��
        if (grid.gridArray[_x, _y, _z] != null)
        {
            Debug.LogError($"[{_x}, {_y}, {_z}] �׸��尡 ������� �ʽ��ϴ�.");
            return;
        }

        //�׸��� ������Ʈ �ʱ�ȭ �� �׸��� �迭�� �ֱ�
        _gridObject.transform.position = new Vector3(_x, _y, _z) + gridOffset;
        grid.gridArray[_x, _y, _z] = _gridObject;
        _gridObject.Init(_x, _y, _z);
    }

    //�׸��� ������Ʈ ��ȯ
    public GridObject GetGridObject(int _x, int _y, int _z)
    {
        return grid.gridArray[_x, _y, _z];
    }

    //�׸��� ����
    public void DeGrid(int _x, int _y, int _z)
    {
        if(grid.gridArray[_x, _y, _z] != null)
        {
            Destroy(grid.gridArray[_x, _y, _z]);
            grid.gridArray[_x, _y, _z] = null;
        }
    }

    public void DeGrid(GridObject _gridObject)
    {
        //���� ó��
        if(_gridObject == null)
        {
            Debug.LogError($"������Ʈ�� ����ֽ��ϴ�.");
            return;
        }

        //�׸��忡�� ����
        grid.gridArray[_gridObject.gridIndexX, _gridObject.gridIndexY, _gridObject.gridIndexZ] = null;
        Destroy(_gridObject);
    }

    //�׸��� �ʱ�ȭ
    public void ClearGrid()
    {
        Loop(DeGrid);
    }

    //�Լ��� GridArray Length ��ŭ �ݺ�
    public void Loop(Action<int,int,int> _action)
    {
        for (int x = 0; x < grid.gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < grid.gridArray.GetLength(1); y++)
            {
                for (int z = 0; z < grid.gridArray.GetLength(2); z++)
                {
                    _action.Invoke(x, y, z);
                }
            }
        }
    }

    //�� �� �׸��� ������Ʈ�� �ִ��� üũ �� �ֱ�
    public void EnGridOnScene()
    {
        Loop(EnGridOnScene);
    }

    //��ǥ �� �׸��� ������Ʈ�� �ִ��� üũ �� �ֱ�
    private void EnGridOnScene(int _x, int _y, int _z)
    {
        Vector3 pos = new Vector3(_x, _y, _z) + gridOffset;         //���� üũ�� ��ǥ
        Vector3 halfExtents = new Vector3(0.45f, 0.45f, 0.45f);     //üũ�� ���� 
        Quaternion quaternion = Quaternion.identity;                //üũ�� ������ ����
        int layerMask = LayerMask.GetMask("GridObject");            //üũ�� ���̾� ����ũ

        //�� üũ
        Collider[] colliderArray = Physics.OverlapBox(pos, halfExtents, Quaternion.identity, layerMask);

        //üũ �� ������Ʈ�� ���� �� ����
        if (colliderArray.Length == 0) return;

        //üũ �� ������Ʈ�� �ΰ� �̻��� �� ����
        if(colliderArray.Length >= 2)
        {
            Debug.LogError($"[{pos.x}, {pos.y}, {pos.z}] ������ ������Ʈ�� ��Ĩ�ϴ�.");
            return;
        }


        //�׸��� ������Ʈ�� EnGrid
        GridObject gridObject = colliderArray[0].GetComponent<GridObject>();
        if (gridObject != null)
            EnGrid(gridObject, _x, _y, _z);
    }

    //�׸��� ����
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 size = new Vector3(gridSize, gridSize, gridSize);
        Vector3 center = Vector3.zero + (size * 0.5f);
        Gizmos.DrawWireCube(center, size);
    }
}

public class Grid
{
    public GridObject[,,] gridArray;
    public Grid(int _gridSize)
    {
        gridArray = new GridObject[_gridSize, _gridSize, _gridSize];
    }
}

[System.Serializable]
public class GridData
{
    public int index;
    public int[,,] gridIndexes;

    public GridData(Grid _grid)
    {
        gridIndexes = new int[_grid.gridArray.GetLength(0), _grid.gridArray.GetLength(1), _grid.gridArray.GetLength(2)];

        for (int x = 0; x < _grid.gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _grid.gridArray.GetLength(1); y++)
            {
                for (int z = 0; z < _grid.gridArray.GetLength(2); z++)
                {
                    if (_grid.gridArray[x,y,z] == null)
                    {
                        gridIndexes[x, y, z] = -1;
                        continue;
                    }

                    gridIndexes[x, y, z] = _grid.gridArray[x, y, z].index;
                }
            }
        }
    }
}

