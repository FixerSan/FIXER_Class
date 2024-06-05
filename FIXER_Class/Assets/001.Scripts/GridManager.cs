using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEditor;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GridManager : MonoBehaviour
{
    //싱글톤 인스턴스
    public static GridManager Instance { get; set; }

    //그리드
    private Grid grid;

    [Header("그리드 사이즈")]
    public int gridSize;

    [Header("그리드 오프셋")]
    public Vector3 gridOffset;
    

    public void Awake()
    {
        //싱글톤
        if (GridManager.Instance == null)
            GridManager.Instance = this;
        else Destroy(this);

        //그리드 초기화
        grid = new Grid(gridSize);
    }

    //그리드에 넣기
    public void EnGrid(GridObject _gridObject, int _x, int _y, int _z)
    {
        //예외 처리
        if (grid.gridArray[_x, _y, _z] != null)
        {
            Debug.LogError($"[{_x}, {_y}, {_z}] 그리드가 비어있지 않습니다.");
            return;
        }

        //그리드 오브젝트 초기화 및 그리드 배열에 넣기
        _gridObject.transform.position = new Vector3(_x, _y, _z) + gridOffset;
        grid.gridArray[_x, _y, _z] = _gridObject;
        _gridObject.Init(_x, _y, _z);
    }

    //그리드 오브젝트 반환
    public GridObject GetGridObject(int _x, int _y, int _z)
    {
        return grid.gridArray[_x, _y, _z];
    }

    //그리드 비우기
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
        //예외 처리
        if(_gridObject == null)
        {
            Debug.LogError($"오브젝트가 비어있습니다.");
            return;
        }

        //그리드에서 빼기
        grid.gridArray[_gridObject.gridIndexX, _gridObject.gridIndexY, _gridObject.gridIndexZ] = null;
        Destroy(_gridObject);
    }

    //그리드 초기화
    public void ClearGrid()
    {
        Loop(DeGrid);
    }

    //함수를 GridArray Length 만큼 반복
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

    //씬 위 그리드 오브젝트가 있는지 체크 후 넣기
    public void EnGridOnScene()
    {
        Loop(EnGridOnScene);
    }

    //좌표 위 그리드 오브젝트가 있는지 체크 후 넣기
    private void EnGridOnScene(int _x, int _y, int _z)
    {
        Vector3 pos = new Vector3(_x, _y, _z) + gridOffset;         //씬을 체크할 좌표
        Vector3 halfExtents = new Vector3(0.45f, 0.45f, 0.45f);     //체크할 범위 
        Quaternion quaternion = Quaternion.identity;                //체크할 범위의 각도
        int layerMask = LayerMask.GetMask("GridObject");            //체크할 레이어 마스크

        //씬 체크
        Collider[] colliderArray = Physics.OverlapBox(pos, halfExtents, Quaternion.identity, layerMask);

        //체크 된 오브젝트가 없을 때 리턴
        if (colliderArray.Length == 0) return;

        //체크 된 오브젝트가 두개 이상일 때 리턴
        if(colliderArray.Length >= 2)
        {
            Debug.LogError($"[{pos.x}, {pos.y}, {pos.z}] 범위에 오브젝트가 겹칩니다.");
            return;
        }


        //그리드 오브젝트를 EnGrid
        GridObject gridObject = colliderArray[0].GetComponent<GridObject>();
        if (gridObject != null)
            EnGrid(gridObject, _x, _y, _z);
    }

    //그리드 범위
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

