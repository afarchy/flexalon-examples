using System.Collections.Generic;
using UnityEngine;

namespace Flexalon.Examples
{
    public class BulletinBoard : MonoBehaviour
    {
        [SerializeField]
        public BulletinBoardItem[] _boardItems;

        private FlexalonGridLayout _grid;
        private List<bool> _occupied = new List<bool>();

        void Start()
        {
            _grid = GetComponent<FlexalonGridLayout>();

            // Fill the grid with empty gameObjects that we can attach to.
            for (int i = 0; i < _grid.Columns * _grid.Rows; i++)
            {
                var go = new GameObject();
                go.transform.SetParent(_grid.transform);
                _occupied.Add(false);
            }

            // Add listeners to the drag events of each item and attach them to the grid.
            foreach (var item in _boardItems)
            {
                var interactable = item.GetComponent<FlexalonInteractable>();
                interactable.DragStart.AddListener((i) => DetachFromGrid(item));
                interactable.DragEnd.AddListener((i) => AttachToGrid(item));
                AttachToGrid(item);
            }
        }

        // Determines which cells have objects already attached.
        private void RefreshOccupied()
        {
            for (int i = 0; i < _occupied.Count; i++)
            {
                _occupied[i] = false;
            }

            foreach (var item in _boardItems)
            {
                var constraint = item.GetComponent<FlexalonConstraint>();
                if (constraint.Target != null)
                {
                    var row = constraint.Target.transform.GetSiblingIndex() / _grid.Columns;
                    var column = constraint.Target.transform.GetSiblingIndex() % _grid.Columns;

                    for (int c = 0; c < item.CellsWide; c++)
                    {
                        for (int r = 0; r < item.CellsTall; r++)
                        {
                            var i = (row + r) * _grid.Columns + (column + c);
                            if (i < _occupied.Count)
                            {
                                _occupied[(int)i] = true;
                            }
                        }
                    }
                }
            }
        }

        // Checks if an item can be attached to a grid cell, based on the results from RefreshOccupied.
        private bool CanPlace(BulletinBoardItem item, Transform target)
        {
            var row = target.GetSiblingIndex() / _grid.Columns;
            var column = target.GetSiblingIndex() % _grid.Columns;

            // Check for out of bounds column.
            if (column + item.CellsWide > _grid.Columns)
            {
                return false;
            }

            // Check for out of bounds row.
            if (row + item.CellsTall > _grid.Rows)
            {
                return false;
            }

            // Check if any cell that the item would cover is already occupied.
            for (int c = 0; c < item.CellsWide; c++)
            {
                for (int r = 0; r < item.CellsTall; r++)
                {
                    var i = (row + r) * _grid.Columns + (column + c);
                    if (_occupied[(int)i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void DetachFromGrid(BulletinBoardItem boardItem)
        {
            var constraint = boardItem.GetComponent<FlexalonConstraint>();
            constraint.Target = null;
        }

        private void AttachToGrid(BulletinBoardItem boardItem)
        {
            RefreshOccupied();

            // Use the top-left corner position to check for the nearest grid cell.
            var collider = boardItem.GetComponent<BoxCollider>();
            var size = Math.Mul(collider.size, boardItem.transform.lossyScale);
            var topLeft = boardItem.transform.up * (size.y * 0.5f) - boardItem.transform.right * (size.x * 0.5f);
            topLeft += boardItem.transform.position;

            // Find the nearest valid grid cell.
            float minDistance = float.MaxValue;
            GameObject nearestTarget = null;
            foreach (Transform child in transform)
            {
                if (CanPlace(boardItem, child))
                {
                    var distance = Vector3.Distance(child.position, topLeft);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestTarget = child.gameObject;
                    }
                }
            }

            // Attach the item to the nearest grid cell using a Flexalon Constraint.
            // Aligns the the top-left corner of the item.
            if (nearestTarget)
            {
                var constraint = boardItem.GetComponent<FlexalonConstraint>();
                constraint.Target = nearestTarget;
                constraint.HorizontalPivot = Align.Start;
                constraint.VerticalPivot = Align.End;
            }
        }
    }
}