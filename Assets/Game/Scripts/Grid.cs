using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int Height;
    public int Width;
    public float CellSize;
    public Vector3 Origin;

    private LineRenderer LineRenderer;
    private List<Vector3> positions = new List<Vector3>();
    private bool lastPositionOnLeft = true;
    private bool lastPositionOnBottom = true;

    private void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();

        CreateGridFromLine();
    }

    /// <summary>
    /// Рисует сетку, начиная с верхнего левого угла, проводя линию до ширины сетки
    /// а затем вниз на один CellSize. Затем он делает то же самое, но сначала направляется влево.
    /// Как только он достигнет дна, он завершает последнюю строку, а затем
    /// начиная с нижнего левого угла, он идет в верхний левый угол и на один CellSize и
    /// обратно вниз и на один CellSize и обратно вверх, пока не закончит.
    /// </summary>
    private void CreateGridFromLine()
    {
        var offset = Origin + new Vector3(
            -Width * CellSize / 2, 0, Height * CellSize / 2);

        positions.Add(offset);

        for (int y = 0; y < Height; y++)
        {
            positions.Add(GetNextHorizontalEnd());
            positions.Add(GetLastPosition() + new Vector3(0, 0, -CellSize));
        }

        // add final horizontal line
        positions.Add(GetNextHorizontalEnd());
        if (!lastPositionOnLeft)
        {
            positions.Add(GetNextHorizontalEnd());
        }

        for (int x = 0; x < Width; x++)
        {
            positions.Add(GetNextVerticalEnd());
            positions.Add(GetLastPosition() + new Vector3(CellSize, 0, 0));
        }

        // add final vertical line
        positions.Add(GetNextVerticalEnd());

        LineRenderer.positionCount = positions.Count;
        LineRenderer.SetPositions(positions.ToArray());
    }

    private Vector3 GetNextHorizontalEnd()
    {
        var end = GetLastPosition() + new Vector3(
            (Width * CellSize) * (lastPositionOnLeft ? 1 : -1), 0, 0);

        lastPositionOnLeft = !lastPositionOnLeft;
        return end;
    }

    private Vector3 GetNextVerticalEnd()
    {
        var end = GetLastPosition() + new Vector3(
            0, 0, (Height * CellSize) * (lastPositionOnBottom ? 1 : -1));

        lastPositionOnBottom = !lastPositionOnBottom;
        return end;
    }

    private Vector3 GetLastPosition()
    {
        return positions[positions.Count - 1];
    }
}